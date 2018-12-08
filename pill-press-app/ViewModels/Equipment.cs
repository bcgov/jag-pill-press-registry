using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Public.ViewModels
{
    public class Equipment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "equipmentType")]
        public string EquipmentType { get; set; }

        [JsonProperty(PropertyName = "equipmentTypeOther")]
        public string EquipmentTypeOther { get; set; }

        [JsonProperty(PropertyName = "levelOfEquipmentAutomation")]
        public string LevelOfEquipmentAutomation { get; set; }

        [JsonProperty(PropertyName = "pillpressEncapsulatorsize")]
        public string PillpressEncapsulatorSize { get; set; }

        [JsonProperty(PropertyName = "pillpressEncapsulatorSizeOtherCheck")]
        public string PillpressEncapsulatorSizeOtherCheck { get; set; }

        [JsonProperty(PropertyName = "pillpressEncapsulatorSizeOther")]
        public string PillpressEncapsulatorSizeOther { get; set; }

        [JsonProperty(PropertyName = "pillpressmaxcapacity")]
        public string PillpressMaxCapacity { get; set; }

        [JsonProperty(PropertyName = "encapsulatorMaxCapacity")]
        public string EncapsulatorMaxCapacity { get; set; }

        [JsonProperty(PropertyName = "explanationOfEquipmentuse")]
        public string ExplanationOfEquipmentUse { get; set; }

        [JsonProperty(PropertyName = "howWasEquipmentBuilt")]
        public string HowWasEquipmentBuilt { get; set; }

        [JsonProperty(PropertyName = "howWasEquipmentBuiltOtherCheck")]
        public string HowWasEquipmentBuiltOtherCheck { get; set; }

        [JsonProperty(PropertyName = "howWasEquipmentBuiltOther")]
        public string HowWasEquipmentBuiltOther { get; set; }

        [JsonProperty(PropertyName = "nameOfManufacturer")]
        public string NameOfManufacturer { get; set; }

        [JsonProperty(PropertyName = "equipmentMake")]
        public string EquipmentMake { get; set; }

        [JsonProperty(PropertyName = "equipmentModel")]
        public string EquipmentModel { get; set; }

        [JsonProperty(PropertyName = "serialNumber")]
        public string SerialNumber { get; set; }

        [JsonProperty(PropertyName = "howEquipmentBuiltDescription")]
        public string HowEquipmentBuiltDescription { get; set; }

        [JsonProperty(PropertyName = "personBusinessThatBuiltEquipment")]
        public string PersonBusinessThatBuiltEquipment { get; set; }

        [JsonProperty(PropertyName = "addressPersonBusinessThatBuiltEquipment")]
        public string AddressPersonBusinessThatBuiltEquipment { get; set; }

        [JsonProperty(PropertyName = "serialNumberForCustomBuilt")]
        public string SerialNumberForCustomBuilt { get; set; }

        [JsonProperty(PropertyName = "customBuiltSerialNumber")]
        public string CustomBuiltSerialNumber { get; set; }

        [JsonProperty(PropertyName = "serialNumberKeyPartDescription")]
        public string SerialNumberKeyPartDescription { get; set; }

        [JsonProperty(PropertyName = "ownedBeforeJan2019")]
        public string OwnedBeforeJan2019 { get; set; }

        [JsonProperty(PropertyName = "purchasedFromBcSeller")]
        public string PurchasedFromBcSeller { get; set; }

        [JsonProperty(PropertyName = "purchasedFromSellerOutsideOfBc")]
        public string PurchasedFromSellerOutsideofBc { get; set; }

        [JsonProperty(PropertyName = "importedToBcByAThirdParty")]
        public string ImportedToBcByAThirdParty { get; set; }

        [JsonProperty(PropertyName = "alternativeOwnershipArrangement")]
        public string alternativeOwnershipArrangement { get; set; }

        [JsonProperty(PropertyName = "iAssembledItMyself")]
        public string IAssembledItMyself { get; set; }

        [JsonProperty(PropertyName = "howCameIntoPossessionOtherCheck")]
        public string HowCameIntoPossessionOtherCheck { get; set; }

        [JsonProperty(PropertyName = "howCameIntoPossessionOther")]
        public string HowCameIntoPossessionOther { get; set; }

        [JsonProperty(PropertyName = "nameOfBcSeller")]
        public string NameOfBcSeller { get; set; }

        [JsonProperty(PropertyName = "bcSellersAddress")]
        public string BCSellersAddress { get; set; }

        [JsonProperty(PropertyName = "bcSellersContactPhoneNumber")]
        public string BcSellersContactPhoneNumber { get; set; }

        [JsonProperty(PropertyName = "bcSellersContactEmail")]
        public string BcSellersContactEmail { get; set; }

        [JsonProperty(PropertyName = "dateOfPurchaseFromBcSeller")]
        public string Dateofpurchasefrombcseller { get; set; }

        [JsonProperty(PropertyName = "bcSellersRegistrationNumber")]
        public string BcSellersRegistrationNumber { get; set; }

        [JsonProperty(PropertyName = "outsideBcSellersName")]
        public string OutsideBcSellersName { get; set; }

        [JsonProperty(PropertyName = "outsideBcSellersAddress")]
        public string OutsideBCSellersAddress { get; set; }

        [JsonProperty(PropertyName = "outsideBcSellersLocation")]
        public string OutsideBcSellersLocation { get; set; }

        [JsonProperty(PropertyName = "dateOfPurchaseFromOutsideBcSeller")]
        public string DateOfPurchaseFromOutsideBcSeller { get; set; }

        [JsonProperty(PropertyName = "nameOfImporter")]
        public string NameOfImporter { get; set; }

        [JsonProperty(PropertyName = "importersAddress")]
        public string ImportersAddress { get; set; }

        [JsonProperty(PropertyName = "importersRegistrationNumber")]
        public string ImportersRegistrationNumber { get; set; }

        [JsonProperty(PropertyName = "nameOfOriginatingSeller")]
        public string nameoforiginatingseller { get; set; }

        [JsonProperty(PropertyName = "OriginatingSellersAddress")]
        public string OriginatingSellersAddress { get; set; }

        [JsonProperty(PropertyName = "originatingSellersLocation")]
        public string OriginatingSellersLocation { get; set; }

        [JsonProperty(PropertyName = "dateOfPurchaseFromImporter")]
        public string DateOfPurchaseFromImporter { get; set; }

        [JsonProperty(PropertyName = "possessUntilICanSell")]
        public string PossessUntilICanSell { get; set; }

        [JsonProperty(PropertyName = "giveNorLoanedToMe")]
        public string GiveNorLoanedToMe { get; set; }

        [JsonProperty(PropertyName = "rentingOrLeasingFromAnotherBusiness")]
        public string RentingOrLeasingFromAnotherBusiness { get; set; }

        [JsonProperty(PropertyName = "kindOfAlternateOwnershipOtherCheck")]
        public string KindOfAlternateOwnershipOtherCheck { get; set; }

        [JsonProperty(PropertyName = "kindOfAlternateOwnershipOther")]
        public string KindOfAlternateOwnershipOther { get; set; }

        [JsonProperty(PropertyName = "usingToManufactureAProduct")]
        public string UsingToManufactureAProduct { get; set; }

        [JsonProperty(PropertyName = "areYouARegisteredSeller")]
        public string AreYouARegisteredSeller { get; set; }

        [JsonProperty(PropertyName = "nameOfBusinessThatHasGivenOrLoaned")]
        public string NameOfBusinessThatHasGivenOrLoaned { get; set; }

        [JsonProperty(PropertyName = "addressofBusinessthathasGivenorLoaned")]
        public string AddressofBusinessthathasGivenorLoaned { get; set; }

        [JsonProperty(PropertyName = "phoneofbusinessthathasgivenorloaned")]
        public string PhoneOfBusinessThatHasGivenOrLoaned { get; set; }

        [JsonProperty(PropertyName = "emailOfTheBusinessThatHasGivenOrLoaned")]
        public string EmailOfTheBusinessThatHasGivenOrLoaned { get; set; }

        [JsonProperty(PropertyName = "whyHaveYouAcceptedOrBorrowed")]
        public string WhyAHaveYouAcceptedOrBorrowed { get; set; }

        [JsonProperty(PropertyName = "NameOfBusinessThatHasRentedOrLeased")]
        public string NameOfBusinessThatHasRentedOrLeased { get; set; }

        [JsonProperty(PropertyName = "addressOfBusinessThatHasRentedorLeased")]
        public string AddressofBusinessThatHasRentedorLeased { get; set; }

        [JsonProperty(PropertyName = "phoneOfBusinessThatHasRentedOrLeased")]
        public string PhoneOfBusinessThatHasRentedOrLeased { get; set; }

        [JsonProperty(PropertyName = "emailOfBusinessThatHasRentedOrLeased")]
        public string EmailOfBusinessThatHasRentedOrLeased { get; set; }

        [JsonProperty(PropertyName = "whyHaveYouRentedOrLeased")]
        public string WhyHaveYouRentedOrLeased { get; set; }

        [JsonProperty(PropertyName = "whenDidYouAssembleEquipment")]
        public string WhenDidYouAssembleEquipment { get; set; }

        [JsonProperty(PropertyName = "whereDidYouObtainParts")]
        public string WhereDidYouObtainParts { get; set; }

        [JsonProperty(PropertyName = "doYouAssembleForOtherBusinesses")]
        public string DoYouAssembleForOtherBusinesses { get; set; }

        [JsonProperty(PropertyName = "detailsOfAssemblyForOtherBusinesses")]
        public string DetailsOfAssemblyForOtherBusinesses { get; set; }

        [JsonProperty(PropertyName = "detailsOfHowEquipmentCameIntoPossession")]
        public string DetailsOfHowEquipmentCameIntoPossession { get; set; }

        [JsonProperty(PropertyName = "declarationOfCorrectInformation")]
        public string DeclarationOfCorrectInformation { get; set; }

        [JsonProperty(PropertyName = "confirmationOfAuthorizedUse")]
        public string ConfirmationOfAuthorizedUse { get; set; }

        public string incidentId { get; set; }
    }
}
