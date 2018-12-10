using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.PillPressRegistry.Public.Models
{
    /// <summary>
    /// ViewModel transforms.
    /// </summary>
    public static class EquipmentExtensions
    {
        /// <summary>
        /// Convert a given Incident to an Application ViewModel
        /// </summary>        
        public static ViewModels.Equipment ToViewModel(this MicrosoftDynamicsCRMbcgovEquipment equipment)
        {
            string changeme = "changeme";
            ViewModels.Equipment result = null;
            if (equipment != null)
            {
                result = new ViewModels.Equipment()
                {
                    Id = equipment.BcgovEquipmentid,
                    EquipmentType = changeme,
                    EquipmentTypeOther = changeme,
                    LevelOfEquipmentAutomation = changeme,
                    PillpressEncapsulatorSize = changeme,
                    PillpressEncapsulatorSizeOtherCheck = changeme,
                    PillpressEncapsulatorSizeOther = changeme,
                    PillpressMaxCapacity = changeme,
                    EncapsulatorMaxCapacity = changeme,
                    ExplanationOfEquipmentUse = changeme,
                    HowWasEquipmentBuilt = changeme,
                    HowWasEquipmentBuiltOtherCheck = changeme,
                    HowWasEquipmentBuiltOther = changeme,
                    NameOfManufacturer = changeme,
                    EquipmentMake = changeme,
                    EquipmentModel = changeme,
                    SerialNumber = changeme,
                    HowEquipmentBuiltDescription = changeme,
                    PersonBusinessThatBuiltEquipment = changeme,
                    AddressPersonBusinessThatBuiltEquipment = changeme,
                    SerialNumberForCustomBuilt = changeme,
                    CustomBuiltSerialNumber = changeme,
                    SerialNumberKeyPartDescription = changeme,
                    OwnedBeforeJan2019 = changeme,
                    PurchasedFromBcSeller = changeme,
                    PurchasedFromSellerOutsideofBc = changeme,
                    ImportedToBcByAThirdParty = changeme,
                    alternativeOwnershipArrangement = changeme,
                    IAssembledItMyself = changeme,
                    HowCameIntoPossessionOtherCheck = changeme,
                    HowCameIntoPossessionOther = changeme,
                    NameOfBcSeller = changeme,
                    BCSellersAddress = changeme,
                    BcSellersContactPhoneNumber = changeme,
                    BcSellersContactEmail = changeme,
                    Dateofpurchasefrombcseller = changeme,
                    BcSellersRegistrationNumber = changeme,
                    OutsideBcSellersName = changeme,
                    OutsideBCSellersAddress = changeme,
                    OutsideBcSellersLocation = changeme,
                    DateOfPurchaseFromOutsideBcSeller = changeme,
                    NameOfImporter = changeme,
                    ImportersAddress = changeme,
                    ImportersRegistrationNumber = changeme,
                    nameoforiginatingseller = changeme,
                    OriginatingSellersAddress = changeme,
                    OriginatingSellersLocation = changeme,
                    DateOfPurchaseFromImporter = changeme,
                    PossessUntilICanSell = changeme,
                    GiveNorLoanedToMe = changeme,
                    RentingOrLeasingFromAnotherBusiness = changeme,
                    KindOfAlternateOwnershipOtherCheck = changeme,
                    KindOfAlternateOwnershipOther = changeme,
                    UsingToManufactureAProduct = changeme,
                    AreYouARegisteredSeller = changeme,
                    NameOfBusinessThatHasGivenOrLoaned = changeme,
                    AddressofBusinessthathasGivenorLoaned = changeme,
                    PhoneOfBusinessThatHasGivenOrLoaned = changeme,
                    EmailOfTheBusinessThatHasGivenOrLoaned = changeme,
                    WhyAHaveYouAcceptedOrBorrowed = changeme,
                    NameOfBusinessThatHasRentedOrLeased = changeme,
                    AddressofBusinessThatHasRentedorLeased = changeme,
                    PhoneOfBusinessThatHasRentedOrLeased = changeme,
                    EmailOfBusinessThatHasRentedOrLeased = changeme,
                    WhyHaveYouRentedOrLeased = changeme,
                    WhenDidYouAssembleEquipment = changeme,
                    WhereDidYouObtainParts = changeme,
                    DoYouAssembleForOtherBusinesses = changeme,
                    DetailsOfAssemblyForOtherBusinesses = changeme,
                    DetailsOfHowEquipmentCameIntoPossession = changeme,
                    DeclarationOfCorrectInformation = changeme,
                    ConfirmationOfAuthorizedUse = changeme,
                };

            }
            return result;
        }

        public static void CopyValues(this MicrosoftDynamicsCRMbcgovEquipment to, ViewModels.Equipment from)
        {
            // Equipment Information
            
        }

    }
}
