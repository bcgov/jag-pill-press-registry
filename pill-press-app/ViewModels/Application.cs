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
    public enum ApplicationStatusCodes
    {
        Draft = 931490005,
        Pending = 931490000,
        [EnumMember(Value = "Under Review")]
        UnderReview = 931490001,
        Incomplete = 931490002,
        [EnumMember(Value = "With Risk Assessment")]
        WithRiskAssessment = 931490007,
        [EnumMember(Value = "With Deputy Registrar")]
        WithDeputyRegistrar = 931490008,
        [EnumMember(Value = "With C&E Investigations")]
        WithCEInvestigations = 931490009,
        Approved = 931490010,
        Hearing = 931490011,
        Denied = 931490012,
        Withdrawn = 931490013,
        Cancelled = 931490014,
        Expired = 931490015
    }

    public enum PublicStatusCodes
    {
        Draft = 931490005,
        Pending = 931490000,
        [EnumMember(Value = "Under Review")]
        UnderReview = 931490001,
        Cancelled = 931490014
    }

    public enum UserApplicationStatusCodes
    {
        Draft = 931490005,
        Pending = 931490000
    }

    public enum AdoxioFinalDecisionCodes
    {
        Approved = 845280000,
        Denied = 845280001
    }

    public enum EquipmentChangeType
    {
        Lost = 931490001,
        Stolen,
        Destroyed,
        Sold
    }
    public enum TypeOfSale
    {
        Sold = 931490000,
        Rented,
        Leased,
        [EnumMember(Value = "Given/Loaned")]
        GivenOrLoaned,
        Other
    }
    public enum MethodOfPayment {
        CreditCard = 931490000,
        DebitCard,
        Cheque,
        Other
    }

    public enum GeographicalLocation
    {
        Canada = 931490000,
        [EnumMember(Value = "United States")]
        UnitedStates = 931490001,

        Europe = 931490002,
        Asia = 931490003,
        [EnumMember(Value = "South America")]
        SouthAmerica = 931490004,
        Africa = 931490005,

    }

    public enum PrivateDwellingOptions
    {
        Yes = 931490000,
        No,
        Unkwown
    }

    public class Application
    {
        public string id { get; set; } //adoxio_applicationid

        public string title { get; set; }

        public DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// The related business
        /// </summary>

        public ViewModels.Account applicant { get; set; }

        // #### EQUIPMENT INFORMATION ####

        /// <summary>
        /// Do you currently own, use, or possess Controlled Equipment?
        /// </summary>

        public bool? currentlyownusepossessequipment { get; set; }

        /// <summary>
        ///  do you intend on purchasing Controlled Equipment in the future?
        /// </summary>
        public bool? intendtopurchaseequipment { get; set; }

        /// <summary>
        /// Do you own or intend to own Controlled Equipment for the sole use of your business?
        /// </summary>
        public bool? ownintendtoownequipmentforbusinessuse { get; set; }

        /// <summary>
        /// Do you borrow, rent, or lease Controlled Equipment from someone else?
        /// </summary>
        public bool? borrowrentleaseequipment { get; set; }

        /// <summary>
        /// Do you sell Controlled Equipment to others?
        /// </summary>
        public bool? sellequipment { get; set; }

        // ### PURPOSE OF CONTROLLED EQUIPMENT ###

        /// <summary>
        /// Do you own, use, or possess (or intend to own) Controlled Equipment for the purposes of producing your own products?
        /// </summary>
        public bool? producingownproduct { get; set; }

        /// <summary>
        /// Do you own, use, or possess (or intend to own) Controlled Equipment for the purposes of providing manufacturing services to others?
        /// </summary>
        public bool? providingmanufacturingtoothers { get; set; }

        /// <summary>
        ///  provide detailed information on the types of products you produce for others and their intended uses.
        /// </summary>

        public List<CustomProduct> CustomProducts { get; set; }

        public List<BusinessContact> BusinessContacts { get; set; }

        // ### BUSINESS DETAILS ###

        /// <summary>
        /// Please explain the main focus of your business and why that requires Controlled Equipment.
        /// </summary>
        public string mainbusinessfocus { get; set; }

        /// <summary>
        /// Please describe the manufacturing process you use to produce the above-noted products. Please include specific information on how you utilize the Controlled Equipment throughout the manufacturing process.
        /// </summary>
        public string manufacturingprocessdescription { get; set; }


        /// <summary>
        /// Consent that by submitting the application the applicant understands their information is being collected for FOIPPA purposes and may be released as per FOIPPA.
        /// </summary>
        public bool? foippaconsent { get; set; }



        public bool? foodanddrugact { get; set; }
        public string legislativeauthorityother { get; set; }
        public bool? kindsofproductsdrugs { get; set; }
        public bool? kindsofproductsnaturalhealthproducts { get; set; }
        public string kindsofproductsother { get; set; }
        public bool? drugestablishmentlicence { get; set; }
        public bool? sitelicence { get; set; }
        public string otherlicence { get; set; }
        public string delbusinessname { get; set; }
        public string drugestablishmentlicencenumber { get; set; }
        public DateTimeOffset? drugestablishmentlicenceexpirydate { get; set; }
        public string sitelicencebusinessname { get; set; }
        public string sitelicencenumber { get; set; }
        public DateTimeOffset? sitelicenceexpirydate { get; set; }
        public string otherlicencebusinessname { get; set; }
        public string otherlicencenumber { get; set; }
        public DateTimeOffset? otherlicenceexpirydate { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApplicationStatusCodes statuscode { get; set; }
        public string applicationtype { get; set; }

        public bool? ownusepossesstoproduceaproduct { get; set; }
        public bool? intendonrentingleasingtoothers { get; set; }
        public bool? intendonsellingequipmenttoothers { get; set; }
        public bool? manufacturerofcontrolledequipment { get; set; }
        public bool? retailerofcontrolledequipment { get; set; }
        public bool? onetimesellerofowncontrolledequipment { get; set; }
        public bool? typeofsellerothercheck { get; set; }
        public string typeofsellerother { get; set; }
        public bool? intendtosellpillpress { get; set; }
        public bool? intendtosellencapsulator { get; set; }
        public bool? intendtoselldiemouldorpunch { get; set; }
        public bool? intendtosellpharmaceuticalmixerorblender { get; set; }
        public bool? intendtosellothercheck { get; set; }
        public string intendtosellother { get; set; }
        public string additionalbusinessinformationaboutseller { get; set; }
        public bool? legislativeauthorityothercheck { get; set; }
        public bool? kindsofproductsothercheck { get; set; }
        public bool? otherlicencecheck { get; set; }

        // EQUIPMENT FIELDS

        [JsonProperty(PropertyName = "equipmentType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Equipmenttype? EquipmentType { get; set; }

        [JsonProperty(PropertyName = "equipmentTypeOther")]
        public string EquipmentTypeOther { get; set; }

        [JsonProperty(PropertyName = "levelOfEquipmentAutomation")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Levelofequipmentautomation? LevelOfEquipmentAutomation { get; set; }

        [JsonProperty(PropertyName = "pillpressEncapsulatorsize")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Pillpressencapsulatorsize? PillpressEncapsulatorSize { get; set; }

        [JsonProperty(PropertyName = "pillpressEncapsulatorSizeOtherCheck")]
        public bool? PillpressEncapsulatorSizeOtherCheck { get; set; }

        [JsonProperty(PropertyName = "pillpressEncapsulatorSizeOther")]
        public string PillpressEncapsulatorSizeOther { get; set; }

        [JsonProperty(PropertyName = "pillpressmaxcapacity")]
        public int? PillpressMaxCapacity { get; set; }

        [JsonProperty(PropertyName = "encapsulatorMaxCapacity")]
        public int? EncapsulatorMaxCapacity { get; set; }

        [JsonProperty(PropertyName = "explanationOfEquipmentuse")]
        public string ExplanationOfEquipmentUse { get; set; }

        [JsonProperty(PropertyName = "howWasEquipmentBuilt")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Howwasequipmentbuilt? HowWasEquipmentBuilt { get; set; }

        [JsonProperty(PropertyName = "howWasEquipmentBuiltOtherCheck")]
        public bool? HowWasEquipmentBuiltOtherCheck { get; set; }

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
        public bool? SerialNumberForCustomBuilt { get; set; }

        [JsonProperty(PropertyName = "customBuiltSerialNumber")]
        public string CustomBuiltSerialNumber { get; set; }

        [JsonProperty(PropertyName = "serialNumberKeyPartDescription")]
        public string SerialNumberKeyPartDescription { get; set; }

        [JsonProperty(PropertyName = "ownedBeforeJan2019")]
        public bool? OwnedBeforeJan2019 { get; set; }

        [JsonProperty(PropertyName = "purchasedFromBcSeller")]
        public bool? PurchasedFromBcSeller { get; set; }

        [JsonProperty(PropertyName = "purchasedFromSellerOutsideOfBc")]
        public bool? PurchasedFromSellerOutsideofBc { get; set; }

        [JsonProperty(PropertyName = "importedToBcByAThirdParty")]
        public bool? ImportedToBcByAThirdParty { get; set; }

        [JsonProperty(PropertyName = "alternativeOwnershipArrangement")]
        public bool? AlternativeOwnershipArrangement { get; set; }

        [JsonProperty(PropertyName = "iAssembledItMyself")]
        public bool? IAssembledItMyself { get; set; }

        [JsonProperty(PropertyName = "howCameIntoPossessionOtherCheck")]
        public bool? HowCameIntoPossessionOtherCheck { get; set; }

        [JsonProperty(PropertyName = "howCameIntoPossessionOther")]
        public string HowCameIntoPossessionOther { get; set; }

        [JsonProperty(PropertyName = "nameOfBcSeller")]
        public string NameOfBcSeller { get; set; }

        [JsonProperty(PropertyName = "bcSellersAddress")]
        public ViewModels.CustomAddress BCSellersAddress { get; set; }

        [JsonProperty(PropertyName = "bcSellersContactPhoneNumber")]
        public string BcSellersContactPhoneNumber { get; set; }

        [JsonProperty(PropertyName = "bcSellersContactEmail")]
        public string BcSellersContactEmail { get; set; }

        [JsonProperty(PropertyName = "dateOfPurchaseFromBcSeller")]
        public DateTimeOffset? Dateofpurchasefrombcseller { get; set; }

        [JsonProperty(PropertyName = "bcSellersRegistrationNumber")]
        public string BcSellersRegistrationNumber { get; set; }

        [JsonProperty(PropertyName = "outsideBcSellersName")]
        public string OutsideBcSellersName { get; set; }

        [JsonProperty(PropertyName = "outsideBcSellersAddress")]
        public ViewModels.CustomAddress OutsideBCSellersAddress { get; set; }

        [JsonProperty(PropertyName = "outsideBcSellersLocation")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GeographicalLocation? OutsideBcSellersLocation { get; set; }

        [JsonProperty(PropertyName = "dateOfPurchaseFromOutsideBcSeller")]
        public DateTimeOffset? DateOfPurchaseFromOutsideBcSeller { get; set; }

        [JsonProperty(PropertyName = "nameOfImporter")]
        public string NameOfImporter { get; set; }

        [JsonProperty(PropertyName = "importersAddress")]
        public ViewModels.CustomAddress ImportersAddress { get; set; }

        [JsonProperty(PropertyName = "addressofPersonBusiness")]
        public ViewModels.CustomAddress AddressofPersonBusiness { get; set; }

        [JsonProperty(PropertyName = "importersRegistrationNumber")]
        public string ImportersRegistrationNumber { get; set; }

        [JsonProperty(PropertyName = "nameOfOriginatingSeller")]
        public string Nameoforiginatingseller { get; set; }

        [JsonProperty(PropertyName = "OriginatingSellersAddress")]
        public ViewModels.CustomAddress OriginatingSellersAddress { get; set; }

        [JsonProperty(PropertyName = "originatingSellersLocation")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GeographicalLocation? OriginatingSellersLocation { get; set; }

        [JsonProperty(PropertyName = "dateOfPurchaseFromImporter")]
        public DateTimeOffset? DateOfPurchaseFromImporter { get; set; }

        [JsonProperty(PropertyName = "possessUntilICanSell")]
        public bool? PossessUntilICanSell { get; set; }

        [JsonProperty(PropertyName = "giveNorLoanedToMe")]
        public bool? GiveNorLoanedToMe { get; set; }

        [JsonProperty(PropertyName = "rentingOrLeasingFromAnotherBusiness")]
        public bool? RentingOrLeasingFromAnotherBusiness { get; set; }

        [JsonProperty(PropertyName = "kindOfAlternateOwnershipOtherCheck")]
        public bool? KindOfAlternateOwnershipOtherCheck { get; set; }

        [JsonProperty(PropertyName = "kindOfAlternateOwnershipOther")]
        public string KindOfAlternateOwnershipOther { get; set; }

        [JsonProperty(PropertyName = "usingToManufactureAProduct")]
        public bool? UsingToManufactureAProduct { get; set; }

        [JsonProperty(PropertyName = "areYouARegisteredSeller")]
        public bool? AreYouARegisteredSeller { get; set; }

        [JsonProperty(PropertyName = "nameOfBusinessThatHasGivenOrLoaned")]
        public string NameOfBusinessThatHasGivenOrLoaned { get; set; }

        [JsonProperty(PropertyName = "addressofBusinessthathasGivenorLoaned")]
        public ViewModels.CustomAddress AddressofBusinessthathasGivenorLoaned { get; set; }

        [JsonProperty(PropertyName = "phoneofbusinessthathasgivenorloaned")]
        public string PhoneOfBusinessThatHasGivenOrLoaned { get; set; }

        [JsonProperty(PropertyName = "emailOfTheBusinessThatHasGivenOrLoaned")]
        public string EmailOfTheBusinessThatHasGivenOrLoaned { get; set; }

        [JsonProperty(PropertyName = "whyHaveYouAcceptedOrBorrowed")]
        public string WhyAHaveYouAcceptedOrBorrowed { get; set; }

        [JsonProperty(PropertyName = "NameOfBusinessThatHasRentedOrLeased")]
        public string NameOfBusinessThatHasRentedOrLeased { get; set; }

        [JsonProperty(PropertyName = "addressOfBusinessThatHasRentedorLeased")]
        public ViewModels.CustomAddress AddressofBusinessThatHasRentedorLeased { get; set; }

        [JsonProperty(PropertyName = "phoneOfBusinessThatHasRentedOrLeased")]
        public string PhoneOfBusinessThatHasRentedOrLeased { get; set; }

        [JsonProperty(PropertyName = "emailOfBusinessThatHasRentedOrLeased")]
        public string EmailOfBusinessThatHasRentedOrLeased { get; set; }

        [JsonProperty(PropertyName = "whyHaveYouRentedOrLeased")]
        public string WhyHaveYouRentedOrLeased { get; set; }

        [JsonProperty(PropertyName = "whenDidYouAssembleEquipment")]
        public DateTimeOffset? WhenDidYouAssembleEquipment { get; set; }

        [JsonProperty(PropertyName = "whereDidYouObtainParts")]
        public string WhereDidYouObtainParts { get; set; }

        [JsonProperty(PropertyName = "doYouAssembleForOtherBusinesses")]
        public bool? DoYouAssembleForOtherBusinesses { get; set; }

        [JsonProperty(PropertyName = "detailsOfAssemblyForOtherBusinesses")]
        public string DetailsOfAssemblyForOtherBusinesses { get; set; }

        [JsonProperty(PropertyName = "detailsOfHowEquipmentCameIntoPossession")]
        public string DetailsOfHowEquipmentCameIntoPossession { get; set; }

        [JsonProperty(PropertyName = "declarationOfCorrectInformation")]
        public bool? DeclarationOfCorrectInformation { get; set; }

        [JsonProperty(PropertyName = "confirmationOfAuthorizedUse")]
        public bool? ConfirmationOfAuthorizedUse { get; set; }

        List<Contact> OwnersAndManagers { get; set; }

        [JsonProperty(PropertyName = "submittedDate")]
        public DateTimeOffset? SubmittedDate { get; set; }

        [JsonProperty(PropertyName = "equipmentLocation")]
        public Location EquipmentLocation { get; set; }

        [JsonProperty(PropertyName = "settingDescription")]
        public string SettingDescription { get; set; }

        public List<Certificate> Certificates { get; set; }

        // Lost Stolen and Destroyed
        [JsonConverter(typeof(StringEnumConverter))]
        public EquipmentChangeType? typeOfChange { get; set; }
        public DateTimeOffset? dateOfEquipmentChange { get; set; }
        public string circumstancesOfLoss { get; set; }
        public bool? policeNotified { get; set; }
        public DateTimeOffset? policeReportDate { get; set; }
        public string policeFileNumber { get; set; }
        public string circumstancesOfStolenEquipment { get; set; }
        public string circumstancesOfDestroyedEquipment { get; set; }
        public string whoDestroyedEquipment { get; set; }
        public ViewModels.CustomAddress AddressWhereEquipmentWasDestroyed { get; set; }


        // reporting sales
        public DateTimeOffset? dateOfSale { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TypeOfSale? typeOfSale { get; set; }
        public string typeOfSaleOther { get; set; }
        public bool? rightsToOwnuseOrPossessRetained { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MethodOfPayment? methodOfPayment { get; set; }
        public string methodOfPaymentOther { get; set; }
        public bool? whereWillEquipmentReside { get; set; }
        public CustomAddress civicAddressOfPurchaser { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PrivateDwellingOptions? privateDwelling { get; set; }
        public bool? purchasedByIndividualOrBusiness { get; set; }
        public string legalNameOfPurchaserIndividual { get; set; }
        public CustomAddress purchasersCivicAddress { get; set; }
        public string purchasersTelephoneNumber { get; set; }
        public string purchasersEmailAddress { get; set; }
        public bool? idNumberCollected { get; set; }
        public string typeOfIdNumberCollected { get; set; }
        public string nameOfPurchaserBusiness { get; set; }
        public string purchaserRegistrationNumber { get; set; }
        public string purchaserdBaName { get; set; }
        public CustomAddress purchasersBusinessAddress { get; set; }
        public string legalNameOfPersonResponsibleForBusiness { get; set; }
        public string phoneNumberOfPersonResponsibleForBusiness { get; set; }
        public string emailOfPersonResponsibleForBusiness { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GeographicalLocation? geographicalLocationOfBusinessPurchaser { get; set; }
        public bool? isPurchaserAPersonOfBC { get; set; }
        public bool? howIsPurchaseAuthorizedAO { get; set; }
        public bool? howIsPurchaserAuthorizedWaiver { get; set; }
        public bool? howIsPurchaserAuthorizedRegisteredSeller { get; set; }
        public string howIsPurchaserAuthorizedOther { get; set; }
        public bool? healthCanadaLicenseDEL { get; set; }
        public bool? healthCanadaLicenseSiteLicense { get; set; }
        public string nameOnPurchasersDEL { get; set; }
        public string purchasersDELNumber { get; set; }
        public string PurchasersSiteLicenseNumber { get; set; }
        public string nameOnPurchasersSiteLicense { get; set; }
        public DateTimeOffset? PurchasersSiteLicenseExpiryDate { get; set; }
        public DateTimeOffset? PurchasersDELExpiryDate { get; set; }
        public string purchasersWaiverNumber { get; set; }
        public string purchasersRegistrationNumber { get; set; }
        public Equipment EquipmentRecord { get; set; }
    }
}
