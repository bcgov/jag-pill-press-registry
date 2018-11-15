using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gov.Jag.PillPressRegistry.Public.Test
{
    public class SecurityHelper
	{
		public static async Task<ViewModels.Account> GetAccountRecord(HttpClient _client, string id, bool expectSuccess)
		{
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/account/" + id);
            var response = await _client.SendAsync(request);
			if (expectSuccess)
			{
				response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                ViewModels.Account responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
				return responseViewModel;
			}
			else
			{
				Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
				var _discard = await response.Content.ReadAsStringAsync();
				return null;
			}
		}

		public static async Task<ViewModels.Account> UpdateAccountRecord(HttpClient _client, string id, ViewModels.Account account, bool expectSuccess)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/account/" + id)
			{
                Content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(request);
            if (expectSuccess)
            {
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                ViewModels.Account responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
                return responseViewModel;
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                var _discard = await response.Content.ReadAsStringAsync();
                return null;
            }
        }

		public static async Task<List<ViewModels.FileSystemItem>> GetFileListForAccount(HttpClient _client, string id, string docType, bool expectSuccess)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, "/api/adoxiolegalentity/" + id + "/attachments/" + docType);
			var response = await _client.SendAsync(request);
            var jsonString = await response.Content.ReadAsStringAsync();
			if (expectSuccess)
			{
				response.EnsureSuccessStatusCode();
				List<ViewModels.FileSystemItem> files = JsonConvert.DeserializeObject<List<ViewModels.FileSystemItem>>(jsonString);
				return files;
			}
			else
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                return null;
            }
		}

        public static string DownloadFileForAccount(HttpClient _client, string id, string fileId, bool expectSuccess)
        {
            /*
			var request = new HttpRequestMessage(HttpMethod.Get, "/api/adoxiolegalentity/" + id + "/attachment/" + fileId);
			var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            if (expectSuccess)
            {
                response.EnsureSuccessStatusCode();
				return responseString;
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                return null;
            }
            */
            return null;
        }

        public static string DeleteFileForAccount(HttpClient _client, string id)
        {
            /*
			var request = new HttpRequestMessage(HttpMethod.Delete, "/api/adoxiolegalentity/" + id + "/attachments");
			var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
			*/
            return null;
        }

        public static ViewModels.Application CreateNewApplication(ViewModels.Account currentAccount)
        {
            ViewModels.Application viewmodel_application = new ViewModels.Application()
            {
                applicant = currentAccount, //account
                mainbusinessfocus = "Testing",
                manufacturingprocessdescription = "Automated Testing"
            };
            return viewmodel_application;
        }

        public static async Task<ViewModels.Application> CreateApplication(HttpClient _client, ViewModels.Account currentAccount)
		{
			var request = new HttpRequestMessage(HttpMethod.Post, "/api/application");


            ViewModels.Application viewmodel_application = CreateNewApplication(currentAccount);

            var jsonString = JsonConvert.SerializeObject(viewmodel_application);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // parse as JSON.
            ViewModels.Application responseViewModel = JsonConvert.DeserializeObject<ViewModels.Application>(jsonString);

            Assert.Equal("Testing", responseViewModel.mainbusinessfocus);
            Assert.Equal("Automated Testing", responseViewModel.manufacturingprocessdescription);           

			return responseViewModel;
		}

		public static async Task<ViewModels.Application> GetApplication(HttpClient _client, string applicationId, bool expectSuccess)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, "/api/application/" + applicationId);
            var response = await _client.SendAsync(request);
			if (expectSuccess)
            {
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
				ViewModels.Application responseViewModel = JsonConvert.DeserializeObject<ViewModels.Application>(jsonString);
                return responseViewModel;
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                var _discard = await response.Content.ReadAsStringAsync();
                return null;
            }
		}
		public static async Task<string> DeleteApplication(HttpClient _client, string applicationId)
		{
			var request = new HttpRequestMessage(HttpMethod.Post, "/api/Application/" + applicationId + "/delete");
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
			return applicationId;
		}
        
		public static async Task<string> UploadFileToApplication(HttpClient _client, string id, string docType)
        {
            // Attach a file
            string testData = "This is just a test.";
            byte[] bytes = Encoding.ASCII.GetBytes(testData);
            string documentType = docType;

            // Create random filename
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[9];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var randomString = new String(stringChars);
            string filename = randomString + ".txt";

            MultipartFormDataContent multiPartContent = new MultipartFormDataContent("----TestBoundary");
            var fileContent = new MultipartContent { new ByteArrayContent(bytes) };
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            fileContent.Headers.ContentDisposition.Name = "File";
            fileContent.Headers.ContentDisposition.FileName = filename;
            multiPartContent.Add(fileContent);
            multiPartContent.Add(new StringContent(documentType), "documentType");   // form input

            // create a new request object for the upload, as we will be using multipart form submission.
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/Application/" + id + "/attachments");
            requestMessage.Content = multiPartContent;

            var uploadResponse = await _client.SendAsync(requestMessage);
            uploadResponse.EnsureSuccessStatusCode();

            return filename;
        }

        public static async Task<List<ViewModels.FileSystemItem>> GetFileListForApplication(HttpClient _client, string id, string docType, bool expectSuccess)
        {
			var request = new HttpRequestMessage(HttpMethod.Get, "/api/Application/" + id + "/attachments/" + docType);
            var response = await _client.SendAsync(request);
            var jsonString = await response.Content.ReadAsStringAsync();
            if (expectSuccess)
            {
                response.EnsureSuccessStatusCode();
                List<ViewModels.FileSystemItem> files = JsonConvert.DeserializeObject<List<ViewModels.FileSystemItem>>(jsonString);
                return files;
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                return null;
            }
        }

        public static string DownloadFileForApplication(HttpClient _client, string id, string fileId, bool expectSuccess)
        {
            /*
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Application/" + id + "/attachment/" + fileId);
            var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            if (expectSuccess)
            {
                response.EnsureSuccessStatusCode();
                return responseString;
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                return null;
            }
            */
            return null;
        }

        public static string DeleteFileForApplication(HttpClient _client, string id)
        {
            /*
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/Application/" + id + "/attachments");
            var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            */
            return null;
        }

    }
}
