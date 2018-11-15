﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Gov.Jag.PillPressRegistry.Public.Test
{
    public class CurrentUserTests : ApiIntegrationTestBaseWithLogin
    {
        public CurrentUserTests(CustomWebApplicationFactory<Startup> factory)
          : base(factory)
        { }

        [Fact]
        public async System.Threading.Tasks.Task DefaultDevelopmentUserIsValid()
        {
            var loginUser = randomNewUserName("NewLoginUser", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser);

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/user/current");
            var response = await _client.SendAsync(request);
            string jsonString = await response.Content.ReadAsStringAsync();
			response.EnsureSuccessStatusCode();

            ViewModels.User user = JsonConvert.DeserializeObject<ViewModels.User>(jsonString);

            // The Default development user should not be a new user.
            Assert.False(user.isNewUser);
			Assert.NotNull(user.accountid);
			Assert.NotEmpty(user.accountid);

			ViewModels.Account account = await GetAccountForCurrentUser();
			Assert.NotNull(account);

            await LogoutAndCleanupTestUser(strId);
        }
        
		[Fact]
        public async System.Threading.Tasks.Task NewRegistrationUserIsValid()
        {
			var loginUser = randomNewUserName("TestUser", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser);

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/user/current");
            var response = await _client.SendAsync(request);
			string jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            ViewModels.User user = JsonConvert.DeserializeObject<ViewModels.User>(jsonString);

            // The Default development user should not be a new user.
            Assert.False(user.isNewUser);
			Assert.NotNull(user.accountid);
            Assert.NotEmpty(user.accountid);

            ViewModels.Account account = await GetAccountForCurrentUser();
            Assert.NotNull(account);
			var prevId = account.id;

            // logout and then login again, and verify we can still get our account
			await Logout();
			await Login(loginUser);

			request = new HttpRequestMessage(HttpMethod.Get, "/api/user/current");
            response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            user = JsonConvert.DeserializeObject<ViewModels.User>(jsonString);

            Assert.NotNull(user.accountid);
            Assert.NotEmpty(user.accountid);

			account = await GetAccountForCurrentUser();
            Assert.NotNull(account);

			Assert.Equal(account.id, prevId);

			await LogoutAndCleanupTestUser(strId);
        }

		[Fact]
		public async System.Threading.Tasks.Task SwitchBetweenLoggedInUsersWorks()
		{
            // register and login as our first user
			var loginUser1 = randomNewUserName("TestUser-1-", 6);
            var strId1 = await LoginAndRegisterAsNewUser(loginUser1);

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/user/current");
            var response = await _client.SendAsync(request);
            string jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            ViewModels.User user1 = JsonConvert.DeserializeObject<ViewModels.User>(jsonString);

            // The Default development user should not be a new user.
            Assert.False(user1.isNewUser);
            Assert.NotNull(user1.accountid);
            Assert.NotEmpty(user1.accountid);

            ViewModels.Account account1 = await GetAccountForCurrentUser();
            Assert.NotNull(account1);
            var prevId1 = account1.id;

            // call the REST API to get the account
			request = new HttpRequestMessage(HttpMethod.Get, "/api/account/" + prevId1);
            response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
			response.EnsureSuccessStatusCode();
			ViewModels.Account retAccount1 = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
			Assert.Equal(user1.accountid, retAccount1.id);

            // logout
            await Logout();

			// register and login as our second user
			var loginUser2 = randomNewUserName("TestUser-2-", 6);
            var strId2 = await LoginAndRegisterAsNewUser(loginUser2);

            request = new HttpRequestMessage(HttpMethod.Get, "/api/user/current");
            response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            ViewModels.User user2 = JsonConvert.DeserializeObject<ViewModels.User>(jsonString);

            // The Default development user should not be a new user.
            Assert.False(user2.isNewUser);
            Assert.NotNull(user2.accountid);
            Assert.NotEmpty(user2.accountid);

            ViewModels.Account account2 = await GetAccountForCurrentUser();
            Assert.NotNull(account2);

            // as our second user, view the account of the first user
			request = new HttpRequestMessage(HttpMethod.Get, "/api/account/" + prevId1);
            response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // logout
            await Logout();

            // cleanup both users
			await Login(loginUser1);
			await LogoutAndCleanupTestUser(strId1);
			await Login(loginUser2);
            await LogoutAndCleanupTestUser(strId2);
		}
        
    }
}
