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
        public async System.Threading.Tasks.Task TestCRUD()
        {
            string initialName = randomNewUserName("Application Initial Name ", 6);
            string changedName = randomNewUserName("Application Changed Name ", 6);
            string service = "Application";

            string initialPhoneNumber = "3331112222";
            string changedPhoneNumber = "1111112222";

            DateTimeOffset dto = DateTimeOffset.Now;

            // login as default and get account for current user
            var loginUser1 = randomNewUserName("TestAccountUser", 6);
            var strId = await LoginAndRegisterAsNewUser(loginUser1);

            User user = await GetCurrentUser();
            Account currentAccount = await GetAccountForCurrentUser();

            // C - Create
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/" + service + "/Equipment Notification");

            Application viewmodel_application = SecurityHelper.CreateNewApplication(currentAccount);

            string[] stringsToMatch = new string[35];
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

            Assert.Equal(Equipmenttype.DieMouldorPunch, responseViewModel.EquipmentType);
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

            Assert.Equal(Levelofequipmentautomation.Automated, responseViewModel.LevelOfEquipmentAutomation);
            Assert.Equal(Pillpressencapsulatorsize.FreeStandingModel, responseViewModel.PillpressEncapsulatorSize);
            Assert.Equal(1000,responseViewModel.PillpressMaxCapacity);
            Assert.Equal(1000,responseViewModel.EncapsulatorMaxCapacity);
            Assert.Equal(Howwasequipmentbuilt.CommerciallyManufactured, responseViewModel.HowWasEquipmentBuilt);

            // U - Update  
            viewmodel_application = new Application();
            viewmodel_application.mainbusinessfocus = changedName;

            // randomize the strings to match
            for (int i = 0; i < stringsToMatch.Length; i++)
            {
                stringsToMatch[i] = RandomTextString(20, i);
            }
            dto.AddYears(-1);
            viewmodel_application.EquipmentType = Equipmenttype.Encapsulator;
            viewmodel_application.EquipmentTypeOther = stringsToMatch[0];

            viewmodel_application.PillpressEncapsulatorSizeOtherCheck = false;
            viewmodel_application.PillpressEncapsulatorSizeOther = stringsToMatch[1];

            viewmodel_application.ExplanationOfEquipmentUse = stringsToMatch[2];

            viewmodel_application.HowWasEquipmentBuiltOtherCheck = false;
            viewmodel_application.HowWasEquipmentBuiltOther = stringsToMatch[3];
            viewmodel_application.NameOfManufacturer = stringsToMatch[4];
            viewmodel_application.EquipmentMake = stringsToMatch[5];
            viewmodel_application.EquipmentModel = stringsToMatch[6];
            viewmodel_application.SerialNumber = stringsToMatch[7];
            viewmodel_application.HowEquipmentBuiltDescription = stringsToMatch[8];
            viewmodel_application.PersonBusinessThatBuiltEquipment = stringsToMatch[9];
            viewmodel_application.SerialNumberForCustomBuilt = false;
            viewmodel_application.CustomBuiltSerialNumber = stringsToMatch[10];
            viewmodel_application.SerialNumberKeyPartDescription = stringsToMatch[11];
            viewmodel_application.OwnedBeforeJan2019 = false;
            viewmodel_application.PurchasedFromBcSeller = false;
            viewmodel_application.PurchasedFromSellerOutsideofBc = false;
            viewmodel_application.ImportedToBcByAThirdParty = false;
            viewmodel_application.AlternativeOwnershipArrangement = false;
            viewmodel_application.IAssembledItMyself = false;
            viewmodel_application.HowCameIntoPossessionOtherCheck = false;
            viewmodel_application.HowCameIntoPossessionOther = stringsToMatch[12];
            viewmodel_application.NameOfBcSeller = stringsToMatch[13];
            viewmodel_application.Dateofpurchasefrombcseller = dto;
            viewmodel_application.BcSellersRegistrationNumber = stringsToMatch[14];
            viewmodel_application.BcSellersContactPhoneNumber = changedPhoneNumber;
            viewmodel_application.BcSellersContactEmail = stringsToMatch[16];
            viewmodel_application.OutsideBcSellersName = stringsToMatch[17];
            viewmodel_application.DateOfPurchaseFromOutsideBcSeller = dto;
            viewmodel_application.NameOfImporter = stringsToMatch[18];
            viewmodel_application.ImportersRegistrationNumber = stringsToMatch[19];
            viewmodel_application.Nameoforiginatingseller = stringsToMatch[20];

            viewmodel_application.DateOfPurchaseFromImporter = dto;
            viewmodel_application.PossessUntilICanSell = false;
            viewmodel_application.GiveNorLoanedToMe = false;
            viewmodel_application.RentingOrLeasingFromAnotherBusiness = false;
            viewmodel_application.KindOfAlternateOwnershipOtherCheck = false;
            viewmodel_application.KindOfAlternateOwnershipOther = stringsToMatch[21];
            viewmodel_application.UsingToManufactureAProduct = false;
            viewmodel_application.AreYouARegisteredSeller = false;
            viewmodel_application.NameOfBusinessThatHasGivenOrLoaned = stringsToMatch[22];

            viewmodel_application.PhoneOfBusinessThatHasGivenOrLoaned = changedPhoneNumber;
            viewmodel_application.EmailOfTheBusinessThatHasGivenOrLoaned = stringsToMatch[24];
            viewmodel_application.WhyAHaveYouAcceptedOrBorrowed = stringsToMatch[25];
            viewmodel_application.NameOfBusinessThatHasRentedOrLeased = stringsToMatch[26];

            viewmodel_application.PhoneOfBusinessThatHasRentedOrLeased = changedPhoneNumber;
            viewmodel_application.EmailOfBusinessThatHasRentedOrLeased = stringsToMatch[28];
            viewmodel_application.WhyHaveYouRentedOrLeased = stringsToMatch[29];
            viewmodel_application.WhenDidYouAssembleEquipment = dto;
            viewmodel_application.WhereDidYouObtainParts = stringsToMatch[30];
            viewmodel_application.DoYouAssembleForOtherBusinesses = false;
            viewmodel_application.DetailsOfAssemblyForOtherBusinesses = stringsToMatch[31];
            viewmodel_application.DetailsOfHowEquipmentCameIntoPossession = stringsToMatch[32];
            viewmodel_application.DeclarationOfCorrectInformation = false;
            viewmodel_application.ConfirmationOfAuthorizedUse = false;

            viewmodel_application.SubmittedDate = dto;

            viewmodel_application.LevelOfEquipmentAutomation = Levelofequipmentautomation.CapableofBeingAutomated;
            viewmodel_application.PillpressEncapsulatorSize = Pillpressencapsulatorsize.IndustrialModel;
            viewmodel_application.PillpressMaxCapacity = 1;
            viewmodel_application.EncapsulatorMaxCapacity = 1;
            viewmodel_application.HowWasEquipmentBuilt = Howwasequipmentbuilt.Custombuilt;


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

            Assert.Equal(Equipmenttype.Encapsulator, responseViewModel.EquipmentType);
            Assert.Equal(responseViewModel.EquipmentTypeOther, stringsToMatch[0]);


            Assert.False(responseViewModel.PillpressEncapsulatorSizeOtherCheck);
            Assert.Equal(responseViewModel.PillpressEncapsulatorSizeOther, stringsToMatch[1]);

            Assert.Equal(responseViewModel.ExplanationOfEquipmentUse, stringsToMatch[2]);

            Assert.False(responseViewModel.HowWasEquipmentBuiltOtherCheck);
            Assert.Equal(responseViewModel.HowWasEquipmentBuiltOther, stringsToMatch[3]);
            Assert.Equal(responseViewModel.NameOfManufacturer, stringsToMatch[4]);
            Assert.Equal(responseViewModel.EquipmentMake, stringsToMatch[5]);
            Assert.Equal(responseViewModel.EquipmentModel, stringsToMatch[6]);
            Assert.Equal(responseViewModel.SerialNumber, stringsToMatch[7]);
            Assert.Equal(responseViewModel.HowEquipmentBuiltDescription, stringsToMatch[8]);
            Assert.Equal(responseViewModel.PersonBusinessThatBuiltEquipment, stringsToMatch[9]);
            Assert.False(responseViewModel.SerialNumberForCustomBuilt);
            Assert.Equal(responseViewModel.CustomBuiltSerialNumber, stringsToMatch[10]);
            Assert.Equal(responseViewModel.SerialNumberKeyPartDescription, stringsToMatch[11]);
            Assert.False(responseViewModel.OwnedBeforeJan2019);
            Assert.False(responseViewModel.PurchasedFromBcSeller);
            Assert.False(responseViewModel.PurchasedFromSellerOutsideofBc);
            Assert.False(responseViewModel.ImportedToBcByAThirdParty);
            Assert.False(responseViewModel.AlternativeOwnershipArrangement);
            Assert.False(responseViewModel.IAssembledItMyself);
            Assert.False(responseViewModel.HowCameIntoPossessionOtherCheck);
            Assert.Equal(responseViewModel.HowCameIntoPossessionOther, stringsToMatch[12]);
            Assert.Equal(responseViewModel.NameOfBcSeller, stringsToMatch[13]);
            Assert.Equal(responseViewModel.Dateofpurchasefrombcseller.Value.Date, dto.Date);
            Assert.Equal(responseViewModel.BcSellersRegistrationNumber, stringsToMatch[14]);
            Assert.Equal(responseViewModel.BcSellersContactPhoneNumber, changedPhoneNumber);
            Assert.Equal(responseViewModel.BcSellersContactEmail, stringsToMatch[16]);
            Assert.Equal(responseViewModel.OutsideBcSellersName, stringsToMatch[17]);
            Assert.Equal(responseViewModel.DateOfPurchaseFromOutsideBcSeller.Value.Date, dto.Date);
            Assert.Equal(responseViewModel.NameOfImporter, stringsToMatch[18]);
            Assert.Equal(responseViewModel.ImportersRegistrationNumber, stringsToMatch[19]);
            Assert.Equal(responseViewModel.Nameoforiginatingseller, stringsToMatch[20]);

            Assert.Equal(responseViewModel.DateOfPurchaseFromImporter.Value.Date, dto.Date);
            Assert.False(responseViewModel.PossessUntilICanSell);
            Assert.False(responseViewModel.GiveNorLoanedToMe);
            Assert.False(responseViewModel.RentingOrLeasingFromAnotherBusiness);
            Assert.False(responseViewModel.KindOfAlternateOwnershipOtherCheck);
            Assert.Equal(responseViewModel.KindOfAlternateOwnershipOther, stringsToMatch[21]);
            Assert.False(responseViewModel.UsingToManufactureAProduct);
            Assert.False(responseViewModel.AreYouARegisteredSeller);
            Assert.Equal(responseViewModel.NameOfBusinessThatHasGivenOrLoaned, stringsToMatch[22]);

            Assert.Equal(responseViewModel.PhoneOfBusinessThatHasGivenOrLoaned, changedPhoneNumber);
            Assert.Equal(responseViewModel.EmailOfTheBusinessThatHasGivenOrLoaned, stringsToMatch[24]);
            Assert.Equal(responseViewModel.WhyAHaveYouAcceptedOrBorrowed, stringsToMatch[25]);
            Assert.Equal(responseViewModel.NameOfBusinessThatHasRentedOrLeased, stringsToMatch[26]);

            Assert.Equal(responseViewModel.PhoneOfBusinessThatHasRentedOrLeased, changedPhoneNumber);
            Assert.Equal(responseViewModel.EmailOfBusinessThatHasRentedOrLeased, stringsToMatch[28]);
            Assert.Equal(responseViewModel.WhyHaveYouRentedOrLeased, stringsToMatch[29]);
            Assert.Equal(responseViewModel.WhenDidYouAssembleEquipment.Value.Date, dto.Date);
            Assert.Equal(responseViewModel.WhereDidYouObtainParts, stringsToMatch[30]);
            Assert.False(responseViewModel.DoYouAssembleForOtherBusinesses);
            Assert.Equal(responseViewModel.DetailsOfAssemblyForOtherBusinesses, stringsToMatch[31]);
            Assert.Equal(responseViewModel.DetailsOfHowEquipmentCameIntoPossession, stringsToMatch[32]);
            Assert.False(responseViewModel.DeclarationOfCorrectInformation);
            Assert.False(responseViewModel.ConfirmationOfAuthorizedUse);

            Assert.Equal(responseViewModel.SubmittedDate.Value.Date, dto.Date);

            Assert.Equal(Levelofequipmentautomation.CapableofBeingAutomated, responseViewModel.LevelOfEquipmentAutomation);
            Assert.Equal(Pillpressencapsulatorsize.IndustrialModel, responseViewModel.PillpressEncapsulatorSize);
            Assert.Equal(1, responseViewModel.PillpressMaxCapacity);
            Assert.Equal(1, responseViewModel.EncapsulatorMaxCapacity);
            Assert.Equal(Howwasequipmentbuilt.Custombuilt, responseViewModel.HowWasEquipmentBuilt);

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
