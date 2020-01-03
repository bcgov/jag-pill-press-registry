﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public class SharePointFileManager
    {
        public const string DefaultDocumentListTitle = "Account";
        public const string ApplicationDocumentListTitle = "incident";
        public const string ApplicationDocumentUrlTitle = "incident";
        public const string ContactDocumentListTitle = "contact";
        public const string AccountDocumentListTitle = "account";
        public const string WorkertDocumentListTitle = "Worker Qualification";

        private AuthenticationResult authenticationResult;

        public string OdataUri { get; set; }
        public string ServerAppIdUri { get; set; }
        public string WebName { get; set; }
        public string ApiEndpoint { get; set; }
        public string NativeBaseUri { get; set; }
        string Authorization { get; set; }
        private HttpClient _Client;
        private string Digest;
        private string FedAuthValue;
        private CookieContainer _CookieContainer;
        private HttpClientHandler _HttpClientHandler;

        public SharePointFileManager(IConfiguration Configuration)
        {
            // create the HttpClient that is used for our direct REST calls.
            _CookieContainer = new CookieContainer();
            _HttpClientHandler = new HttpClientHandler() { UseCookies = true, AllowAutoRedirect = false, CookieContainer = _CookieContainer };
            _Client = new HttpClient(_HttpClientHandler);

            _Client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");

            // SharePoint configuration settings.

            string sharePointServerAppIdUri = Configuration["SHAREPOINT_SERVER_APPID_URI"];
            string sharePointOdataUri = Configuration["SHAREPOINT_ODATA_URI"];
            string sharePointWebname = Configuration["SHAREPOINT_WEBNAME"];
            string sharePointNativeBaseURI = Configuration["SHAREPOINT_NATIVE_BASE_URI"];

            // ADFS using fed auth

            string sharePointStsTokenUri = Configuration["SHAREPOINT_STS_TOKEN_URI"]; // Full URI to the STS service we will use to get the initial token.
            string sharePointRelyingPartyIdentifier = Configuration["SHAREPOINT_RELYING_PARTY_IDENTIFIER"]; // use Fiddler to grab this from an interactive session.  Will normally start with urn:
            string sharePointUsername = Configuration["SHAREPOINT_USERNAME"]; // Service account username.  Be sure to add this user to the SharePoint instance.
            string sharePointPassword = Configuration["SHAREPOINT_PASSWORD"]; // Service account password

            // SharePoint Online
            string sharePointAadTenantId = Configuration["SHAREPOINT_AAD_TENANTID"];
            string sharePointClientId = Configuration["SHAREPOINT_CLIENT_ID"];
            string sharePointCertFileName = Configuration["SHAREPOINT_CERTIFICATE_FILENAME"];
            string sharePointCertPassword = Configuration["SHAREPOINT_CERTIFICATE_PASSWORD"];

            // Basic Auth (SSG API Gateway)
            string ssgUsername = Configuration["SSG_USERNAME"];  // BASIC authentication username
            string ssgPassword = Configuration["SSG_PASSWORD"];  // BASIC authentication password

            // sometimes SharePoint could be using a different username / password.
            string sharePointSsgUsername = Configuration["SHAREPOINT_SSG_USERNAME"];
            string sharePointSsgPassword = Configuration["SHAREPOINT_SSG_PASSWORD"];

            if (string.IsNullOrEmpty(sharePointSsgUsername))
            {
                sharePointSsgUsername = ssgUsername;
            }

            if (string.IsNullOrEmpty(sharePointSsgPassword))
            {
                sharePointSsgPassword = ssgPassword;
            }

            OdataUri = sharePointOdataUri;
            ServerAppIdUri = sharePointServerAppIdUri;
            NativeBaseUri = sharePointNativeBaseURI;
            WebName = sharePointWebname;

            // ensure the webname has a slash.
            if (!string.IsNullOrEmpty(WebName) && WebName[0] != '/')
            {
                WebName = "/" + WebName;
            }

            ApiEndpoint = sharePointOdataUri + "/_api/";
            FedAuthValue = null;

            // Scenario #1 - ADFS (2016) using FedAuth
            if (!string.IsNullOrEmpty(sharePointRelyingPartyIdentifier)
                && !string.IsNullOrEmpty(sharePointUsername)
                && !string.IsNullOrEmpty(sharePointPassword)
                && !string.IsNullOrEmpty(sharePointStsTokenUri)
                )
            {
                Authorization = null;
                var samlST = Authentication.GetStsSamlToken(sharePointRelyingPartyIdentifier, sharePointUsername, sharePointPassword, sharePointStsTokenUri).GetAwaiter().GetResult();
                //FedAuthValue = 
                Authentication.GetFedAuth(sharePointOdataUri, samlST, sharePointRelyingPartyIdentifier, _Client, _CookieContainer).GetAwaiter().GetResult();
            }
            // Scenario #2 - SharePoint Online (Cloud) using a Client Certificate
            else if (!string.IsNullOrEmpty(sharePointAadTenantId)
                && !string.IsNullOrEmpty(sharePointCertFileName)
                && !string.IsNullOrEmpty(sharePointCertPassword)
                && !string.IsNullOrEmpty(sharePointClientId)
                )
            {
                // add authentication.
                var authenticationContext = new AuthenticationContext(
                   "https://login.windows.net/" + sharePointAadTenantId);

                // Create the Client cert.
                X509Certificate2 cert = new X509Certificate2(sharePointCertFileName, sharePointCertPassword);
                ClientAssertionCertificate clientAssertionCertificate = new ClientAssertionCertificate(sharePointClientId, cert);

                //ClientCredential clientCredential = new ClientCredential(clientId, clientKey);
                var task = authenticationContext.AcquireTokenAsync(sharePointServerAppIdUri, clientAssertionCertificate);
                task.Wait();
                authenticationResult = task.Result;
                Authorization = authenticationResult.CreateAuthorizationHeader();
            }
            else
            // Scenario #3 - Using an API Gateway with Basic Authentication.  The API Gateway will handle other authentication and have different credentials, which may be NTLM
            {
                // authenticate using the SSG.                
                string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(sharePointSsgUsername + ":" + sharePointSsgPassword));
                Authorization = "Basic " + credentials;
            }

            // Authorization header is used for Cloud or Basic API Gateway access
            if (!string.IsNullOrEmpty(Authorization))
            {
                _Client.DefaultRequestHeaders.Add("Authorization", Authorization);
            }

            // Add a Digest header.  Needed for certain API operations
            Digest = GetDigest(_Client).GetAwaiter().GetResult();
            if (Digest != null)
            {
                _Client.DefaultRequestHeaders.Add("X-RequestDigest", Digest);
            }

            // Standard headers for API access
            _Client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            _Client.DefaultRequestHeaders.Add("OData-Version", "4.0");


        }
        /// <summary>
        /// Escape the apostrophe character.  Since we use it to enclose the filename it must be escaped.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Filename, with apropstophes escaped.</returns>
        private string EscapeApostrophe (string filename)
        {
            string result = null;
            if (! string.IsNullOrEmpty(filename))
            {
                result = filename.Replace("'", "''");
            }
            return result;
        }

        public class FileSystemItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Documenttype { get; set; }
            public int Size { get; set; }
            public string Serverrelativeurl { get; set; }
            public DateTime Timecreated { get; set; }
            public DateTime Timelastmodified { get; set; }
        }
        

        public class FileDetailsList
        {
            public string Name { get; set; }
            public string TimeLastModified { get; set; }
            public string Length { get; set; }
            public string DocumentType { get; set; }
            public string ServerRelativeUrl { get; set; }
        }

        /// <summary>
        /// Get file details list from SharePoint filtered by folder name and document type
        /// </summary>
        /// <param name="listTitle"></param>
        /// <param name="folderName"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public async Task<List<FileDetailsList>> GetFileDetailsListInFolder(string listTitle, string folderName, string documentType)
        {
            string serverRelativeUrl = GetServerRelativeURL(listTitle, folderName);
            string _responseContent = null;
            HttpRequestMessage _httpRequest =
                            new HttpRequestMessage(HttpMethod.Post, ApiEndpoint + "web/getFolderByServerRelativeUrl('" + EscapeApostrophe(serverRelativeUrl) + "')/files");
            // make the request.
            var _httpResponse = await _Client.SendAsync(_httpRequest);
            HttpStatusCode _statusCode = _httpResponse.StatusCode;

            if ((int)_statusCode != 200)
            {
                var ex = new SharePointRestException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                ex.Request = new HttpRequestMessageWrapper(_httpRequest, null);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);

                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            else
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync();
            }

            // parse the response
            // parse the response
            JObject responseObject = null;
            try
            {
                responseObject = JObject.Parse(_responseContent);
            }
            catch (JsonReaderException jre)
            {
                throw jre;
            }
            // get JSON response objects into a list
            List<JToken> responseResults = responseObject["d"]["results"].Children().ToList();
            // create file details list to add from response
            List<FileDetailsList> fileDetailsList = new List<FileDetailsList>();
            // create .NET objects
            foreach (JToken responseResult in responseResults)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                FileDetailsList searchResult = responseResult.ToObject<FileDetailsList>();
                //filter by parameter documentType
                int fileDoctypeEnd = searchResult.Name.IndexOf("__");
                if (fileDoctypeEnd > -1)
                {
                    string fileDoctype = searchResult.Name.Substring(0, fileDoctypeEnd);
                    if (fileDoctype == documentType)
                    {
                        searchResult.DocumentType = documentType;
                        fileDetailsList.Add(searchResult);
                    }
                }
            }

            return fileDetailsList;
        }

        /// <summary>
        /// Create Folder
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Object> CreateFolder(string listTitle, string folderName)
        {
            HttpRequestMessage endpointRequest =
                new HttpRequestMessage(HttpMethod.Post, ApiEndpoint + "web/folders");


            var folder = CreateNewFolderRequest($"{listTitle}/{folderName}");


            string jsonString = JsonConvert.SerializeObject(folder);
            StringContent strContent = new StringContent(jsonString, Encoding.UTF8);
            strContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
            endpointRequest.Content = strContent;

            // make the request.
            var response = await _Client.SendAsync(endpointRequest);
            HttpStatusCode _statusCode = response.StatusCode;

            if (_statusCode != HttpStatusCode.Created)
            {
                string _responseContent = null;
                var ex = new SharePointRestException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                _responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                ex.Request = new HttpRequestMessageWrapper(endpointRequest, null);
                ex.Response = new HttpResponseMessageWrapper(response, _responseContent);

                endpointRequest.Dispose();
                if (response != null)
                {
                    response.Dispose();
                }
                throw ex;
            }
            else
            {
                jsonString = await response.Content.ReadAsStringAsync();
            }

            return folder;
        }
        /// <summary>
        /// Create Folder
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Object> CreateDocumentLibrary(string listTitle, string documentTemplateUrlTitle = null)
        {
            HttpRequestMessage endpointRequest =
                new HttpRequestMessage(HttpMethod.Post, ApiEndpoint + "web/Lists");

            if(documentTemplateUrlTitle == null)
            {
                documentTemplateUrlTitle = listTitle;
            }
            var library = CreateNewDocumentLibraryRequest(documentTemplateUrlTitle);


            string jsonString = JsonConvert.SerializeObject(library);
            StringContent strContent = new StringContent(jsonString, Encoding.UTF8);
            strContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
            endpointRequest.Content = strContent;

            // make the request.
            var response = await _Client.SendAsync(endpointRequest);
            HttpStatusCode _statusCode = response.StatusCode;

            if (_statusCode != HttpStatusCode.Created)
            {
                string _responseContent = null;
                var ex = new SharePointRestException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                _responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                ex.Request = new HttpRequestMessageWrapper(endpointRequest, null);
                ex.Response = new HttpResponseMessageWrapper(response, _responseContent);

                endpointRequest.Dispose();
                if (response != null)
                {
                    response.Dispose();
                }
                throw ex;
            }
            else
            {
                jsonString = await response.Content.ReadAsStringAsync();
                var ob = Newtonsoft.Json.JsonConvert.DeserializeObject<DocumentLibraryResponse>(jsonString);

                if(listTitle != documentTemplateUrlTitle)
                {
                    // update list title
                    endpointRequest = new HttpRequestMessage(HttpMethod.Post, $"{ApiEndpoint}web/lists(guid'{ob.d.Id}')");
                    var type = new { type = "SP.List" };
                    var request = new
                    {
                        __metadata = type,
                        Title = listTitle
                    };
                    jsonString = JsonConvert.SerializeObject(request);
                    strContent = new StringContent(jsonString, Encoding.UTF8);
                    strContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
                    endpointRequest.Headers.Add("IF-MATCH", "*");
                    endpointRequest.Headers.Add("X-HTTP-Method", "MERGE");
                    endpointRequest.Content = strContent;
                    response = await _Client.SendAsync(endpointRequest);
                    jsonString = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();
                }
            }

            return library;
        }

        public async Task<Object> UpdateDocumentLibrary(string listTitle)
        {
            HttpRequestMessage endpointRequest =
                new HttpRequestMessage(HttpMethod.Put, $"{ApiEndpoint}web/Lists");


            var library = CreateNewDocumentLibraryRequest(listTitle);


            string jsonString = JsonConvert.SerializeObject(library);
            StringContent strContent = new StringContent(jsonString, Encoding.UTF8);
            strContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
            endpointRequest.Content = strContent;

            // make the request.
            var response = await _Client.SendAsync(endpointRequest);
            HttpStatusCode _statusCode = response.StatusCode;

            if (_statusCode != HttpStatusCode.Created)
            {
                string _responseContent = null;
                var ex = new SharePointRestException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                _responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                ex.Request = new HttpRequestMessageWrapper(endpointRequest, null);
                ex.Response = new HttpResponseMessageWrapper(response, _responseContent);

                endpointRequest.Dispose();
                if (response != null)
                {
                    response.Dispose();
                }
                throw ex;
            }
            else
            {
                jsonString = await response.Content.ReadAsStringAsync();
            }

            return library;
        }

        private object CreateNewFolderRequest(string serverRelativeUri)
        {
            var type = new { type = "SP.Folder" };
            var request = new { __metadata = type, ServerRelativeUrl = serverRelativeUri };
            return request;
        }

        private object CreateNewDocumentLibraryRequest(string listName)
        {
            var type = new { type = "SP.List" };
            var request = new { __metadata = type,
                BaseTemplate = 101,
                Title = listName
            };
            return request;
        }


        public async Task<bool> DeleteFolder(string listTitle, string folderName)
        {
            bool result = false;
            // Delete is very similar to a GET.
            string serverRelativeUrl = GetServerRelativeURL(listTitle, folderName);

            HttpRequestMessage endpointRequest =
    new HttpRequestMessage(HttpMethod.Post, ApiEndpoint + "web/getFolderByServerRelativeUrl('" + EscapeApostrophe(serverRelativeUrl) + "')");


            // We want to delete this folder.
            endpointRequest.Headers.Add("IF-MATCH", "*");
            endpointRequest.Headers.Add("X-HTTP-Method", "DELETE");

            // make the request.
            var response = await _Client.SendAsync(endpointRequest);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = true;
            }
            else
            {
                string _responseContent = null;
                var ex = new SharePointRestException(string.Format("Operation returned an invalid status code '{0}'", response.StatusCode));
                _responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                ex.Request = new HttpRequestMessageWrapper(endpointRequest, null);
                ex.Response = new HttpResponseMessageWrapper(response, _responseContent);

                endpointRequest.Dispose();
                if (response != null)
                {
                    response.Dispose();
                }
                throw ex;
            }

            return result;
        }

        public async Task<bool> FolderExists(string listTitle, string folderName)
        {
            Object folder = await GetFolder(listTitle, folderName);

            return (folder != null);
        }

        public async Task<bool> DocumentLibraryExists(string listTitle)
        {
            Object lisbrary = await GetDocumentLibrary(listTitle);

            return (lisbrary != null);
        }

        public async Task<Object> GetFolder(string listTitle, string folderName)
        {
            Object result = null;
            string serverRelativeUrl = GetServerRelativeURL(listTitle, folderName);

            HttpRequestMessage endpointRequest = new HttpRequestMessage(HttpMethod.Post, ApiEndpoint + "web/getFolderByServerRelativeUrl('" + EscapeApostrophe(serverRelativeUrl) + "')");


            // make the request.
            var response = await _Client.SendAsync(endpointRequest);
            string jsonString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {

                result = JsonConvert.DeserializeObject(jsonString);
            }

            return result;
        }

        public async Task<Object> GetDocumentLibrary(string listTitle)
        {
            Object result = null;
            string title = Uri.EscapeUriString(listTitle);
            string query = $"web/lists/GetByTitle('{title}')";

            HttpRequestMessage endpointRequest = new HttpRequestMessage(HttpMethod.Post, ApiEndpoint + query);


            // make the request.
            var response = await _Client.SendAsync(endpointRequest);
            string jsonString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {

                result = JsonConvert.DeserializeObject(jsonString);
            }

            return result;
        }

        public async Task AddFile(String folderName, String fileName, Stream fileData, string contentType)
        {
            await this.AddFile(DefaultDocumentListTitle, folderName, fileName, fileData, contentType);
        }



        public async Task AddFile(String documentLibrary, String folderName, String fileName, Stream fileData, string contentType)
        {

            bool folderExists = await this.FolderExists(documentLibrary, folderName);
            if (!folderExists)
            {
                var folder = await this.CreateFolder(documentLibrary, folderName);
            }

            // now add the file to the folder.

            await this.UploadFile(fileName, documentLibrary, folderName, fileData, contentType);

        }

        public string GetServerRelativeURL(string listTitle, string folderName)
        {
            string serverRelativeUrl = Uri.EscapeUriString(listTitle) + "/" + Uri.EscapeUriString(folderName);
            return serverRelativeUrl;
        }

        /// <summary>
        /// Upload a file
        /// </summary>
        /// <param name="name"></param>
        /// <param name="listTitle"></param>
        /// <param name="folderName"></param>
        /// <param name="fileData"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task<bool> UploadFile(string name, string listTitle, string folderName, Stream fileData, string contentType)
        {
            bool result = false;
            
            // Delete is very similar to a GET.
            string serverRelativeUrl = GetServerRelativeURL(listTitle, folderName);

            HttpRequestMessage endpointRequest =
                new HttpRequestMessage(HttpMethod.Post, ApiEndpoint + "web/getFolderByServerRelativeUrl('" + EscapeApostrophe(serverRelativeUrl) + "')/Files/add(url='"
                + EscapeApostrophe(name) + "',overwrite=true)");
            // convert the stream into a byte array.
            MemoryStream ms = new MemoryStream();
            fileData.CopyTo(ms);
            Byte[] data = ms.ToArray();
            ByteArrayContent byteArrayContent = new ByteArrayContent(data);
            byteArrayContent.Headers.Add(@"content-length", data.Length.ToString());
            endpointRequest.Content = byteArrayContent;

            // make the request.
            var response = await _Client.SendAsync(endpointRequest);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = true;
            }
            else
            {
                string _responseContent = null;
                var ex = new SharePointRestException(string.Format("Operation returned an invalid status code '{0}'", response.StatusCode));
                _responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                ex.Request = new HttpRequestMessageWrapper(endpointRequest, null);
                ex.Response = new HttpResponseMessageWrapper(response, _responseContent);

                endpointRequest.Dispose();
                if (response != null)
                {
                    response.Dispose();
                }
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Download a file
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<byte[]> DownloadFile(string url)
        {
            byte[] result = null;
            var webRequest = System.Net.WebRequest.Create(ApiEndpoint + "web/GetFileByServerRelativeUrl('" + EscapeApostrophe(url) + "')/$value");
            HttpWebRequest request = (HttpWebRequest)webRequest;
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", Authorization);
            request.Accept = "*";

            // we need to add authentication to a HTTP Client to fetch the file.
            using (
                MemoryStream ms = new MemoryStream())
            {
                await request.GetResponse().GetResponseStream().CopyToAsync(ms);
                result = ms.ToArray();
            }

            return result;
        }

        public async Task<string> GetDigest(HttpClient client)
        {
            string result = null;

            HttpRequestMessage endpointRequest = new HttpRequestMessage(HttpMethod.Post, ApiEndpoint + "contextinfo");

            // make the request.
            var response = await client.SendAsync(endpointRequest);
            string jsonString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {

                JToken t = JToken.Parse(jsonString);
                result = t["d"]["GetContextWebInformation"]["FormDigestValue"].ToString();
            }

            return result;
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFile(string listTitle, string folderName, string fileName)
        {
            bool result = false;
            // Delete is very similar to a GET.
            string serverRelativeUrl = $"{WebName}/" + Uri.EscapeUriString(listTitle) + "/" + Uri.EscapeUriString(folderName) + "/" + Uri.EscapeUriString(fileName);

            result = await DeleteFile(serverRelativeUrl);

            return result;
        }

        public async Task<bool> DeleteFile(string serverRelativeUrl)
        {
            bool result = false;
            // Delete is very similar to a GET.

            HttpRequestMessage endpointRequest =
    new HttpRequestMessage(HttpMethod.Post, ApiEndpoint + "web/GetFileByServerRelativeUrl('" + EscapeApostrophe(serverRelativeUrl) + "')");

            // We want to delete this file.
            endpointRequest.Headers.Add("IF-MATCH", "*");
            endpointRequest.Headers.Add("X-HTTP-Method", "DELETE");

            // make the request.
            var response = await _Client.SendAsync(endpointRequest);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = true;
            }
            else
            {
                string _responseContent = null;
                var ex = new SharePointRestException(string.Format("Operation returned an invalid status code '{0}'", response.StatusCode));
                _responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                ex.Request = new HttpRequestMessageWrapper(endpointRequest, null);
                ex.Response = new HttpResponseMessageWrapper(response, _responseContent);

                endpointRequest.Dispose();
                if (response != null)
                {
                    response.Dispose();
                }
                throw ex;
            }

            return result;
        }
    }

   class DocumentLibraryResponse
    {
        public DocumentLibraryResponseContent d { get; set; }
    }

    class DocumentLibraryResponseContent
    {
        public string Id { get; set; }
    }
}
