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
        public async System.Threading.Tasks.Task TestCRUD()
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

            // setup the equipment fields.
            viewmodel_application.EquipmentType = Equipmenttype.DieMouldorPunch;
            viewmodel_application.EquipmentTypeOther = stringsToMatch[0];

            viewmodel_application.PillpressEncapsulatorSizeOtherCheck = true;
            viewmodel_application.PillpressEncapsulatorSizeOther = stringsToMatch[1];

            viewmodel_application.ExplanationOfEquipmentUse = stringsToMatch[2];

            viewmodel_application.HowWasEquipmentBuiltOtherCheck = true;
            viewmodel_application.HowWasEquipmentBuiltOther = stringsToMatch[3];
            viewmodel_application.NameOfManufacturer = stringsToMatch[4];
            viewmodel_application.EquipmentMake = stringsToMatch[5];
            viewmodel_application.EquipmentModel = stringsToMatch[6];
            viewmodel_application.SerialNumber = stringsToMatch[7];
            viewmodel_application.HowEquipmentBuiltDescription = stringsToMatch[8];
            viewmodel_application.PersonBusinessThatBuiltEquipment = stringsToMatch[9];
            viewmodel_application.SerialNumberForCustomBuilt = true;
            viewmodel_application.CustomBuiltSerialNumber = stringsToMatch[10];
            viewmodel_application.SerialNumberKeyPartDescription = stringsToMatch[11];
            viewmodel_application.OwnedBeforeJan2019 = true;
            viewmodel_application.PurchasedFromBcSeller = true;
            viewmodel_application.PurchasedFromSellerOutsideofBc = true;
            viewmodel_application.ImportedToBcByAThirdParty = true;
            viewmodel_application.AlternativeOwnershipArrangement = true;
            viewmodel_application.IAssembledItMyself = true;
            viewmodel_application.HowCameIntoPossessionOtherCheck = true;
            viewmodel_application.HowCameIntoPossessionOther = stringsToMatch[12];
            viewmodel_application.NameOfBcSeller = stringsToMatch[13];
            viewmodel_application.Dateofpurchasefrombcseller = dto;
            viewmodel_application.BcSellersRegistrationNumber = stringsToMatch[14];
            viewmodel_application.BcSellersContactPhoneNumber = initialPhoneNumber;
            viewmodel_application.BcSellersContactEmail = stringsToMatch[16];
            viewmodel_application.OutsideBcSellersName = stringsToMatch[17];
            viewmodel_application.DateOfPurchaseFromOutsideBcSeller = dto;
            viewmodel_application.NameOfImporter = stringsToMatch[18];
            viewmodel_application.ImportersRegistrationNumber = stringsToMatch[19];
            viewmodel_application.Nameoforiginatingseller = stringsToMatch[20];

            viewmodel_application.DateOfPurchaseFromImporter = dto;
            viewmodel_application.PossessUntilICanSell = true;
            viewmodel_application.GiveNorLoanedToMe = true;
            viewmodel_application.RentingOrLeasingFromAnotherBusiness = true;
            viewmodel_application.KindOfAlternateOwnershipOtherCheck = true;
            viewmodel_application.KindOfAlternateOwnershipOther = stringsToMatch[21];
            viewmodel_application.UsingToManufactureAProduct = true;
            viewmodel_application.AreYouARegisteredSeller = true;
            viewmodel_application.NameOfBusinessThatHasGivenOrLoaned = stringsToMatch[22];

            viewmodel_application.PhoneOfBusinessThatHasGivenOrLoaned = initialPhoneNumber;
            viewmodel_application.EmailOfTheBusinessThatHasGivenOrLoaned = stringsToMatch[24];
            viewmodel_application.WhyAHaveYouAcceptedOrBorrowed = stringsToMatch[25];
            viewmodel_application.NameOfBusinessThatHasRentedOrLeased = stringsToMatch[26];

            viewmodel_application.PhoneOfBusinessThatHasRentedOrLeased = initialPhoneNumber;
            viewmodel_application.EmailOfBusinessThatHasRentedOrLeased = stringsToMatch[28];
            viewmodel_application.WhyHaveYouRentedOrLeased = stringsToMatch[29];
            viewmodel_application.WhenDidYouAssembleEquipment = dto;
            viewmodel_application.WhereDidYouObtainParts = stringsToMatch[30];
            viewmodel_application.DoYouAssembleForOtherBusinesses = true;
            viewmodel_application.DetailsOfAssemblyForOtherBusinesses = stringsToMatch[31];
            viewmodel_application.DetailsOfHowEquipmentCameIntoPossession = stringsToMatch[32];
            viewmodel_application.DeclarationOfCorrectInformation = true;
            viewmodel_application.ConfirmationOfAuthorizedUse = true;

            viewmodel_application.SubmittedDate = dto;

            viewmodel_application.LevelOfEquipmentAutomation = Levelofequipmentautomation.Automated;
            viewmodel_application.PillpressEncapsulatorSize = Pillpressencapsulatorsize.FreeStandingModel;
            viewmodel_application.PillpressMaxCapacity = 1000;
            viewmodel_application.EncapsulatorMaxCapacity = 1000;
            viewmodel_application.HowWasEquipmentBuilt = Howwasequipmentbuilt.CommerciallyManufactured;

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

            Assert.Equal(responseViewModel.EquipmentType, Equipmenttype.DieMouldorPunch);
            Assert.Equal(responseViewModel.EquipmentTypeOther, stringsToMatch[0]);
      

            Assert.True(responseViewModel.PillpressEncapsulatorSizeOtherCheck);
            Assert.Equal(responseViewModel.PillpressEncapsulatorSizeOther,stringsToMatch[1]);

            Assert.Equal(responseViewModel.ExplanationOfEquipmentUse, stringsToMatch[2]);

            Assert.True(responseViewModel.HowWasEquipmentBuiltOtherCheck);
            Assert.Equal(responseViewModel.HowWasEquipmentBuiltOther , stringsToMatch[3]);
            Assert.Equal(responseViewModel.NameOfManufacturer , stringsToMatch[4]);
            Assert.Equal(responseViewModel.EquipmentMake ,stringsToMatch[5]);
            Assert.Equal(responseViewModel.EquipmentModel, stringsToMatch[6]);
            Assert.Equal(responseViewModel.SerialNumber , stringsToMatch[7]);
            Assert.Equal(responseViewModel.HowEquipmentBuiltDescription, stringsToMatch[8]);
            Assert.Equal(responseViewModel.PersonBusinessThatBuiltEquipment , stringsToMatch[9]);
            Assert.True(responseViewModel.SerialNumberForCustomBuilt);
            Assert.Equal(responseViewModel.CustomBuiltSerialNumber, stringsToMatch[10]);
            Assert.Equal(responseViewModel.SerialNumberKeyPartDescription ,stringsToMatch[11]);
            Assert.True(responseViewModel.OwnedBeforeJan2019);
            Assert.True(responseViewModel.PurchasedFromBcSeller);
            Assert.True(responseViewModel.PurchasedFromSellerOutsideofBc);
            Assert.True(responseViewModel.ImportedToBcByAThirdParty);
            Assert.True(responseViewModel.AlternativeOwnershipArrangement);
            Assert.True(responseViewModel.IAssembledItMyself);
            Assert.True(responseViewModel.HowCameIntoPossessionOtherCheck);
            Assert.Equal(responseViewModel.HowCameIntoPossessionOther , stringsToMatch[12]);
            Assert.Equal(responseViewModel.NameOfBcSeller, stringsToMatch[13]);
            Assert.Equal(responseViewModel.Dateofpurchasefrombcseller.Value.Date , dto.Date);
            Assert.Equal(responseViewModel.BcSellersRegistrationNumber ,stringsToMatch[14]);
            Assert.Equal(responseViewModel.BcSellersContactPhoneNumber , initialPhoneNumber);
            Assert.Equal(responseViewModel.BcSellersContactEmail , stringsToMatch[16]);
            Assert.Equal(responseViewModel.OutsideBcSellersName , stringsToMatch[17]);
            Assert.Equal(responseViewModel.DateOfPurchaseFromOutsideBcSeller.Value.Date , dto.Date);
            Assert.Equal(responseViewModel.NameOfImporter , stringsToMatch[18]);
            Assert.Equal(responseViewModel.ImportersRegistrationNumber , stringsToMatch[19]);
            Assert.Equal(responseViewModel.Nameoforiginatingseller, stringsToMatch[20]);

            Assert.Equal(responseViewModel.DateOfPurchaseFromImporter.Value.Date, dto.Date);
            Assert.True(responseViewModel.PossessUntilICanSell);
            Assert.True(responseViewModel.GiveNorLoanedToMe);
            Assert.True(responseViewModel.RentingOrLeasingFromAnotherBusiness);
            Assert.True(responseViewModel.KindOfAlternateOwnershipOtherCheck);
            Assert.Equal(responseViewModel.KindOfAlternateOwnershipOther,stringsToMatch[21]);
            Assert.True(responseViewModel.UsingToManufactureAProduct);
            Assert.True(responseViewModel.AreYouARegisteredSeller);
            Assert.Equal(responseViewModel.NameOfBusinessThatHasGivenOrLoaned , stringsToMatch[22]);

            Assert.Equal(responseViewModel.PhoneOfBusinessThatHasGivenOrLoaned ,initialPhoneNumber);
            Assert.Equal(responseViewModel.EmailOfTheBusinessThatHasGivenOrLoaned , stringsToMatch[24]);
            Assert.Equal(responseViewModel.WhyAHaveYouAcceptedOrBorrowed, stringsToMatch[25]);
            Assert.Equal(responseViewModel.NameOfBusinessThatHasRentedOrLeased, stringsToMatch[26]);

            Assert.Equal(responseViewModel.PhoneOfBusinessThatHasRentedOrLeased, initialPhoneNumber);
            Assert.Equal(responseViewModel.EmailOfBusinessThatHasRentedOrLeased, stringsToMatch[28]);
            Assert.Equal(responseViewModel.WhyHaveYouRentedOrLeased, stringsToMatch[29]);
            Assert.Equal(responseViewModel.WhenDidYouAssembleEquipment.Value.Date, dto.Date);
            Assert.Equal(responseViewModel.WhereDidYouObtainParts, stringsToMatch[30]);
            Assert.True(responseViewModel.DoYouAssembleForOtherBusinesses);
            Assert.Equal(responseViewModel.DetailsOfAssemblyForOtherBusinesses, stringsToMatch[31]);
            Assert.Equal(responseViewModel.DetailsOfHowEquipmentCameIntoPossession, stringsToMatch[32]);
            Assert.True(responseViewModel.DeclarationOfCorrectInformation);
            Assert.True(responseViewModel.ConfirmationOfAuthorizedUse);

            Assert.Equal(responseViewModel.SubmittedDate.Value.Date, dto.Date);

            Assert.Equal(responseViewModel.LevelOfEquipmentAutomation , Levelofequipmentautomation.Automated);
            Assert.Equal(responseViewModel.PillpressEncapsulatorSize , Pillpressencapsulatorsize.FreeStandingModel);
            Assert.Equal(responseViewModel.PillpressMaxCapacity , 1000);
            Assert.Equal(responseViewModel.EncapsulatorMaxCapacity, 1000);
            Assert.Equal(responseViewModel.HowWasEquipmentBuilt, Howwasequipmentbuilt.CommerciallyManufactured);


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

        private void CheckAddress (CustomAddress ca, string stringsToMatch1,
            string stringsToMatch2, string stringsToMatch3, string stringsToMatch4,
            string stringsToMatch5, string stringsToMatch6, string stringsToMatch7)
        {
            Assert.NotNull(ca);
            Assert.Equal(ca.StreetLine1, stringsToMatch1);
            Assert.Equal(ca.StreetLine2, stringsToMatch2);
            Assert.Equal(ca.StreetLine3, stringsToMatch3);
            Assert.Equal(ca.City, stringsToMatch4);
            Assert.Equal(ca.Province, stringsToMatch5);
            Assert.Equal(ca.Postalcode, stringsToMatch6);
            Assert.Equal(ca.Country, stringsToMatch7);
        }


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
