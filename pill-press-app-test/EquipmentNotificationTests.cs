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
    public class EquipmentNotificationTests : ApiIntegrationTestBaseWithLogin
    {
        public EquipmentNotificationTests(CustomWebApplicationFactory<Startup> factory)
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
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/Equipment Notification");

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

        
    }
}
