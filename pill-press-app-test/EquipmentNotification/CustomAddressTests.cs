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
        public EquipmentNotificationTests(CustomWebApplicationFactory<Startup> factory)
          : base(factory)
        { }

        

        [Fact]
        public async System.Threading.Tasks.Task TestCustomAddress()
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

            viewmodel_application.BCSellersAddress = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[1],
                StreetLine2 = stringsToMatch[2],
                StreetLine3 = stringsToMatch[3],
                City = stringsToMatch[4],
                Province = stringsToMatch[5],
                Postalcode = stringsToMatch[6],
                Country = stringsToMatch[7]
            };

            viewmodel_application.OutsideBCSellersAddress = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[11],
                StreetLine2 = stringsToMatch[12],
                StreetLine3 = stringsToMatch[13],
                City = stringsToMatch[14],
                Province = stringsToMatch[15],
                Postalcode = stringsToMatch[16],
                Country = stringsToMatch[17]
            };

            viewmodel_application.ImportersAddress = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[21],
                StreetLine2 = stringsToMatch[22],
                StreetLine3 = stringsToMatch[23],
                City = stringsToMatch[24],
                Province = stringsToMatch[25],
                Postalcode = stringsToMatch[26],
                Country = stringsToMatch[27]
            };

            viewmodel_application.OriginatingSellersAddress = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[31],
                StreetLine2 = stringsToMatch[32],
                StreetLine3 = stringsToMatch[33],
                City = stringsToMatch[34],
                Province = stringsToMatch[35],
                Postalcode = stringsToMatch[36],
                Country = stringsToMatch[37]
            };

            viewmodel_application.AddressofBusinessthathasGivenorLoaned = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[41],
                StreetLine2 = stringsToMatch[42],
                StreetLine3 = stringsToMatch[43],
                City = stringsToMatch[44],
                Province = stringsToMatch[45],
                Postalcode = stringsToMatch[46],
                Country = stringsToMatch[47]
            };

            viewmodel_application.AddressofBusinessThatHasRentedorLeased = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[51],
                StreetLine2 = stringsToMatch[52],
                StreetLine3 = stringsToMatch[53],
                City = stringsToMatch[54],
                Province = stringsToMatch[55],
                Postalcode = stringsToMatch[56],
                Country = stringsToMatch[57]
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

            CheckAddress(responseViewModel.BCSellersAddress, stringsToMatch[1],
                stringsToMatch[2], stringsToMatch[3], stringsToMatch[4],
                stringsToMatch[5], stringsToMatch[6], stringsToMatch[7]
                );

            CheckAddress(responseViewModel.OutsideBCSellersAddress, stringsToMatch[11],
                stringsToMatch[12], stringsToMatch[13], stringsToMatch[14],
                stringsToMatch[15], stringsToMatch[16], stringsToMatch[17]
                );

            CheckAddress(responseViewModel.ImportersAddress, stringsToMatch[21],
                stringsToMatch[22], stringsToMatch[23], stringsToMatch[24],
                stringsToMatch[25], stringsToMatch[26], stringsToMatch[27]
                );

            CheckAddress(responseViewModel.OriginatingSellersAddress, stringsToMatch[31],
                stringsToMatch[32], stringsToMatch[33], stringsToMatch[34],
                stringsToMatch[35], stringsToMatch[36], stringsToMatch[37]
                );

            CheckAddress(responseViewModel.AddressofBusinessthathasGivenorLoaned, stringsToMatch[41],
                stringsToMatch[42], stringsToMatch[43], stringsToMatch[44],
                stringsToMatch[45], stringsToMatch[46], stringsToMatch[47]
                );

            CheckAddress(responseViewModel.AddressofBusinessThatHasRentedorLeased, stringsToMatch[51],
                stringsToMatch[52], stringsToMatch[53], stringsToMatch[54],
                stringsToMatch[55], stringsToMatch[56], stringsToMatch[57]
                );

            Assert.True(responseViewModel.applicant != null);
            Assert.Equal(currentAccount.id, responseViewModel.applicant.id);


            // U - Update  
            viewmodel_application = new Application();
            viewmodel_application.mainbusinessfocus = changedName;

            // test update with existing addresses

            for (int i = 0; i < stringsToMatch.Length; i++)
            {
                stringsToMatch[i] = RandomTextString(20, i);
            }

            viewmodel_application.BCSellersAddress = new CustomAddress()
            {
                Id = responseViewModel.BCSellersAddress.Id,
                StreetLine1 = stringsToMatch[1],
                StreetLine2 = stringsToMatch[2],
                StreetLine3 = stringsToMatch[3],
                City = stringsToMatch[4],
                Province = stringsToMatch[5],
                Postalcode = stringsToMatch[6],
                Country = stringsToMatch[7]
            };

            viewmodel_application.OutsideBCSellersAddress = new CustomAddress()
            {
                Id = responseViewModel.OutsideBCSellersAddress.Id,
                StreetLine1 = stringsToMatch[11],
                StreetLine2 = stringsToMatch[12],
                StreetLine3 = stringsToMatch[13],
                City = stringsToMatch[14],
                Province = stringsToMatch[15],
                Postalcode = stringsToMatch[16],
                Country = stringsToMatch[17]
            };

            viewmodel_application.ImportersAddress = new CustomAddress()
            {
                Id = responseViewModel.ImportersAddress.Id,
                StreetLine1 = stringsToMatch[21],
                StreetLine2 = stringsToMatch[22],
                StreetLine3 = stringsToMatch[23],
                City = stringsToMatch[24],
                Province = stringsToMatch[25],
                Postalcode = stringsToMatch[26],
                Country = stringsToMatch[27]
            };

            viewmodel_application.OriginatingSellersAddress = new CustomAddress()
            {
                Id = responseViewModel.OriginatingSellersAddress.Id,
                StreetLine1 = stringsToMatch[31],
                StreetLine2 = stringsToMatch[32],
                StreetLine3 = stringsToMatch[33],
                City = stringsToMatch[34],
                Province = stringsToMatch[35],
                Postalcode = stringsToMatch[36],
                Country = stringsToMatch[37]
            };

            viewmodel_application.AddressofBusinessthathasGivenorLoaned = new CustomAddress()
            {
                Id = responseViewModel.AddressofBusinessthathasGivenorLoaned.Id,
                StreetLine1 = stringsToMatch[41],
                StreetLine2 = stringsToMatch[42],
                StreetLine3 = stringsToMatch[43],
                City = stringsToMatch[44],
                Province = stringsToMatch[45],
                Postalcode = stringsToMatch[46],
                Country = stringsToMatch[47]
            };

            viewmodel_application.AddressofBusinessThatHasRentedorLeased = new CustomAddress()
            {
                Id = responseViewModel.AddressofBusinessThatHasRentedorLeased.Id,
                StreetLine1 = stringsToMatch[51],
                StreetLine2 = stringsToMatch[52],
                StreetLine3 = stringsToMatch[53],
                City = stringsToMatch[54],
                Province = stringsToMatch[55],
                Postalcode = stringsToMatch[56],
                Country = stringsToMatch[57]
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

            CheckAddress(responseViewModel.BCSellersAddress, stringsToMatch[1],
               stringsToMatch[2], stringsToMatch[3], stringsToMatch[4],
               stringsToMatch[5], stringsToMatch[6], stringsToMatch[7]
               );

            CheckAddress(responseViewModel.OutsideBCSellersAddress, stringsToMatch[11],
                stringsToMatch[12], stringsToMatch[13], stringsToMatch[14],
                stringsToMatch[15], stringsToMatch[16], stringsToMatch[17]
                );

            CheckAddress(responseViewModel.ImportersAddress, stringsToMatch[21],
                stringsToMatch[22], stringsToMatch[23], stringsToMatch[24],
                stringsToMatch[25], stringsToMatch[26], stringsToMatch[27]
                );

            CheckAddress(responseViewModel.OriginatingSellersAddress, stringsToMatch[31],
                stringsToMatch[32], stringsToMatch[33], stringsToMatch[34],
                stringsToMatch[35], stringsToMatch[36], stringsToMatch[37]
                );

            CheckAddress(responseViewModel.AddressofBusinessthathasGivenorLoaned, stringsToMatch[41],
                stringsToMatch[42], stringsToMatch[43], stringsToMatch[44],
                stringsToMatch[45], stringsToMatch[46], stringsToMatch[47]
                );

            CheckAddress(responseViewModel.AddressofBusinessThatHasRentedorLeased, stringsToMatch[51],
                stringsToMatch[52], stringsToMatch[53], stringsToMatch[54],
                stringsToMatch[55], stringsToMatch[56], stringsToMatch[57]
                );


            // U - Update  
            viewmodel_application = new Application();
            viewmodel_application.mainbusinessfocus = changedName;

            // test update with new addresses

            for (int i = 0; i < stringsToMatch.Length; i++)
            {
                stringsToMatch[i] = RandomTextString(20, i);
            }

            viewmodel_application.BCSellersAddress = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[1],
                StreetLine2 = stringsToMatch[2],
                StreetLine3 = stringsToMatch[3],
                City = stringsToMatch[4],
                Province = stringsToMatch[5],
                Postalcode = stringsToMatch[6],
                Country = stringsToMatch[7]
            };

            viewmodel_application.OutsideBCSellersAddress = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[11],
                StreetLine2 = stringsToMatch[12],
                StreetLine3 = stringsToMatch[13],
                City = stringsToMatch[14],
                Province = stringsToMatch[15],
                Postalcode = stringsToMatch[16],
                Country = stringsToMatch[17]
            };

            viewmodel_application.ImportersAddress = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[21],
                StreetLine2 = stringsToMatch[22],
                StreetLine3 = stringsToMatch[23],
                City = stringsToMatch[24],
                Province = stringsToMatch[25],
                Postalcode = stringsToMatch[26],
                Country = stringsToMatch[27]
            };

            viewmodel_application.OriginatingSellersAddress = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[31],
                StreetLine2 = stringsToMatch[32],
                StreetLine3 = stringsToMatch[33],
                City = stringsToMatch[34],
                Province = stringsToMatch[35],
                Postalcode = stringsToMatch[36],
                Country = stringsToMatch[37]
            };

            viewmodel_application.AddressofBusinessthathasGivenorLoaned = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[41],
                StreetLine2 = stringsToMatch[42],
                StreetLine3 = stringsToMatch[43],
                City = stringsToMatch[44],
                Province = stringsToMatch[45],
                Postalcode = stringsToMatch[46],
                Country = stringsToMatch[47]
            };

            viewmodel_application.AddressofBusinessThatHasRentedorLeased = new CustomAddress()
            {
                StreetLine1 = stringsToMatch[51],
                StreetLine2 = stringsToMatch[52],
                StreetLine3 = stringsToMatch[53],
                City = stringsToMatch[54],
                Province = stringsToMatch[55],
                Postalcode = stringsToMatch[56],
                Country = stringsToMatch[57]
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

            CheckAddress(responseViewModel.BCSellersAddress, stringsToMatch[1],
               stringsToMatch[2], stringsToMatch[3], stringsToMatch[4],
               stringsToMatch[5], stringsToMatch[6], stringsToMatch[7]
               );

            CheckAddress(responseViewModel.OutsideBCSellersAddress, stringsToMatch[11],
                stringsToMatch[12], stringsToMatch[13], stringsToMatch[14],
                stringsToMatch[15], stringsToMatch[16], stringsToMatch[17]
                );

            CheckAddress(responseViewModel.ImportersAddress, stringsToMatch[21],
                stringsToMatch[22], stringsToMatch[23], stringsToMatch[24],
                stringsToMatch[25], stringsToMatch[26], stringsToMatch[27]
                );

            CheckAddress(responseViewModel.OriginatingSellersAddress, stringsToMatch[31],
                stringsToMatch[32], stringsToMatch[33], stringsToMatch[34],
                stringsToMatch[35], stringsToMatch[36], stringsToMatch[37]
                );

            CheckAddress(responseViewModel.AddressofBusinessthathasGivenorLoaned, stringsToMatch[41],
                stringsToMatch[42], stringsToMatch[43], stringsToMatch[44],
                stringsToMatch[45], stringsToMatch[46], stringsToMatch[47]
                );

            CheckAddress(responseViewModel.AddressofBusinessThatHasRentedorLeased, stringsToMatch[51],
                stringsToMatch[52], stringsToMatch[53], stringsToMatch[54],
                stringsToMatch[55], stringsToMatch[56], stringsToMatch[57]
                );

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
