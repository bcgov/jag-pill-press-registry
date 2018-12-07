using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gov.Jag.PillPressRegistry.Public.Test
{
    public class ApplicationTestBase : ApiIntegrationTestBaseWithLogin, IAsyncLifetime
    {
        private string service;
        public ApplicationTestBase(CustomWebApplicationFactory<Startup> factory, string service)
          : base(factory)
        {
            this.service = service;
        }

        /// <summary>
        /// Log in a random user. This is required by most tests. Tests that do not require a user should call Logout().
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            var loginUser = randomNewUserName("NewLoginUser", 6);
            await LoginAndRegisterAsNewUser(loginUser);
        }

        public async Task DisposeAsync()
        {
            try
            {
                ViewModels.Account currentAccount = await GetAccountForCurrentUser();
                await LogoutAndCleanupTestUser(currentAccount.id);
            }
            catch (HttpRequestException requestException)
            {
                // Ignore any failures to clean up the test user.
                Console.WriteLine(requestException.ToString());
            }
        }

        /// <summary>
        /// Create a new Application for testing, using the passed account.
        /// </summary>
        /// <param name="currentAccount">Non-null account to use when creating the Application</param>
        /// <returns>The GUID of the created Application</returns>
        protected async Task<Guid> CreateNewApplicationGuid(ViewModels.Account currentAccount)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Application/Waiver");

            ViewModels.Application viewmodel_application = SecurityHelper.CreateNewApplication(currentAccount);

            var jsonString = JsonConvert.SerializeObject(viewmodel_application);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // parse as JSON.
            jsonString = await response.Content.ReadAsStringAsync();
            ViewModels.Application responseViewModel = JsonConvert.DeserializeObject<ViewModels.Application>(jsonString);

            return new Guid(responseViewModel.id);
        }

        protected async Task<HttpResponseMessage> CreateNewTypeWithContent(string jsonString)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service);

            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return await _client.SendAsync(request);
        }
    }
}
