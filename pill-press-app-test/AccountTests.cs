using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Gov.Jag.PillPressRegistry.Public.Test
{
    public class AccountTests : ApiIntegrationTestBaseWithLogin
    {
        public AccountTests(CustomWebApplicationFactory<Startup> factory)
          : base(factory)
        { }

        [Fact]
        public async System.Threading.Tasks.Task TestNoAccessToAnonymousUser()
        {
            string service = "account";
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

            string initialName = "InitialName";
            string changedName = "ChangedName";
            string initialBusinessNumber = "123456789";
            string changedBusinessNumber = "987654321";

            string service = "account";

            // register and login as our first user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            await Login(loginUser1);

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service);

            ViewModels.Account viewmodel_account = new ViewModels.Account()
            {
                businessType = "PublicCorporation",
                businessLegalName = initialName,
                doingBusinessAs = initialName,
                businessNumber = initialBusinessNumber,
                description = initialName,
                businessEmail = initialName,
                businessPhone = initialName,
                mailingAddressName = initialName,
                mailingAddressStreet = initialName,
                mailingAddressCity = initialName,
                mailingAddressCountry = initialName,
                mailingAddressProvince = initialName,
                mailingAddresPostalCode = initialName
            };

            string jsonString = JsonConvert.SerializeObject(viewmodel_account);

            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // parse as JSON.            
            ViewModels.Account responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);

            // verify the record.
            Assert.Equal(initialName, responseViewModel.businessLegalName);
            Assert.Equal(initialName, responseViewModel.doingBusinessAs);
            Assert.Equal(initialBusinessNumber, responseViewModel.businessNumber);
            Assert.Equal(initialName, responseViewModel.description);
            Assert.Equal(initialName, responseViewModel.businessEmail);
            Assert.Equal(initialName, responseViewModel.businessPhone);
            Assert.Equal(initialName, responseViewModel.mailingAddressName);
            Assert.Equal(initialName, responseViewModel.mailingAddressStreet);
            Assert.Equal(initialName, responseViewModel.mailingAddressCity);
            Assert.Equal(initialName, responseViewModel.mailingAddressCountry);
            Assert.Equal(initialName, responseViewModel.mailingAddressProvince);
            Assert.Equal(initialName, responseViewModel.mailingAddresPostalCode);

            Guid id = new Guid(responseViewModel.id);
            //String strid = responseViewModel.externalId;
            //Assert.Equal(strid, viewmodel_account.externalId);

            // R - Read

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);

            // verify the record.
            Assert.Equal(initialName, responseViewModel.businessLegalName);
            Assert.Equal(initialName, responseViewModel.doingBusinessAs);
            Assert.Equal(initialBusinessNumber, responseViewModel.businessNumber);
            Assert.Equal(initialName, responseViewModel.description);
            Assert.Equal(initialName, responseViewModel.businessEmail);
            Assert.Equal(initialName, responseViewModel.businessPhone);
            Assert.Equal(initialName, responseViewModel.mailingAddressName);
            Assert.Equal(initialName, responseViewModel.mailingAddressStreet);
            Assert.Equal(initialName, responseViewModel.mailingAddressCity);
            Assert.Equal(initialName, responseViewModel.mailingAddressCountry);
            Assert.Equal(initialName, responseViewModel.mailingAddressProvince);
            Assert.Equal(initialName, responseViewModel.mailingAddresPostalCode);

            // U - Update 
            ViewModels.Account changedAccount = new ViewModels.Account()
            {
                businessType = "PublicCorporation",
                businessLegalName = changedName,
                doingBusinessAs = changedName,
                businessNumber = changedBusinessNumber,
                description = changedName,             
                businessEmail = changedName,
                businessPhone = changedName,
                mailingAddressName = changedName,
                mailingAddressStreet = changedName,
                mailingAddressCity = changedName,
                mailingAddressCountry = changedName,
                mailingAddressProvince = changedName,
                mailingAddresPostalCode = changedName
            };
           
            request = new HttpRequestMessage(HttpMethod.Put, "/api/" + service + "/" + id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(changedAccount), Encoding.UTF8, "application/json")
            };
            response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // verify that the update persisted.
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            // verify the record.
            Assert.Equal(changedName, responseViewModel.businessLegalName);
            Assert.Equal(changedName, responseViewModel.doingBusinessAs);
            Assert.Equal(changedBusinessNumber, responseViewModel.businessNumber);
            Assert.Equal(changedName, responseViewModel.description);
            Assert.Equal(changedName, responseViewModel.businessEmail);
            Assert.Equal(changedName, responseViewModel.businessPhone);
            Assert.Equal(changedName, responseViewModel.mailingAddressName);
            Assert.Equal(changedName, responseViewModel.mailingAddressStreet);
            Assert.Equal(changedName, responseViewModel.mailingAddressCity);
            Assert.Equal(changedName, responseViewModel.mailingAddressCountry);
            Assert.Equal(changedName, responseViewModel.mailingAddressProvince);
            Assert.Equal(changedName, responseViewModel.mailingAddresPostalCode);

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();

            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            Assert.Equal(changedName, responseViewModel.doingBusinessAs);

            // D - Delete

            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // second delete should return a 404.
            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // should get a 404 if we try a get now.
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await Logout();
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPrimaryContact()
        {
            string initialName = "InitialName";
            string changedName = "ChangedName";
            string initialPhoneNumber = "1231231234";
            string changedPhoneNumber = "987654321";
            string initialBusinessNumber = "123456789";
            string changedBusinessNumber = "987654321";

            string service = "account";

            // register and login as our first user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            await Login(loginUser1);

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service);
            

            ViewModels.Contact contact = new ViewModels.Contact()
            {
                firstName = initialName,
                lastName = initialName,
                title = initialName,
                phoneNumber = initialPhoneNumber,
                phoneNumberAlt = initialPhoneNumber,
                email = initialName
            };

            ViewModels.Account viewmodel_account = new ViewModels.Account()
            {
                businessType = "PublicCorporation",
                businessLegalName = initialName,
                doingBusinessAs = initialName,
                businessNumber = initialBusinessNumber,
                description = initialName,
                businessEmail = initialName,
                businessPhone = initialName,
                mailingAddressName = initialName,
                mailingAddressStreet = initialName,
                mailingAddressCity = initialName,
                mailingAddressCountry = initialName,
                mailingAddressProvince = initialName,
                mailingAddresPostalCode = initialName,
                primaryContact = contact
            };


            string jsonString = JsonConvert.SerializeObject(viewmodel_account);

            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // parse as JSON.            
            ViewModels.Account responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);

            // verify the record.
            Assert.NotNull(responseViewModel.primaryContact);
            Assert.Equal(initialName, responseViewModel.primaryContact.firstName);
            Assert.Equal(initialName, responseViewModel.primaryContact.lastName);
            Assert.Equal(initialName, responseViewModel.primaryContact.title);
            Assert.Equal(initialPhoneNumber, responseViewModel.primaryContact.phoneNumber);
            Assert.Equal(initialPhoneNumber, responseViewModel.primaryContact.phoneNumberAlt);
            Assert.Equal(initialName, responseViewModel.primaryContact.email);

            Guid id = new Guid(responseViewModel.id);
            //String strid = responseViewModel.externalId;
            //Assert.Equal(strid, viewmodel_account.externalId);

            // R - Read

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);

            // verify the record.
            Assert.NotNull(responseViewModel.primaryContact);

            // U - Update 
            ViewModels.Contact changedContact = new ViewModels.Contact()
            {
                id = responseViewModel.primaryContact.id,
                firstName = changedName,
                lastName = changedName,
                title = changedName,
                phoneNumber = changedPhoneNumber,
                phoneNumberAlt = changedPhoneNumber,
                email = changedName
            };

            ViewModels.Account changedAccount = new ViewModels.Account()
            {
                businessType = "PublicCorporation",
                businessLegalName = changedName,
                doingBusinessAs = changedName,
                businessNumber = changedBusinessNumber,
                description = changedName,
                businessEmail = changedName,
                businessPhone = changedName,
                mailingAddressName = changedName,
                mailingAddressStreet = changedName,
                mailingAddressCity = changedName,
                mailingAddressCountry = changedName,
                mailingAddressProvince = changedName,
                mailingAddresPostalCode = changedName,
                primaryContact = changedContact
            };

            request = new HttpRequestMessage(HttpMethod.Put, "/api/" + service + "/" + id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(changedAccount), Encoding.UTF8, "application/json")
            };
            response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // verify that the update persisted.
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            // verify the record.
            Assert.Equal(changedName, responseViewModel.businessLegalName);
            Assert.Equal(changedName, responseViewModel.doingBusinessAs);
            Assert.Equal(changedBusinessNumber, responseViewModel.businessNumber);
            Assert.Equal(changedName, responseViewModel.description);
            Assert.Equal(changedName, responseViewModel.businessEmail);
            Assert.Equal(changedName, responseViewModel.businessPhone);
            Assert.Equal(changedName, responseViewModel.mailingAddressName);
            Assert.Equal(changedName, responseViewModel.mailingAddressStreet);
            Assert.Equal(changedName, responseViewModel.mailingAddressCity);
            Assert.Equal(changedName, responseViewModel.mailingAddressCountry);
            Assert.Equal(changedName, responseViewModel.mailingAddressProvince);
            Assert.Equal(changedName, responseViewModel.mailingAddresPostalCode);

            Assert.NotNull(responseViewModel.primaryContact);
            Assert.Equal(changedName, responseViewModel.primaryContact.firstName);
            Assert.Equal(changedName, responseViewModel.primaryContact.lastName);
            Assert.Equal(changedName, responseViewModel.primaryContact.title);
            Assert.Equal(changedPhoneNumber, responseViewModel.primaryContact.phoneNumber);
            Assert.Equal(changedPhoneNumber, responseViewModel.primaryContact.phoneNumberAlt);
            Assert.Equal(changedName, responseViewModel.primaryContact.email);


            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();

            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            Assert.Equal(changedName, responseViewModel.doingBusinessAs);

            // D - Delete

            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // second delete should return a 404.
            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // should get a 404 if we try a get now.
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await Logout();
        }


        [Fact]
        public async System.Threading.Tasks.Task TestAdditionalContact()
        {
            string initialName = "InitialName";
            string changedName = "ChangedName";
            string initialPhoneNumber = "1231231234";
            string changedPhoneNumber = "987654321";
            string initialBusinessNumber = "123456789";
            string changedBusinessNumber = "987654321";

            string service = "account";

            // register and login as our first user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            await Login(loginUser1);

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service);


            ViewModels.Contact contact = new ViewModels.Contact()
            {
                firstName = initialName,
                lastName = initialName,
                title = initialName,
                phoneNumber = initialPhoneNumber,
                phoneNumberAlt = initialPhoneNumber,
                email = initialName,
            };

            ViewModels.Account viewmodel_account = new ViewModels.Account()
            {
                businessType = "PublicCorporation",
                businessLegalName = initialName,
                doingBusinessAs = initialName,
                businessNumber = initialBusinessNumber,
                description = initialName,
                businessEmail = initialName,
                businessPhone = initialName,
                mailingAddressName = initialName,
                mailingAddressStreet = initialName,
                mailingAddressCity = initialName,
                mailingAddressCountry = initialName,
                mailingAddressProvince = initialName,
                mailingAddresPostalCode = initialName,
                additionalContact = contact
            };

            string jsonString = JsonConvert.SerializeObject(viewmodel_account);

            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // parse as JSON.            
            ViewModels.Account responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);

            // verify the record.
            Assert.NotNull(responseViewModel.additionalContact);

            Guid id = new Guid(responseViewModel.id);
            //String strid = responseViewModel.externalId;
            //Assert.Equal(strid, viewmodel_account.externalId);

            // R - Read

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);

            // verify the record.

            Assert.NotNull(responseViewModel.additionalContact);
            Assert.Equal(initialName, responseViewModel.additionalContact.firstName);
            Assert.Equal(initialName, responseViewModel.additionalContact.lastName);
            Assert.Equal(initialName, responseViewModel.additionalContact.title);
            Assert.Equal(initialPhoneNumber, responseViewModel.additionalContact.phoneNumber);
            Assert.Equal(initialPhoneNumber, responseViewModel.additionalContact.phoneNumberAlt);
            Assert.Equal(initialName, responseViewModel.additionalContact.email);

            // U - Update 
            ViewModels.Contact changedContact = new ViewModels.Contact()
            {
                id = responseViewModel.additionalContact.id,
                firstName = changedName,
                lastName = changedName,
                title = changedName,
                phoneNumber = changedPhoneNumber,
                phoneNumberAlt = changedPhoneNumber,
                email = changedName
            };

            ViewModels.Account changedAccount = new ViewModels.Account()
            {
                businessType = "PublicCorporation",
                businessLegalName = changedName,
                doingBusinessAs = changedName,
                businessNumber = changedBusinessNumber,
                description = changedName,
                businessEmail = changedName,
                businessPhone = changedName,
                mailingAddressName = changedName,
                mailingAddressStreet = changedName,
                mailingAddressCity = changedName,
                mailingAddressCountry = changedName,
                mailingAddressProvince = changedName,
                mailingAddresPostalCode = changedName,
                additionalContact = changedContact
            };

            request = new HttpRequestMessage(HttpMethod.Put, "/api/" + service + "/" + id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(changedAccount), Encoding.UTF8, "application/json")
            };
            response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // verify that the update persisted.
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            // verify the record.
            Assert.Equal(changedName, responseViewModel.businessLegalName);
            Assert.Equal(changedName, responseViewModel.doingBusinessAs);
            Assert.Equal(changedBusinessNumber, responseViewModel.businessNumber);
            Assert.Equal(changedName, responseViewModel.description);
            Assert.Equal(changedName, responseViewModel.businessEmail);
            Assert.Equal(changedName, responseViewModel.businessPhone);
            Assert.Equal(changedName, responseViewModel.mailingAddressName);
            Assert.Equal(changedName, responseViewModel.mailingAddressStreet);
            Assert.Equal(changedName, responseViewModel.mailingAddressCity);
            Assert.Equal(changedName, responseViewModel.mailingAddressCountry);
            Assert.Equal(changedName, responseViewModel.mailingAddressProvince);
            Assert.Equal(changedName, responseViewModel.mailingAddresPostalCode);

            Assert.NotNull(responseViewModel.additionalContact);
            Assert.Equal(changedName, responseViewModel.additionalContact.firstName);
            Assert.Equal(changedName, responseViewModel.additionalContact.lastName);
            Assert.Equal(changedName, responseViewModel.additionalContact.title);
            Assert.Equal(changedPhoneNumber, responseViewModel.additionalContact.phoneNumber);
            Assert.Equal(changedPhoneNumber, responseViewModel.additionalContact.phoneNumberAlt);
            Assert.Equal(changedName, responseViewModel.additionalContact.email);

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();

            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            Assert.Equal(changedName, responseViewModel.doingBusinessAs);

            // D - Delete

            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // second delete should return a 404.
            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // should get a 404 if we try a get now.
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await Logout();
        }


        [Fact]
        public async System.Threading.Tasks.Task TestMailingAddress()
        {
            string initialName = "InitialName";
            string changedName = "ChangedName";
            string initialPhoneNumber = "1231231234";
            string changedPhoneNumber = "987654321";
            string initialBusinessNumber = "123456789";
            string changedBusinessNumber = "987654321";

            string service = "account";

            // register and login as our first user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            await Login(loginUser1);

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service);


            ViewModels.CustomAddress customAddress = new ViewModels.CustomAddress()
            {
                Emailaddress = initialName,
                Streetline1 = initialName,
                Streetline2 = initialName,
                Streetline3 = initialName,
                City = initialName,
                Province = initialName,
                Postalcode = initialName
                // Country
            };

            ViewModels.Account viewmodel_account = new ViewModels.Account()
            {
                businessType = "PublicCorporation",
                businessLegalName = initialName,
                doingBusinessAs = initialName,
                businessNumber = initialBusinessNumber,
                description = initialName,
                businessEmail = initialName,
                businessPhone = initialPhoneNumber,
                mailingAddressName = initialName,
                mailingAddressStreet = initialName,
                mailingAddressCity = initialName,
                mailingAddressCountry = initialName,
                mailingAddressProvince = initialName,
                mailingAddresPostalCode = initialName,
                mailingAddress = customAddress
            };

            string jsonString = JsonConvert.SerializeObject(viewmodel_account);

            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // parse as JSON.            
            ViewModels.Account responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);

            // verify the record.
            Assert.NotNull(responseViewModel.mailingAddress);

            Guid id = new Guid(responseViewModel.id);
            //String strid = responseViewModel.externalId;
            //Assert.Equal(strid, viewmodel_account.externalId);

            // R - Read

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);

            // verify the record.

            Assert.NotNull(responseViewModel.mailingAddress);
            Assert.Equal(initialName, responseViewModel.mailingAddress.Emailaddress);
            Assert.Equal(initialName, responseViewModel.mailingAddress.Streetline1);
            Assert.Equal(initialName, responseViewModel.mailingAddress.Streetline2);
            Assert.Equal(initialName, responseViewModel.mailingAddress.Streetline3);
            Assert.Equal(initialName, responseViewModel.mailingAddress.City);
            Assert.Equal(initialName, responseViewModel.mailingAddress.Province);
            Assert.Equal(initialName, responseViewModel.mailingAddress.Postalcode);

            // U - Update 
            ViewModels.CustomAddress changedAddress = new ViewModels.CustomAddress()
            {
                Id = responseViewModel.mailingAddress.Id,
                Emailaddress = changedName,
                Streetline1 = changedName,
                Streetline2 = changedName,
                Streetline3 = changedName,
                City = changedName,
                Province = changedName,
                Postalcode = changedName
                // Country
            };

            ViewModels.Account changedAccount = new ViewModels.Account()
            {
                businessType = "PublicCorporation",
                businessLegalName = changedName,
                doingBusinessAs = changedName,
                businessNumber = changedBusinessNumber,
                description = changedName,
                businessEmail = changedName,
                businessPhone = changedPhoneNumber,
                mailingAddressName = changedName,
                mailingAddressStreet = changedName,
                mailingAddressCity = changedName,
                mailingAddressCountry = changedName,
                mailingAddressProvince = changedName,
                mailingAddresPostalCode = changedName,
                mailingAddress = changedAddress
            };

            request = new HttpRequestMessage(HttpMethod.Put, "/api/" + service + "/" + id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(changedAccount), Encoding.UTF8, "application/json")
            };
            response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // verify that the update persisted.
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            // verify the record.
            Assert.Equal(changedName, responseViewModel.businessLegalName);
            Assert.Equal(changedName, responseViewModel.doingBusinessAs);
            Assert.Equal(changedBusinessNumber, responseViewModel.businessNumber);
            Assert.Equal(changedName, responseViewModel.description);
            Assert.Equal(changedName, responseViewModel.businessEmail);
            Assert.Equal(changedPhoneNumber, responseViewModel.businessPhone);
            Assert.Equal(changedName, responseViewModel.mailingAddressName);
            Assert.Equal(changedName, responseViewModel.mailingAddressStreet);
            Assert.Equal(changedName, responseViewModel.mailingAddressCity);
            Assert.Equal(changedName, responseViewModel.mailingAddressCountry);
            Assert.Equal(changedName, responseViewModel.mailingAddressProvince);
            Assert.Equal(changedName, responseViewModel.mailingAddresPostalCode);

            Assert.NotNull(responseViewModel.mailingAddress);
            Assert.Equal(changedName, responseViewModel.mailingAddress.Emailaddress);
            Assert.Equal(changedName, responseViewModel.mailingAddress.Streetline1);
            Assert.Equal(changedName, responseViewModel.mailingAddress.Streetline2);
            Assert.Equal(changedName, responseViewModel.mailingAddress.Streetline3);
            Assert.Equal(changedName, responseViewModel.mailingAddress.City);
            Assert.Equal(changedName, responseViewModel.mailingAddress.Province);
            Assert.Equal(changedName, responseViewModel.mailingAddress.Postalcode);

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();

            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            Assert.Equal(changedName, responseViewModel.doingBusinessAs);

            // D - Delete

            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // second delete should return a 404.
            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // should get a 404 if we try a get now.
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await Logout();
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPhysicalAddress()
        {
            string initialName = "InitialName";
            string changedName = "ChangedName";
            string initialPhoneNumber = "1231231234";
            string changedPhoneNumber = "987654321";
            string initialBusinessNumber = "123456789";
            string changedBusinessNumber = "987654321";

            string service = "account";

            // register and login as our first user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            await Login(loginUser1);

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service);


            ViewModels.CustomAddress customAddress = new ViewModels.CustomAddress()
            {             
                    Emailaddress = initialName,
                    Streetline1 = initialName,
                    Streetline2 = initialName,
                    Streetline3 = initialName,
                    City = initialName,
                    Province = initialName,
                    Postalcode = initialName
                    // Country
            };

            ViewModels.Account viewmodel_account = new ViewModels.Account()
            {
                businessType = "PublicCorporation",
                businessLegalName = initialName,
                doingBusinessAs = initialName,
                businessNumber = initialBusinessNumber,
                description = initialName,
                businessEmail = initialName,
                businessPhone = initialPhoneNumber,
                mailingAddressName = initialName,
                mailingAddressStreet = initialName,
                mailingAddressCity = initialName,
                mailingAddressCountry = initialName,
                mailingAddressProvince = initialName,
                mailingAddresPostalCode = initialName,
                physicalAddress = customAddress
            };

            string jsonString = JsonConvert.SerializeObject(viewmodel_account);

            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // parse as JSON.            
            ViewModels.Account responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);

            // verify the record.
            Assert.NotNull(responseViewModel.physicalAddress);

            Guid id = new Guid(responseViewModel.id);
            //String strid = responseViewModel.externalId;
            //Assert.Equal(strid, viewmodel_account.externalId);

            // R - Read

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);

            // verify the record.

            Assert.NotNull(responseViewModel.physicalAddress);
            Assert.Equal(initialName, responseViewModel.physicalAddress.Emailaddress);
            Assert.Equal(initialName, responseViewModel.physicalAddress.Streetline1);
            Assert.Equal(initialName, responseViewModel.physicalAddress.Streetline2);
            Assert.Equal(initialName, responseViewModel.physicalAddress.Streetline3);
            Assert.Equal(initialName, responseViewModel.physicalAddress.City);
            Assert.Equal(initialName, responseViewModel.physicalAddress.Province);
            Assert.Equal(initialName, responseViewModel.physicalAddress.Postalcode);

            // U - Update 
            ViewModels.CustomAddress changedAddress = new ViewModels.CustomAddress()
            {
                Id = responseViewModel.physicalAddress.Id,
                Emailaddress = changedName,
                Streetline1 = changedName,
                Streetline2 = changedName,
                Streetline3 = changedName,
                City = changedName,
                Province = changedName,
                Postalcode = changedName
                // Country
            };

            ViewModels.Account changedAccount = new ViewModels.Account()
            {
                businessType = "PublicCorporation",
                businessLegalName = changedName,
                doingBusinessAs = changedName,
                businessNumber = changedBusinessNumber,
                description = changedName,
                businessEmail = changedName,
                businessPhone = changedPhoneNumber,
                mailingAddressName = changedName,
                mailingAddressStreet = changedName,
                mailingAddressCity = changedName,
                mailingAddressCountry = changedName,
                mailingAddressProvince = changedName,
                mailingAddresPostalCode = changedName,
                physicalAddress = changedAddress
            };

            request = new HttpRequestMessage(HttpMethod.Put, "/api/" + service + "/" + id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(changedAccount), Encoding.UTF8, "application/json")
            };
            response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // verify that the update persisted.
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            // verify the record.
            Assert.Equal(changedName, responseViewModel.businessLegalName);
            Assert.Equal(changedName, responseViewModel.doingBusinessAs);
            Assert.Equal(changedBusinessNumber, responseViewModel.businessNumber);
            Assert.Equal(changedName, responseViewModel.description);
            Assert.Equal(changedName, responseViewModel.businessEmail);
            Assert.Equal(changedPhoneNumber, responseViewModel.businessPhone);
            Assert.Equal(changedName, responseViewModel.mailingAddressName);
            Assert.Equal(changedName, responseViewModel.mailingAddressStreet);
            Assert.Equal(changedName, responseViewModel.mailingAddressCity);
            Assert.Equal(changedName, responseViewModel.mailingAddressCountry);
            Assert.Equal(changedName, responseViewModel.mailingAddressProvince);
            Assert.Equal(changedName, responseViewModel.mailingAddresPostalCode);

            Assert.NotNull(responseViewModel.physicalAddress);
            Assert.Equal(changedName, responseViewModel.physicalAddress.Emailaddress);
            Assert.Equal(changedName, responseViewModel.physicalAddress.Streetline1);
            Assert.Equal(changedName, responseViewModel.physicalAddress.Streetline2);
            Assert.Equal(changedName, responseViewModel.physicalAddress.Streetline3);
            Assert.Equal(changedName, responseViewModel.physicalAddress.City);
            Assert.Equal(changedName, responseViewModel.physicalAddress.Province);
            Assert.Equal(changedName, responseViewModel.physicalAddress.Postalcode);

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();

            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            Assert.Equal(changedName, responseViewModel.doingBusinessAs);

            // D - Delete

            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // second delete should return a 404.
            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // should get a 404 if we try a get now.
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await Logout();
        }



        [Fact]
        public async System.Threading.Tasks.Task TestCRUDLongName()
        {

            string initialName = randomNewUserName("Test", 246);
            string changedName = randomNewUserName("Test", 246);
            string service = "account";

            // register and login as our first user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            await Login(loginUser1);

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service);

            MicrosoftDynamicsCRMaccount account = new MicrosoftDynamicsCRMaccount()
            {
                Name = initialName,                
            };

            ViewModels.Account viewmodel_account = account.ToViewModel();
            viewmodel_account.doingBusinessAs = initialName;
            viewmodel_account.businessType = "PublicCorporation";

            string jsonString = JsonConvert.SerializeObject(viewmodel_account);

            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // parse as JSON.            
            ViewModels.Account responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);

            // name should match.
            Assert.Equal(initialName, responseViewModel.doingBusinessAs);
            Guid id = new Guid(responseViewModel.id);
            //String strid = responseViewModel.externalId;
            //Assert.Equal(strid, viewmodel_account.externalId);

            // R - Read

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();
            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            Assert.Equal(initialName, responseViewModel.doingBusinessAs);

            account.Accountid = id.ToString();


            ViewModels.Account changedAccount = new ViewModels.Account()
            {
                doingBusinessAs = changedName
            };
            // U - Update            
            
            request = new HttpRequestMessage(HttpMethod.Put, "/api/" + service + "/" + id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(changedAccount), Encoding.UTF8, "application/json")
            };
            response = await _client.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // verify that the update persisted.

            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            jsonString = await response.Content.ReadAsStringAsync();

            responseViewModel = JsonConvert.DeserializeObject<ViewModels.Account>(jsonString);
            Assert.Equal(changedName, responseViewModel.doingBusinessAs);

            // D - Delete

            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            // second delete should return a 404.
            request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/" + id + "/delete");
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // should get a 404 if we try a get now.
            request = new HttpRequestMessage(HttpMethod.Get, "/api/" + service + "/" + id);
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await Logout();
        }
    }
}
