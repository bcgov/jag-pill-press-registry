﻿using Gov.Jag.PillPressRegistry.Interfaces.Models;
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
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Convert a given Incident to an Application ViewModel
        /// </summary>        
        public static ViewModels.Application ToViewModel(this MicrosoftDynamicsCRMincident incident)
        {
            ViewModels.Application result = null;
            if (incident != null)
            {
                result = new ViewModels.Application()
                {
                    id = incident.Incidentid,
                    // Equipment Information
                    currentlyownusepossessequipment = incident.BcgovCurrentlyownusepossessequipment,
                    intendtopurchaseequipment = incident.BcgovIntendtopurchaseequipment,
                    ownintendtoownequipmentforbusinessuse = incident.BcgovOwnintendtoownequipmentforbusinessuse,
                    borrowrentleaseequipment = incident.BcgovBorrowrentleaseequipment,
                    sellequipment = incident.BcgovSellequipment,

                    // Purpose of Controlled Equipment
                    producingownproduct = incident.BcgovProducingownproduct,
                    providingmanufacturingtoothers = incident.BcgovProvidingmanufacturingtoothers,

                    // Business Details
                    mainbusinessfocus = incident.BcgovMainbusinessfocus,
                    manufacturingprocessdescription = incident.BcgovManufacturingprocessdescription,

                    // Declarations and Consent
                    DeclarationOfCorrectInformation = incident.BcgovDeclarationofcorrectinformation,
                    foippaconsent = incident.BcgovFoippaconsent,

                    foodanddrugact = incident.BcgovFoodanddrugact,
                    legislativeauthorityother = incident.BcgovLegislativeauthorityother,
                    kindsofproductsdrugs = incident.BcgovKindsofproductsdrugs,
                    kindsofproductsnaturalhealthproducts = incident.BcgovKindsofproductsnaturalhealthproducts,
                    kindsofproductsother = incident.BcgovKindsofproductsother,
                    drugestablishmentlicence = incident.BcgovDrugestablishmentlicence,
                    sitelicence = incident.BcgovSitelicence,
                    otherlicence = incident.BcgovOtherlicence,
                    delbusinessname = incident.BcgovDelbusinessname,
                    drugestablishmentlicencenumber = incident.BcgovDrugestablishmentlicencenumber,
                    drugestablishmentlicenceexpirydate = incident.BcgovDrugestablishmentlicenceexpirydate,
                    sitelicencebusinessname = incident.BcgovSitelicencebusinessname,
                    sitelicencenumber = incident.BcgovSitelicencenumber,
                    sitelicenceexpirydate = incident.BcgovSitelicenceexpirydate,
                    otherlicencebusinessname = incident.BcgovOtherlicencebusinessname,
                    otherlicencenumber = incident.BcgovOtherlicencenumber,
                    otherlicenceexpirydate = incident.BcgovOtherlicenceexpirydate,

                    ownusepossesstoproduceaproduct = incident.BcgovOwnusepossesstoproduceaproduct,
                    intendonrentingleasingtoothers = incident.BcgovIntendonrentingleasingtoothers,
                    intendonsellingequipmenttoothers = incident.BcgovIntendonsellingequipmenttoothers,
                    manufacturerofcontrolledequipment = incident.BcgovManufacturerofcontrolledequipment,
                    retailerofcontrolledequipment = incident.BcgovRetailerofcontrolledequipment,
                    onetimesellerofowncontrolledequipment = incident.BcgovOnetimesellerofowncontrolledequipment,
                    typeofsellerothercheck = incident.BcgovTypeofsellerothercheck,
                    typeofsellerother = incident.BcgovTypeofsellerother,

                    intendtosellpillpress = incident.BcgovIntendtosellpillpress,
                    intendtosellencapsulator = incident.BcgovIntendtosellencapsulator,
                    intendtoselldiemouldorpunch = incident.BcgovIntendtoselldiemouldorpunch,
                    intendtosellpharmaceuticalmixerorblender = incident.BcgovIntendtosellpharmaceuticalmixerorblender,
                    intendtosellothercheck = incident.BcgovIntendtosellothercheck,
                    intendtosellother = incident.BcgovIntendtosellother,
                    legislativeauthorityothercheck = incident.BcgovBcgovLegislativeauthorityothercheck,
                    kindsofproductsothercheck = incident.BcgovKindsofproductothercheck,
                    otherlicencecheck = incident.BcgovOtherlicencecheck,

                    additionalbusinessinformationaboutseller = incident.BcgovAdditionalbusinessinformationaboutseller,
                    statuscode = (ApplicationStatusCodes)incident.Statuscode,

                    title = incident.Title,

                    // EQUIPMENT FIELDS
                    EquipmentType = (Equipmenttype?)incident.BcgovEquipmenttype,
                    EquipmentTypeOther = incident.BcgovEquipmenttypeother,

                    PillpressEncapsulatorSizeOtherCheck = incident.BcgovPillpressencapsulatorsizeothercheck,
                    PillpressEncapsulatorSizeOther = incident.BcgovPillpressencapsulatorsizeother,

                    ExplanationOfEquipmentUse = incident.BcgovExplanationofequipmentuse,

                    HowWasEquipmentBuiltOtherCheck = incident.BcgovHowwasequipmentbuiltothercheck,
                    HowWasEquipmentBuiltOther = incident.BcgovHowwasequipmentbuiltother,
                    NameOfManufacturer = incident.BcgovNameofmanufacturer,
                    EquipmentMake = incident.BcgovEquipmentmake,
                    EquipmentModel = incident.BcgovEquipmentmodel,
                    SerialNumber = incident.BcgovSerialnumber,
                    HowEquipmentBuiltDescription = incident.BcgovHowequipmentbuiltdescription,
                    PersonBusinessThatBuiltEquipment = incident.BcgovPersonbusinessthatbuiltequipment,
                    SerialNumberForCustomBuilt = incident.BcgovSerialnumberforcustombuilt,
                    CustomBuiltSerialNumber = incident.BcgovCustombuiltserialnumber,
                    SerialNumberKeyPartDescription = incident.BcgovSerialnumberkeypartdescription,
                    OwnedBeforeJan2019 = incident.BcgovOwnedbeforejan2019,
                    PurchasedFromBcSeller = incident.BcgovPurchasedfrombcseller,
                    PurchasedFromSellerOutsideofBc = incident.BcgovPurchasedfromselleroutsideofbc,
                    ImportedToBcByAThirdParty = incident.BcgovImportedtobcbyathirdparty,
                    AlternativeOwnershipArrangement = incident.BcgovAlternativeownershiparrangement,
                    IAssembledItMyself = incident.BcgovIassembleditmyself,
                    HowCameIntoPossessionOtherCheck = incident.BcgovHowcameintopossessionothercheck,
                    HowCameIntoPossessionOther = incident.BcgovHowcameintopossessionother,
                    NameOfBcSeller = incident.BcgovNameofbcseller,
                    Dateofpurchasefrombcseller = incident.BcgovDateofpurchasefrombcseller,
                    BcSellersRegistrationNumber = incident.BcgovBcsellersregistrationnumber,
                    BcSellersContactPhoneNumber = incident.BcgovBcsellerscontactphonenumber,
                    BcSellersContactEmail = incident.BcgovBcsellerscontactemail,
                    OutsideBcSellersName = incident.BcgovOutsidebcsellersname,
                    DateOfPurchaseFromOutsideBcSeller = incident.BcgovDateofpurchasefromoutsidebcseller,
                    NameOfImporter = incident.BcgovNameofimporter,
                    ImportersRegistrationNumber = incident.BcgovImportersregistrationnumber,
                    Nameoforiginatingseller = incident.BcgovNameoforiginatingseller,

                    DateOfPurchaseFromImporter = incident.BcgovDateofpurchasefromimporter,
                    PossessUntilICanSell = incident.BcgovPossessuntilicansell,
                    GiveNorLoanedToMe = incident.BcgovGivenorloanedtome,
                    RentingOrLeasingFromAnotherBusiness = incident.BcgovRentingorleasingfromanotherbusiness,
                    KindOfAlternateOwnershipOtherCheck = incident.BcgovKindofalternateownershipothercheck,
                    KindOfAlternateOwnershipOther = incident.BcgovKindofalternateownershipother,
                    UsingToManufactureAProduct = incident.BcgovUsingtomanufactureaproduct,
                    AreYouARegisteredSeller = incident.BcgovAreyouaregisteredseller,
                    NameOfBusinessThatHasGivenOrLoaned = incident.BcgovNameofbusinessthathasgivenorloaned,

                    PhoneOfBusinessThatHasGivenOrLoaned = incident.BcgovPhoneofbusinessthathasgivenorloaned,
                    EmailOfTheBusinessThatHasGivenOrLoaned = incident.BcgovEmailofthebusinessthathasgivenorloaned,
                    WhyAHaveYouAcceptedOrBorrowed = incident.BcgovWhyhaveyouacceptedorborrowed,
                    NameOfBusinessThatHasRentedOrLeased = incident.BcgovNameofbusinessthathasrentedorleased,

                    PhoneOfBusinessThatHasRentedOrLeased = incident.BcgovPhoneofbusinessthathasrentedorleased,
                    EmailOfBusinessThatHasRentedOrLeased = incident.BcgovEmailofbusinessthathasrentedorleased,
                    WhyHaveYouRentedOrLeased = incident.BcgovWhyhaveyourentedorleased,
                    WhenDidYouAssembleEquipment = incident.BcgovWhendidyouassembleequipment,
                    WhereDidYouObtainParts = incident.BcgovWheredidyouobtainparts,
                    DoYouAssembleForOtherBusinesses = incident.BcgovDoyouassembleforotherbusinesses,
                    DetailsOfAssemblyForOtherBusinesses = incident.BcgovDetailsofassemblyforotherbusinesses,
                    DetailsOfHowEquipmentCameIntoPossession = incident.BcgovDetailsofhowequipmentcameintopossession,
                    ConfirmationOfAuthorizedUse = incident.BcgovConfirmationofauthorizeduse,

                    SubmittedDate = incident.BcgovSubmitteddate,

                    LevelOfEquipmentAutomation = (Levelofequipmentautomation?)incident.BcgovLevelofequipmentautomation,
                    PillpressEncapsulatorSize = (Pillpressencapsulatorsize?)incident.BcgovPillpressencapsulatorsize,
                    PillpressMaxCapacity = incident.BcgovPillpressmaxcapacity,
                    EncapsulatorMaxCapacity = incident.BcgovEncapsulatormaxcapacity,
                    HowWasEquipmentBuilt = (Howwasequipmentbuilt?)incident.BcgovHowwasequipmentbuilt,

                    SettingDescription = incident.BcgovSettingdescription,

                    // Lost Stolen and Destroyed fields
                    typeOfChange = (EquipmentChangeType?)incident.BcgovTypeofchange,
                    dateOfEquipmentChange = incident.BcgovDateofequipmentchange,
                    circumstancesOfLoss = incident.BcgovCircumstancesofloss,
                    policeNotified = incident.BcgovPolicenotified,
                    policeReportDate = incident.BcgovPolicereportdate,
                    policeFileNumber = incident.BcgovPolicefilenumber,
                    circumstancesOfStolenEquipment = incident.BcgovCircumstancesofstolenequipment,
                    circumstancesOfDestroyedEquipment = incident.BcgovCircumstancesofdestroyedequipment,
                    whoDestroyedEquipment = incident.BcgovWhodestroyedequipment
                };

                if (incident.BcgovApplicationTypeId != null)
                {
                    result.applicationtype = incident.BcgovApplicationTypeId.BcgovName;
                }

                // CustomerID
                if (incident.CustomeridAccount != null)
                {
                    result.applicant = incident.CustomeridAccount.ToViewModel();
                }

                // Custom Products
                if (incident?.BcgovIncidentCustomproductRelatedApplication?.Count > 0)
                {
                    result.CustomProducts = new List<CustomProduct>();
                    foreach (var product in incident.BcgovIncidentCustomproductRelatedApplication)
                    {
                        result.CustomProducts.Add(product.ToViewModel());
                    }
                }

                // Additional Contacts
                if (incident?.BcgovIncidentBusinesscontact?.Count > 0)
                {
                    result.BusinessContacts = new List<BusinessContact>();
                    foreach (var businessContact in incident.BcgovIncidentBusinesscontact)
                    {
                        result.BusinessContacts.Add(businessContact.ToViewModel());
                    }
                }

                // Certificates
                if (incident?.BcgovIncidentBcgovCertificateApplication?.Count > 0)
                {
                    result.Certificates = new List<Certificate>();
                    foreach (var certificate in incident.BcgovIncidentBcgovCertificateApplication)
                    {
                        result.Certificates.Add(certificate.ToViewModel());
                    }
                }

                ///
                /// EQUIPMENT RELATED ENTITIES
                /// 

                // Addresses of equipment builders - seems to be removed
                // AddressPersonBusinessThatBuiltEquipment = changeme,                

                if (incident?.BcgovBCSellersAddress != null)
                {
                    result.BCSellersAddress = incident.BcgovBCSellersAddress.ToViewModel();
                }

                if (incident?.BcgovImportersAddress != null)
                {
                    result.ImportersAddress = incident.BcgovImportersAddress.ToViewModel();
                }

                if (incident?.BcgovAddressofPersonBusiness != null)
                {
                    result.AddressofPersonBusiness = incident.BcgovAddressofPersonBusiness.ToViewModel();
                }

                if (incident?.BcgovOriginatingSellersAddress != null)
                {
                    result.OriginatingSellersAddress = incident.BcgovOriginatingSellersAddress.ToViewModel();
                }

                if (incident?.BcgovOutsideBCSellersAddress != null)
                {
                    result.OutsideBCSellersAddress = incident.BcgovOutsideBCSellersAddress.ToViewModel();
                }

                if (incident?.BcgovAddressofBusinessthathasGivenorLoaned != null)
                {
                    result.AddressofBusinessthathasGivenorLoaned = incident.BcgovAddressofBusinessthathasGivenorLoaned.ToViewModel();
                }

                if (incident?.BcgovAddressofBusinessthathasRentedorLeased != null)
                {
                    result.AddressofBusinessThatHasRentedorLeased = incident.BcgovAddressofBusinessthathasRentedorLeased.ToViewModel();
                }

                if (incident?.BcgovEquipmentLocation != null)
                {
                    result.EquipmentLocation = incident?.BcgovEquipmentLocation.ToViewModel();
                }

                if (incident?.BcgovEquipmentRecord != null)
                {
                    result.EquipmentRecord = incident?.BcgovEquipmentRecord.ToViewModel();
                }

                if (incident?.BcgovAddressWhereEquipmentWasDestroyed != null)
                {
                    result.addressWhereEquipmentWasDestroyed = incident?.BcgovAddressWhereEquipmentWasDestroyed.ToViewModel();
                }

                result.OutsideBcSellersLocation = (GeographicalLocation?)incident.BcgovOutsidebcsellerslocation;
                result.OriginatingSellersLocation = (GeographicalLocation?)incident.BcgovOriginatingsellerslocation;

            }
            return result;
        }

        public static void CopyValues(this MicrosoftDynamicsCRMincident to, ViewModels.Application from)
        {
            // Equipment Information
            to.BcgovCurrentlyownusepossessequipment = from.currentlyownusepossessequipment;
            to.BcgovIntendtopurchaseequipment = from.intendtopurchaseequipment;
            to.BcgovOwnintendtoownequipmentforbusinessuse = from.ownintendtoownequipmentforbusinessuse;
            to.BcgovBorrowrentleaseequipment = from.borrowrentleaseequipment;
            to.BcgovSellequipment = from.sellequipment;

            // Purchase of Controlled Equipment
            to.BcgovProducingownproduct = from.producingownproduct;
            to.BcgovProvidingmanufacturingtoothers = from.providingmanufacturingtoothers;

            // Business Details
            to.BcgovMainbusinessfocus = from.mainbusinessfocus;
            to.BcgovManufacturingprocessdescription = from.manufacturingprocessdescription;

            to.BcgovFoodanddrugact = from.foodanddrugact;
            to.BcgovLegislativeauthorityother = from.legislativeauthorityother;
            to.BcgovKindsofproductsdrugs = from.kindsofproductsdrugs;
            to.BcgovKindsofproductsnaturalhealthproducts = from.kindsofproductsnaturalhealthproducts;
            to.BcgovKindsofproductsother = from.kindsofproductsother;
            to.BcgovDrugestablishmentlicence = from.drugestablishmentlicence;
            to.BcgovSitelicence = from.sitelicence;
            to.BcgovOtherlicence = from.otherlicence;
            to.BcgovDelbusinessname = from.delbusinessname;
            to.BcgovDrugestablishmentlicencenumber = from.drugestablishmentlicencenumber;
            to.BcgovDrugestablishmentlicenceexpirydate = from.drugestablishmentlicenceexpirydate;
            to.BcgovSitelicencebusinessname = from.sitelicencebusinessname;
            to.BcgovSitelicencenumber = from.sitelicencenumber;
            to.BcgovSitelicenceexpirydate = from.sitelicenceexpirydate;
            to.BcgovOtherlicencebusinessname = from.otherlicencebusinessname;
            to.BcgovOtherlicencenumber = from.otherlicencenumber;
            to.BcgovOtherlicenceexpirydate = from.otherlicenceexpirydate;

            // Declarations and Consent
            to.BcgovDeclarationofcorrectinformation = from.DeclarationOfCorrectInformation;
            to.BcgovFoippaconsent = from.foippaconsent;


            to.BcgovOwnusepossesstoproduceaproduct = from.ownusepossesstoproduceaproduct;
            to.BcgovIntendonrentingleasingtoothers = from.intendonrentingleasingtoothers;
            to.BcgovIntendonsellingequipmenttoothers = from.intendonsellingequipmenttoothers;
            to.BcgovManufacturerofcontrolledequipment = from.manufacturerofcontrolledequipment;
            to.BcgovRetailerofcontrolledequipment = from.retailerofcontrolledequipment;
            to.BcgovOnetimesellerofowncontrolledequipment = from.onetimesellerofowncontrolledequipment;
            to.BcgovTypeofsellerothercheck = from.typeofsellerothercheck;
            to.BcgovTypeofsellerother = from.typeofsellerother;

            to.BcgovIntendtosellpillpress = from.intendtosellpillpress;
            to.BcgovIntendtosellencapsulator = from.intendtosellencapsulator;
            to.BcgovIntendtoselldiemouldorpunch = from.intendtoselldiemouldorpunch;
            to.BcgovIntendtosellpharmaceuticalmixerorblender = from.intendtosellpharmaceuticalmixerorblender;
            to.BcgovIntendtosellothercheck = from.intendtosellothercheck;
            to.BcgovIntendtosellother = from.intendtosellother;
            to.BcgovBcgovLegislativeauthorityothercheck = from.legislativeauthorityothercheck;
            to.BcgovKindsofproductothercheck = from.kindsofproductsothercheck;
            to.BcgovOtherlicencecheck = from.otherlicencecheck;

            to.BcgovAdditionalbusinessinformationaboutseller = from.additionalbusinessinformationaboutseller;

            // EQUIPMENT FIELDS
            to.BcgovEquipmenttype = (int?)from.EquipmentType;
            to.BcgovEquipmenttypeother = from.EquipmentTypeOther;

            to.BcgovPillpressencapsulatorsizeothercheck = from.PillpressEncapsulatorSizeOtherCheck;
            to.BcgovPillpressencapsulatorsizeother = from.PillpressEncapsulatorSizeOther;

            to.BcgovExplanationofequipmentuse = from.ExplanationOfEquipmentUse;

            to.BcgovHowwasequipmentbuiltothercheck = from.HowWasEquipmentBuiltOtherCheck;
            to.BcgovHowwasequipmentbuiltother = from.HowWasEquipmentBuiltOther;
            to.BcgovNameofmanufacturer = from.NameOfManufacturer;
            to.BcgovEquipmentmake = from.EquipmentMake;
            to.BcgovEquipmentmodel = from.EquipmentModel;
            to.BcgovSerialnumber = from.SerialNumber;
            to.BcgovHowequipmentbuiltdescription = from.HowEquipmentBuiltDescription;
            to.BcgovPersonbusinessthatbuiltequipment = from.PersonBusinessThatBuiltEquipment;
            to.BcgovSerialnumberforcustombuilt = from.SerialNumberForCustomBuilt;
            to.BcgovCustombuiltserialnumber = from.CustomBuiltSerialNumber;
            to.BcgovSerialnumberkeypartdescription = from.SerialNumberKeyPartDescription;
            to.BcgovOwnedbeforejan2019 = from.OwnedBeforeJan2019;
            to.BcgovPurchasedfrombcseller = from.PurchasedFromBcSeller;
            to.BcgovPurchasedfromselleroutsideofbc = from.PurchasedFromSellerOutsideofBc;
            to.BcgovImportedtobcbyathirdparty = from.ImportedToBcByAThirdParty;
            to.BcgovAlternativeownershiparrangement = from.AlternativeOwnershipArrangement;
            to.BcgovIassembleditmyself = from.IAssembledItMyself;
            to.BcgovHowcameintopossessionothercheck = from.HowCameIntoPossessionOtherCheck;
            to.BcgovHowcameintopossessionother = from.HowCameIntoPossessionOther;
            to.BcgovNameofbcseller = from.NameOfBcSeller;
            to.BcgovDateofpurchasefrombcseller = from.Dateofpurchasefrombcseller;
            to.BcgovBcsellersregistrationnumber = from.BcSellersRegistrationNumber;
            to.BcgovBcsellerscontactphonenumber = from.BcSellersContactPhoneNumber;
            to.BcgovBcsellerscontactemail = from.BcSellersContactEmail;

            to.BcgovOutsidebcsellersname = from.OutsideBcSellersName;
            to.BcgovDateofpurchasefromoutsidebcseller = from.DateOfPurchaseFromOutsideBcSeller;
            to.BcgovNameofimporter = from.NameOfImporter;
            to.BcgovImportersregistrationnumber = from.ImportersRegistrationNumber;
            to.BcgovNameoforiginatingseller = from.Nameoforiginatingseller;

            to.BcgovDateofpurchasefromimporter = from.DateOfPurchaseFromImporter;
            to.BcgovPossessuntilicansell = from.PossessUntilICanSell;
            to.BcgovGivenorloanedtome = from.GiveNorLoanedToMe;
            to.BcgovRentingorleasingfromanotherbusiness = from.RentingOrLeasingFromAnotherBusiness;
            to.BcgovKindofalternateownershipothercheck = from.KindOfAlternateOwnershipOtherCheck;
            to.BcgovKindofalternateownershipother = from.KindOfAlternateOwnershipOther;
            to.BcgovUsingtomanufactureaproduct = from.UsingToManufactureAProduct;
            to.BcgovAreyouaregisteredseller = from.AreYouARegisteredSeller;
            to.BcgovNameofbusinessthathasgivenorloaned = from.NameOfBusinessThatHasGivenOrLoaned;

            to.BcgovPhoneofbusinessthathasgivenorloaned = from.PhoneOfBusinessThatHasGivenOrLoaned;
            to.BcgovEmailofthebusinessthathasgivenorloaned = from.EmailOfTheBusinessThatHasGivenOrLoaned;
            to.BcgovWhyhaveyouacceptedorborrowed = from.WhyAHaveYouAcceptedOrBorrowed;
            to.BcgovNameofbusinessthathasrentedorleased = from.NameOfBusinessThatHasRentedOrLeased;

            to.BcgovPhoneofbusinessthathasrentedorleased = from.PhoneOfBusinessThatHasRentedOrLeased;
            to.BcgovEmailofbusinessthathasrentedorleased = from.EmailOfBusinessThatHasRentedOrLeased;
            to.BcgovWhyhaveyourentedorleased = from.WhyHaveYouRentedOrLeased;
            to.BcgovWhendidyouassembleequipment = from.WhenDidYouAssembleEquipment;
            to.BcgovWheredidyouobtainparts = from.WhereDidYouObtainParts;
            to.BcgovDoyouassembleforotherbusinesses = from.DoYouAssembleForOtherBusinesses;
            to.BcgovDetailsofassemblyforotherbusinesses = from.DetailsOfAssemblyForOtherBusinesses;
            to.BcgovDetailsofhowequipmentcameintopossession = from.DetailsOfHowEquipmentCameIntoPossession;
            to.BcgovDeclarationofcorrectinformation = from.DeclarationOfCorrectInformation;
            to.BcgovConfirmationofauthorizeduse = from.ConfirmationOfAuthorizedUse;

            to.BcgovSubmitteddate = from.SubmittedDate;

            to.BcgovLevelofequipmentautomation = (int?)from.LevelOfEquipmentAutomation;
            to.BcgovPillpressencapsulatorsize = (int?)from.PillpressEncapsulatorSize;
            to.BcgovPillpressmaxcapacity = from.PillpressMaxCapacity;
            to.BcgovEncapsulatormaxcapacity = from.EncapsulatorMaxCapacity;
            to.BcgovHowwasequipmentbuilt = (int?)from.HowWasEquipmentBuilt;

            to.BcgovSettingdescription = from.SettingDescription;
            to.BcgovOutsidebcsellerslocation = (int?)from.OutsideBcSellersLocation;
            to.BcgovOriginatingsellerslocation = (int?)from.OriginatingSellersLocation;

            to.BcgovTypeofchange = (int?)from.typeOfChange;
            to.BcgovDateofequipmentchange = from.dateOfEquipmentChange;
            to.BcgovCircumstancesofloss = from.circumstancesOfLoss;
            to.BcgovPolicenotified = from.policeNotified;
            to.BcgovPolicereportdate = from.policeReportDate;
            to.BcgovPolicefilenumber = from.policeFileNumber;
            to.BcgovCircumstancesofstolenequipment = from.circumstancesOfStolenEquipment;
            to.BcgovCircumstancesofdestroyedequipment = from.circumstancesOfDestroyedEquipment;
            to.BcgovWhodestroyedequipment = from.whoDestroyedEquipment;
        }
    }
}
