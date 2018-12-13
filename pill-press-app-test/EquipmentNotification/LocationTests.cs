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
    public partial class EquipmentNotificationTests : ApiIntegrationTestBaseWithLogin
    {

        [Fact]
        public async System.Threading.Tasks.Task TestLocation()
        {
            string initialName = randomNewUserName("Application Initial Name ", 6);
            string changedName = randomNewUserName("Application Changed Name ", 6);
            string service = "Application";

            string initialPhoneNumber = "3331112222";

            DateTimeOffset dto = DateTimeOffset.Now;

            // login as default and get account for current user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser1);

            User user = await GetCurrentUser();
            Account currentAccount = await GetAccountForCurrentUser();

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/Equipment Notification");

            Application viewmodel_application = SecurityHelper.CreateNewApplication(currentAccount);

            string[] stringsToMatch = new string[65];
            for (int i = 0; i < stringsToMatch.Length; i++)
            {
                stringsToMatch[i] = RandomTextString(20, i);
            }

            viewmodel_application.EquipmentLocation = new Location()
            {
                Address = new CustomAddress()
                {
                    StreetLine1 = stringsToMatch[1],
                    StreetLine2 = stringsToMatch[2],
                    StreetLine3 = stringsToMatch[3],
                    City = stringsToMatch[4],
                    Province = stringsToMatch[5],
                    Postalcode = stringsToMatch[6],
                    Country = stringsToMatch[7]
                },
                PrivateDwelling = true,
                SettingDescription = stringsToMatch[8]
            };
            

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

            CheckAddress(responseViewModel.EquipmentLocation.Address, stringsToMatch[1],
                stringsToMatch[2], stringsToMatch[3], stringsToMatch[4],
                stringsToMatch[5], stringsToMatch[6], stringsToMatch[7]
                );

            Assert.True(responseViewModel.EquipmentLocation.PrivateDwelling);
            Assert.Equal(stringsToMatch[8], responseViewModel.EquipmentLocation.SettingDescription);
            

            // U - Update  
            viewmodel_application = new Application();
            viewmodel_application.mainbusinessfocus = changedName;

            // test update with existing addresses

            for (int i = 0; i < stringsToMatch.Length; i++)
            {
                stringsToMatch[i] = RandomTextString(20, i);
            }

            viewmodel_application.EquipmentLocation = new Location()
            {
                Id = responseViewModel.id,
                Address = new CustomAddress()
                {
                    Id = responseViewModel.EquipmentLocation.Id,
                    StreetLine1 = stringsToMatch[1],
                    StreetLine2 = stringsToMatch[2],
                    StreetLine3 = stringsToMatch[3],
                    City = stringsToMatch[4],
                    Province = stringsToMatch[5],
                    Postalcode = stringsToMatch[6],
                    Country = stringsToMatch[7]
                },
                PrivateDwelling = false,
                SettingDescription = stringsToMatch[8]
            };

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

            CheckAddress(responseViewModel.EquipmentLocation.Address, stringsToMatch[1],
                stringsToMatch[2], stringsToMatch[3], stringsToMatch[4],
                stringsToMatch[5], stringsToMatch[6], stringsToMatch[7]
                );

            Assert.False(responseViewModel.EquipmentLocation.PrivateDwelling);
            Assert.Equal(stringsToMatch[8], responseViewModel.EquipmentLocation.SettingDescription);

            // U - Update  
            viewmodel_application = new Application();
            viewmodel_application.mainbusinessfocus = changedName;

            // test update with new addresses

            for (int i = 0; i < stringsToMatch.Length; i++)
            {
                stringsToMatch[i] = RandomTextString(20, i);
            }

            viewmodel_application.EquipmentLocation = new Location()
            {                
                Address = new CustomAddress()
                {                    
                    StreetLine1 = stringsToMatch[1],
                    StreetLine2 = stringsToMatch[2],
                    StreetLine3 = stringsToMatch[3],
                    City = stringsToMatch[4],
                    Province = stringsToMatch[5],
                    Postalcode = stringsToMatch[6],
                    Country = stringsToMatch[7]
                },
                PrivateDwelling = false,
                SettingDescription = stringsToMatch[8]
            };



            request = new HttpRequestMessage(HttpMethod.Put, "/api/" + service + "/" + id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(viewmodel_application), Encoding.UTF8, "application/json")
            };
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // verify that the update persisted.

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            

            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            responseViewModel = JsonConvert.DeserializeObject<Application>(jsonString);
            Assert.Equal(changedName, responseViewModel.mainbusinessfocus);

            CheckAddress(responseViewModel.EquipmentLocation.Address, stringsToMatch[1],
                stringsToMatch[2], stringsToMatch[3], stringsToMatch[4],
                stringsToMatch[5], stringsToMatch[6], stringsToMatch[7]
                );

            Assert.False(responseViewModel.EquipmentLocation.PrivateDwelling);
            Assert.Equal(stringsToMatch[8], responseViewModel.EquipmentLocation.SettingDescription);


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
