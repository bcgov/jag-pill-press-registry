using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gov.Jag.PillPressRegistry.Public.Test
{
    public class EquipmentNotificationTests : ApplicationTestBase, IAsyncLifetime
    {
        public EquipmentNotificationTests(CustomWebApplicationFactory<Startup> factory)
          : base(factory, service)
        { }

        const string service = "equipmentnotification";

        [Fact]
        public async System.Threading.Tasks.Task TestNoAccessToAnonymousUser()
        {
            string id = "SomeRandomId";
            await Logout();

            // first confirm we are not logged in
            await GetCurrentUserIsUnauthorized();

            // try a random GET, should return unauthorized
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            string _discard = await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async System.Threading.Tasks.Task TestNullIncidentId()
        {
            // Create an equipment notification with a null incidentId.
            var response = await CreateNewEquipmentNotification(null);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseMessage = response.Content.ReadAsStringAsync().Result;
            Assert.Equal("IncidentId missing", responseMessage);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestCRUD()
        {
            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service);
            var incidentId = await CreateNewApplicationGuid(await GetAccountForCurrentUser());

            ViewModels.Equipment viewmodel_customproduct = new ViewModels.Equipment()
            {
                incidentId = incidentId.ToString()
            };

            string jsonString = JsonConvert.SerializeObject(viewmodel_customproduct);

            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // parse as JSON.
            jsonString = await response.Content.ReadAsStringAsync();
            ViewModels.Equipment responseViewModel = JsonConvert.DeserializeObject<ViewModels.Equipment>(jsonString);

            // name should match.
            Assert.Equal(incidentId.ToString(), responseViewModel.incidentId);
            Guid id = new Guid(responseViewModel.Id);

            // R - Read

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Equipment>(jsonString);
            //TODO: Assert.Equal(changedName, responseViewModel.someimportantequipmentnotificationfield);

            // U - Update            
            ViewModels.CustomProduct patchModel = new ViewModels.CustomProduct()
            {
                //TODO: fill in update fields.
            };

            request = new HttpRequestMessage(HttpMethod.Put, "/api/" + service + "/" + id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(patchModel), Encoding.UTF8, "application/json")
            };
            response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // verify that the update persisted.

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();

            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Equipment>(jsonString);
            //TODO: Assert.Equal(changedName, responseViewModel.someimportantequipmentnotificationfield);

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
        }

        /// <summary>
        /// Create a new equipment notification, using the passed parameters to create the equipment notification view model.
        /// </summary>
        /// <param name="incidentId"></param>
        /// <returns>The http response of the creation request.</returns>
        private async Task<HttpResponseMessage> CreateNewEquipmentNotification(String incidentId)
        {
            ViewModels.CustomProduct viewmodel_equipmentnotification = new ViewModels.CustomProduct()
            {
                incidentId = incidentId
            };
            string jsonString = JsonConvert.SerializeObject(viewmodel_equipmentnotification);

            return await CreateNewTypeWithContent(jsonString);
        }
    }
}
