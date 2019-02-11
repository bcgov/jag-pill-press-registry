using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gov.Jag.PillPressRegistry.Public.Test
{
    public class EquipmentTests : ApplicationTestBase, IAsyncLifetime
    {
        public EquipmentTests(CustomWebApplicationFactory<Startup> factory)
          : base(factory, service)
        { }

        const string service = "equipment";

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
