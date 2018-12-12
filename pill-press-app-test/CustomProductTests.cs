using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gov.Jag.PillPressRegistry.Public.Test
{
    public class CustomProductTests : ApplicationTestBase, IAsyncLifetime
    {
        public CustomProductTests(CustomWebApplicationFactory<Startup> factory)
          : base(factory, service)
        { }

        const string service = "customproduct";
        const string FRENCH_CHARACTERS = "ÀàÂâÆæÈèÉéÊêËëÎîÏïÔôŒœÙùÛûÜüŸÿÇç";
        const int MAX_CHAR_LENGTH_PRODUCT_DESCRIPTION_AND_INTENDED_USE = 1000;

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
            string initialName = "InitialName";

            // Create a custom product with a null incidentId.
            var response = await CreateNewCustomProduct(initialName, null);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseMessage = response.Content.ReadAsStringAsync().Result;
            Assert.Equal("IncidentId missing", responseMessage);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestMaxCharacterLength()
        {
            var incidentGuid = await CreateNewApplicationGuid(await GetAccountForCurrentUser());
            // Create a custom product max number of characters in the product description and intended use string.
            string maxString = TestUtilities.RandomANString(MAX_CHAR_LENGTH_PRODUCT_DESCRIPTION_AND_INTENDED_USE);
            var response = await CreateNewCustomProduct(maxString, incidentGuid.ToString());
            response.EnsureSuccessStatusCode();

            // parse as JSON.
            var jsonString = await response.Content.ReadAsStringAsync();
            ViewModels.CustomProduct responseViewModel = JsonConvert.DeserializeObject<ViewModels.CustomProduct>(jsonString);

            // productdescriptionandintendeduse should match.
            Assert.Equal(maxString, responseViewModel.productdescriptionandintendeduse);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestMaxCharacterLengthExceeded()
        {
            var incidentGuid = await CreateNewApplicationGuid(await GetAccountForCurrentUser());
            // Create a custom product max number of characters in the product description and intended use string.
            string maxStringExceeded = TestUtilities.RandomANString(MAX_CHAR_LENGTH_PRODUCT_DESCRIPTION_AND_INTENDED_USE + 1);
            var response = await CreateNewCustomProduct(maxStringExceeded, incidentGuid.ToString());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            // TODO: assert against returned error message when above assertion is fixed.
        }

        [Fact]
        public async System.Threading.Tasks.Task TestUnicodeSupport()
        {
            var incidentGuid = await CreateNewApplicationGuid(await GetAccountForCurrentUser());
            // Create a custom product with French Unicode characters in the product description and intended use string.
            var response = await CreateNewCustomProduct(FRENCH_CHARACTERS, incidentGuid.ToString());
            response.EnsureSuccessStatusCode();

            // parse as JSON.
            var jsonString = await response.Content.ReadAsStringAsync();
            ViewModels.CustomProduct responseViewModel = JsonConvert.DeserializeObject<ViewModels.CustomProduct>(jsonString);

            // productdescriptionandintendeduse should match.
            Assert.Equal(FRENCH_CHARACTERS, responseViewModel.productdescriptionandintendeduse);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestCRUD()
        {
            string initialName = "InitialName";
            string changedName = "ChangedName";

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service);
            var incidentId = await CreateNewApplicationGuid(await GetAccountForCurrentUser());

            ViewModels.CustomProduct viewmodel_customproduct = new ViewModels.CustomProduct()
            {
                productdescriptionandintendeduse = initialName,
                incidentId = incidentId.ToString()
            };

            string jsonString = JsonConvert.SerializeObject(viewmodel_customproduct);

            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // parse as JSON.
            jsonString = await response.Content.ReadAsStringAsync();
            ViewModels.CustomProduct responseViewModel = JsonConvert.DeserializeObject<ViewModels.CustomProduct>(jsonString);

            // name should match.
            Assert.Equal(initialName, responseViewModel.productdescriptionandintendeduse);
            Guid id = new Guid(responseViewModel.id);

            // R - Read

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.CustomProduct>(jsonString);
            Assert.Equal(initialName, responseViewModel.productdescriptionandintendeduse);

            // U - Update            
            ViewModels.CustomProduct patchModel = new ViewModels.CustomProduct()
            {
                productdescriptionandintendeduse = changedName
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

            responseViewModel = JsonConvert.DeserializeObject<ViewModels.CustomProduct>(jsonString);
            Assert.Equal(changedName, responseViewModel.productdescriptionandintendeduse);

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
        /// Create a new custom product, using the passed parameters to create the custom product view model.
        /// </summary>
        /// <param name="productDescriptionAndIntendedUse"></param>
        /// <param name="incidentId"></param>
        /// <returns>The http response of the creation request.</returns>
        private async Task<HttpResponseMessage> CreateNewCustomProduct(String productDescriptionAndIntendedUse, String incidentId)
        {
            ViewModels.CustomProduct viewmodel_customproduct = new ViewModels.CustomProduct()
            {
                productdescriptionandintendeduse = productDescriptionAndIntendedUse,
                incidentId = incidentId
            };
            string jsonString = JsonConvert.SerializeObject(viewmodel_customproduct);

            return await CreateNewTypeWithContent(jsonString);
        }
    }
}
