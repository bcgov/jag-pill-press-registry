using Gov.Jag.PillPressRegistry.Public.ViewModels;
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
    public class ApplicationTests : ApiIntegrationTestBaseWithLogin
    {
        public ApplicationTests(CustomWebApplicationFactory<Startup> factory)
          : base(factory)
        { }

        [Fact]
        public async System.Threading.Tasks.Task TestNoAccessToAnonymousUser()
        {
            string service = "Application";
            string id = "SomeRandomId";

            // first confirm we are not logged in
            await GetCurrentUserIsUnauthorized();

            // try a random GET, should return unauthorized
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            string _discard = await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async System.Threading.Tasks.Task TestCRUD()
        {
            string initialName = randomNewUserName("Application Initial Name ", 6);
            string changedName = randomNewUserName("Application Changed Name ", 6);
            string service = "Application";

            // login as default and get account for current user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser1);

            User user = await GetCurrentUser();
            Account currentAccount = await GetAccountForCurrentUser();

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/Waiver");

            Application viewmodel_application = SecurityHelper.CreateNewApplication(currentAccount);

            var jsonString = JsonConvert.SerializeObject(viewmodel_application);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // parse as JSON.
            jsonString = await response.Content.ReadAsStringAsync();
            Application responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);

            Assert.Equal("Testing", responseViewModel.mainbusinessfocus);
            Assert.Equal("Automated Testing", responseViewModel.manufacturingprocessdescription);


            Guid id = new Guid(responseViewModel.id);
            //return;
            // R - Read
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);

            Assert.Equal("Testing", responseViewModel.mainbusinessfocus);
            Assert.Equal("Automated Testing", responseViewModel.manufacturingprocessdescription);


            Assert.True(responseViewModel.applicant != null);
            Assert.Equal(currentAccount.id, responseViewModel.applicant.id);


            // U - Update  
            viewmodel_application = new Application();
            viewmodel_application.mainbusinessfocus = changedName;
            

            request = new HttpRequestMessage(HttpMethod.Put, "/api/" + service + "/" + id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(viewmodel_application), Encoding.UTF8, "application/json")
            };
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // verify that the update persisted.

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();

            responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);
            Assert.Equal(changedName, responseViewModel.mainbusinessfocus);

            // D - Delete

            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // second delete should return a 404.
            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // should get a 404 if we try a get now.
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // logout and cleanup (deletes the account and contact created above ^^^)
            await LogoutAndCleanupTestUser(strId);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestBusinessContactNewContact()
        {
            // login as default and get account for current user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser1);

            Application responseViewModel = await CreateDefaultApplication();

            // U - Update  
            responseViewModel.BusinessContacts = new List<BusinessContact>() { new ViewModels.BusinessContact()
            {
                account = await GetAccountForCurrentUser(),
                contact = new Contact(),
                id = null,
                contactType = ContactTypeCodes.Primary,
                jobTitle = "Test Job"
            }};
            await UpdateApplication(responseViewModel);

            // verify that the update persisted.
            Application persistedApplication = await GetApplicationById(responseViewModel.id);
            
            Assert.NotNull(persistedApplication.BusinessContacts);
            Assert.Equal(responseViewModel.BusinessContacts.Count, persistedApplication.BusinessContacts.Count);
            responseViewModel.BusinessContacts[0].id = persistedApplication.BusinessContacts[0].id;
            responseViewModel.BusinessContacts[0].contact = persistedApplication.BusinessContacts[0].contact;

            AssertBusinessContacts(responseViewModel.BusinessContacts, persistedApplication.BusinessContacts);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestBusinessContactCreate()
        {
            // login as default and get account for current user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser1);

            User user = await GetCurrentUser();
            Account currentAccount = await GetAccountForCurrentUser();

            Application responseViewModel = await CreateDefaultApplication();

            responseViewModel.BusinessContacts = new List<BusinessContact>() { new ViewModels.BusinessContact()
            {
                account = await GetAccountForCurrentUser(),
                contact = await GetContactForCurrentUser(),
                id = responseViewModel.id,
                contactType = ContactTypeCodes.Primary,
                jobTitle = "Test Job"
            }};

            await UpdateApplication(responseViewModel);

            // verify that the update persisted.
            Application persistedApplication = await GetApplicationById(responseViewModel.id);

            AssertBusinessContacts(responseViewModel.BusinessContacts, persistedApplication.BusinessContacts);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestBusinessContactUpdate()
        {
            // login as default and get account for current user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser1);

            User user = await GetCurrentUser();
            Account currentAccount = await GetAccountForCurrentUser();

            Application responseViewModel = await CreateDefaultApplication();

            responseViewModel.BusinessContacts = new List<BusinessContact>() { new ViewModels.BusinessContact()
            {
                account = await GetAccountForCurrentUser(),
                contact = await GetContactForCurrentUser(),                
                contactType = ContactTypeCodes.Primary,
                jobTitle = "Test Job"
            }};

            await UpdateApplication(responseViewModel);

            responseViewModel = await GetApplicationById(responseViewModel.id);

            // do a change

            responseViewModel.BusinessContacts[0].jobTitle = "Test Job - Updated";
            

            await UpdateApplication(responseViewModel);

            // verify that the update persisted.
            Application persistedApplication = await GetApplicationById(responseViewModel.id);

            AssertBusinessContacts(responseViewModel.BusinessContacts, persistedApplication.BusinessContacts);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestBusinessContactAddRemove()
        {
            string service = "Application";
            string initialName = randomNewUserName("Application Initial Name ", 6);

            // login as default and get account for current user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser1);

            User user = await GetCurrentUser();
            Account currentAccount = await GetAccountForCurrentUser();

            Application responseViewModel = await CreateDefaultApplication();

            // U - Update  
            responseViewModel.BusinessContacts = new List<BusinessContact>() { new ViewModels.BusinessContact()
            {
                account = await GetAccountForCurrentUser(),
                contact = new Contact(),               
                contactType = ContactTypeCodes.Primary,
                jobTitle = "Test Job"
            }};

            await UpdateApplication(responseViewModel);

            responseViewModel.BusinessContacts = new List<BusinessContact>() { new ViewModels.BusinessContact()
            {
                account = await GetAccountForCurrentUser(),
                contact = new Contact(),
                id = null,
                contactType = ContactTypeCodes.Additional,
                jobTitle = "Updated Test Job"
            }};

            await UpdateApplication(responseViewModel);
            // verify that the update persisted.

            Application persistedApplication = await GetApplicationById(responseViewModel.id);

            Assert.Equal(persistedApplication.BusinessContacts[0].jobTitle, "Updated Test Job");

            // Cleanup
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + responseViewModel.id + "/delete");
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // should get a 404 if we try a get now.
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + responseViewModel.id);
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await LogoutAndCleanupTestUser(strId);

        }

        [Fact]
        public async System.Threading.Tasks.Task TestFileListing()
        {
            string initialName = randomNewUserName("First InitialName", 6);
            string changedName = randomNewUserName("First ChangedName", 6);
            string service = "Application";

            // Login as default user

            var loginUser = randomNewUserName("NewLoginUser", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser);

            User user = await GetCurrentUser();
            Account currentAccount = await GetAccountForCurrentUser();

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/Waiver");

            Application viewmodel_application = SecurityHelper.CreateNewApplication(currentAccount);

            var jsonString = JsonConvert.SerializeObject(viewmodel_application);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // parse as JSON.

            Application responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);

            //Assert.Equal("Applying Person", responseViewModel.applyingPerson);
            Assert.Equal("Testing", responseViewModel.mainbusinessfocus);
            Assert.Equal("Automated Testing", responseViewModel.manufacturingprocessdescription);

            Guid id = new Guid(responseViewModel.id);

            // Attach a file

            string testData = "This is just a test.";
            byte[] bytes = Encoding.ASCII.GetBytes(testData);
            string documentType = "Test Document Type";
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

            string accountId = user.accountid;

            // create a new request object for the upload, as we will be using multipart form submission.
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"/api/file/{ id }/attachments/application");
            requestMessage.Content = multiPartContent;

            var uploadResponse = await _client.SendAsync(requestMessage);
            uploadResponse.EnsureSuccessStatusCode();

            // Cleanup
            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // should get a 404 if we try a get now.
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await LogoutAndCleanupTestUser(strId);
        }

        

        [Fact]
        public async System.Threading.Tasks.Task TestUserCanAccessApplicationForTheirAccount()
        {
            string initialName = randomNewUserName("Application Shared ", 6);
            string service = "Application";

            // login as default and get account for current user
            string loginUser1 = randomNewUserName("TestAppUser", 6);
            string loginAccount = randomNewUserName(loginUser1, 6);
            string loginUser2 = loginUser1 + "-2";
            loginUser1 = loginUser1 + "-1";
            var strId1 = await LoginAndRegisterAsNewUser(loginUser1, loginAccount);

            User user1 = await GetCurrentUser();
            Account currentAccount1 = await GetAccountForCurrentUser();

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/Waiver");

            Application viewmodel_application = SecurityHelper.CreateNewApplication(currentAccount1);

            var jsonString = JsonConvert.SerializeObject(viewmodel_application);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // parse as JSON.
            jsonString = await response.Content.ReadAsStringAsync();
            Application responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);

            // name should match.
            Assert.Equal("Testing", responseViewModel.mainbusinessfocus);
            Assert.Equal("Automated Testing", responseViewModel.manufacturingprocessdescription);

            Guid id = new Guid(responseViewModel.id);

            // R - Read
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);
            Assert.Equal(currentAccount1.id, responseViewModel.applicant.id);

            await Logout();

            // register and login as a second user
            var strId2 = await LoginAndRegisterAsNewUser(loginUser2, loginAccount);

            User user2 = await GetCurrentUser();
            Account currentAccount2 = await GetAccountForCurrentUser();
            // same account as user 1
            Assert.Equal(currentAccount2.id, currentAccount1.id);

            // R - Read (should be able to access by user)
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // logout and cleanup (deletes the account and contact created above ^^^)
            await LogoutAndCleanupTestUser(strId2);

            // log back in as first user
            await Login(loginUser1);

            // R - Read - still has access to application
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);
            Assert.Equal(currentAccount1.id, responseViewModel.applicant.id);

            // D - Delete
            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // should get a 404 if we try a get now.
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // logout and cleanup (deletes the account and contact created above ^^^)
            await LogoutAndCleanupTestUser(strId1);
            //await Logout();
        }

        [Fact]
        public async System.Threading.Tasks.Task TestUploadFile()
        {
            // Create application
            string initialName = randomNewUserName("Application Initial Name ", 6);
            string changedName = randomNewUserName("Application Changed Name ", 6);
            string service = "Application";

            // login as default and get account for current user
            string loginUser = randomNewUserName("TestAppUser_", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser);

            User user = await GetCurrentUser();
            Account currentAccount = await GetAccountForCurrentUser();

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/Waiver");

            

            Application viewmodel_application = new Application()
            {
                applicant = currentAccount, //account
                mainbusinessfocus = "Testing",
                manufacturingprocessdescription = "Automated Testing"
            };

            var jsonString = JsonConvert.SerializeObject(viewmodel_application);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            Application responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);

            Assert.Equal("Testing", responseViewModel.mainbusinessfocus);
            Assert.Equal("Automated Testing", responseViewModel.manufacturingprocessdescription);

            Guid id = new Guid(responseViewModel.id);

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);

            Assert.Equal("Testing", responseViewModel.mainbusinessfocus);
            Assert.Equal("Automated Testing", responseViewModel.manufacturingprocessdescription);


            Assert.True(responseViewModel.applicant != null);
            Assert.Equal(currentAccount.id, responseViewModel.applicant.id);

            /*  file tests disabled as there is no SharePoint yet

            // Test upload, get, delete attachment
            string documentType = "Licence Application Main";


            using (var formData = new MultipartFormDataContent())
            {
                // Upload
                var fileContent = new ByteArrayContent(new byte[100]);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "file",
                    FileName = "test.pdf"
                };
                formData.Add(fileContent);
                formData.Add(new StringContent(documentType, Encoding.UTF8, "application/json"), "documentType");
                response = _client.PostAsync($"/api/file/{id}/attachments/application", formData).Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // Get
                request = new HttpRequestMessage(HttpMethod.Get, $"/api/file/{id}/attachments/application/{documentType}");
                response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                jsonString = await response.Content.ReadAsStringAsync();
                var files = JsonConvert.DeserializeObject<List<FileSystemItem>>(jsonString);
                files.ForEach(async file =>
                {
                    // Delete
                    request = new HttpRequestMessage(HttpMethod.Delete, $"/api/file/{id}/attachments/application?serverRelativeUrl={Uri.EscapeDataString(file.serverrelativeurl)}&documentType={documentType}");
                    response = await _client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                });
            }

        */

            await LogoutAndCleanupTestUser(strId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test25MbUploadFile()
        {
            // Create application
            string initialName = randomNewUserName("Application Initial Name ", 6);
            string changedName = randomNewUserName("Application Changed Name ", 6);
            string service = "Application";

            // login as default and get account for current user
            string loginUser = randomNewUserName("TestAppUser_", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser);

            User user = await GetCurrentUser();
            Account currentAccount = await GetAccountForCurrentUser();

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/Waiver");

            Application viewmodel_application = SecurityHelper.CreateNewApplication(currentAccount);
            

            var jsonString = JsonConvert.SerializeObject(viewmodel_application);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            Application responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);

            Assert.Equal("Testing", responseViewModel.mainbusinessfocus);
            Assert.Equal("Automated Testing", responseViewModel.manufacturingprocessdescription);

            Guid id = new Guid(responseViewModel.id);

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);
            Assert.Equal("Not a Dispensary", responseViewModel.mainbusinessfocus);
            Assert.True(responseViewModel.applicant != null);
            Assert.Equal(currentAccount.id, responseViewModel.applicant.id);

            // Test upload, get, delete attachment
            string documentType = "Licence Application Main";


            using (var formData = new MultipartFormDataContent())
            {
                // Upload
                var fileContent = new ByteArrayContent(new byte[25 * 1024 * 1024]);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "file",
                    FileName = "test.pdf"
                };
                formData.Add(fileContent);
                formData.Add(new StringContent(documentType, Encoding.UTF8, "application/json"), "documentType");
                response = _client.PostAsync($"/api/file/{id}/attachments/application", formData).Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            // Get
            request = new HttpRequestMessage(HttpMethod.Get, $"/api/file/{id}/attachments/application/{documentType}");
            response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            var files = JsonConvert.DeserializeObject<List<FileSystemItem>>(jsonString);
            files.ForEach(async file =>
            {
                // Delete
                request = new HttpRequestMessage(HttpMethod.Delete, $"/api/file/{id}/attachments/application?serverRelativeUrl={Uri.EscapeDataString(file.serverrelativeurl)}&documentType={documentType}");
                response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            });

            await LogoutAndCleanupTestUser(strId);
        }

        private async Task<Application> GetApplicationById(string id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/Application/" + id);
            HttpResponseMessage response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string jsonString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Application>(jsonString);
        }

        private async Task<Application> CreateDefaultApplication()
        {
            User user = await GetCurrentUser();
            Account currentAccount = await GetAccountForCurrentUser();

            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/Application/AuthorizedOwner");

            Application viewmodel_application = SecurityHelper.CreateNewApplication(currentAccount);

            var jsonString = JsonConvert.SerializeObject(viewmodel_application);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // parse as JSON.
            jsonString = await response.Content.ReadAsStringAsync();
            Application responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);

            Assert.Equal("Testing", responseViewModel.mainbusinessfocus);
            Assert.Equal("Automated Testing", responseViewModel.manufacturingprocessdescription);
            return responseViewModel;
        }

        private async Task<Application> UpdateApplication(Application applicationToUpdate)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "/api/Application/" + applicationToUpdate.id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(applicationToUpdate), Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Application>(jsonString);
        }

        private void AssertBusinessContacts(List<BusinessContact> expectedBusinessContacts, List<BusinessContact> actualBusinessContacts)
        {
            Assert.Equal(expectedBusinessContacts.Count, actualBusinessContacts.Count);
            expectedBusinessContacts.ForEach(expectedBusinessContact =>
            {
                var actualBusinessContact = actualBusinessContacts[expectedBusinessContacts.IndexOf(expectedBusinessContact)];
                Assert.Equal(expectedBusinessContact.id, actualBusinessContact.id);
                Assert.Equal(expectedBusinessContact.account.id, actualBusinessContact.account.id);
                Assert.Equal(expectedBusinessContact.contact.id, actualBusinessContact.contact.id);
                Assert.Equal(expectedBusinessContact.jobTitle, actualBusinessContact.jobTitle);
                Assert.Equal(expectedBusinessContact.contactType.ToString(), actualBusinessContact.contactType.ToString());
            });
        }
    }
}
