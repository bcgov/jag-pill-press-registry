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
                result = new ViewModels.Application();
                if (incident.Incidentid != null)
                {
                    result.id = incident.Incidentid;
                }

                // Equipment Information
                result.currentlyownusepossessequipment = incident.BcgovCurrentlyownusepossessequipment;
                result.intendtopurchaseequipment = incident.BcgovIntendtopurchaseequipment;
                result.ownintendtoownequipmentforbusinessuse = incident.BcgovOwnintendtoownequipmentforbusinessuse;
                result.borrowrentleaseequipment = incident.BcgovBorrowrentleaseequipment;
                result.sellequipment = incident.BcgovSellequipment;

                // Purpose of Controlled Equipment
                result.producingownproduct = incident.BcgovProducingownproduct;
                result.providingmanufacturingtoothers = incident.BcgovProvidingmanufacturingtoothers;

                // Business Details
                result.mainbusinessfocus = incident.BcgovMainbusinessfocus;
                result.manufacturingprocessdescription = incident.BcgovManufacturingprocessdescription;

                // Declarations and Consent
                result.declarationofcorrectinformation = incident.BcgovDeclarationofcorrectinformation;
                result.foippaconsent = incident.BcgovFoippaconsent;

                result.foodanddrugact = incident.BcgovFoodanddrugact;
                result.legislativeauthorityother = incident.BcgovLegislativeauthorityother;
                result.kindsofproductsdrugs = incident.BcgovKindsofproductsdrugs;
                result.kindsofproductsnaturalhealthproducts = incident.BcgovKindsofproductsnaturalhealthproducts;
                result.kindsofproductsother = incident.BcgovKindsofproductsother;
                result.drugestablishmentlicence = incident.BcgovDrugestablishmentlicence;
                result.sitelicence = incident.BcgovSitelicence;
                result.otherlicence = incident.BcgovOtherlicence;
                result.delbusinessname = incident.BcgovDelbusinessname;
                result.drugestablishmentlicencenumber = incident.BcgovDrugestablishmentlicencenumber;
                result.drugestablishmentlicenceexpirydate = incident.BcgovDrugestablishmentlicenceexpirydate;
                result.sitelicencebusinessname = incident.BcgovSitelicencebusinessname;
                result.sitelicencenumber = incident.BcgovSitelicencenumber;
                result.sitelicenceexpirydate = incident.BcgovSitelicenceexpirydate;
                result.otherlicencebusinessname = incident.BcgovOtherlicencebusinessname;
                result.otherlicencenumber = incident.BcgovOtherlicencenumber;
                result.otherlicenceexpirydate = incident.BcgovOtherlicenceexpirydate;

                result.ownusepossesstoproduceaproduct = incident.BcgovOwnusepossesstoproduceaproduct;
                result.intendonrentingleasingtoothers = incident.BcgovIntendonrentingleasingtoothers;
                result.intendonsellingequipmenttoothers = incident.BcgovIntendonsellingequipmenttoothers;
                result.manufacturerofcontrolledequipment = incident.BcgovManufacturerofcontrolledequipment;
                result.retailerofcontrolledequipment = incident.BcgovRetailerofcontrolledequipment;
                result.onetimesellerofowncontrolledequipment = incident.BcgovOnetimesellerofowncontrolledequipment;
                // result.typeofsellerothercheck = incident.BcgovTypeofsellerothercheck;
                result.typeofsellerother = incident.BcgovTypeofsellerother;

                result.intendtosellpillpress = incident.BcgovIntendtosellpillpress;
                result.intendtosellencapsulator = incident.BcgovIntendtosellencapsulator;
                result.intendtoselldiemouldorpunch = incident.BcgovIntendtoselldiemouldorpunch;
                result.intendtosellpharmaceuticalmixerorblender = incident.BcgovIntendtosellpharmaceuticalmixerorblender;
                // result.intendtosellothercheck = incident.BcgovIntendtosellother ;
                result.intendtosellother = incident.BcgovIntendtosellother;

                result.additionalbusinessinformationaboutseller = incident.BcgovAdditionalbusinessinformationaboutseller;

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
            to.BcgovDeclarationofcorrectinformation = from.declarationofcorrectinformation;
            to.BcgovFoippaconsent = from.foippaconsent;


            to.BcgovOwnusepossesstoproduceaproduct = from.ownusepossesstoproduceaproduct;
            to.BcgovIntendonrentingleasingtoothers = from.intendonrentingleasingtoothers;
            to.BcgovIntendonsellingequipmenttoothers = from.intendonsellingequipmenttoothers;
            to.BcgovManufacturerofcontrolledequipment = from.manufacturerofcontrolledequipment;
            to.BcgovRetailerofcontrolledequipment = from.retailerofcontrolledequipment;
            to.BcgovOnetimesellerofowncontrolledequipment = from.onetimesellerofowncontrolledequipment;
            // to.BcgovTypeofsellerothercheck = from.typeofsellerothercheck;
            to.BcgovTypeofsellerother = from.typeofsellerother;

            to.BcgovIntendtosellpillpress = from.intendtosellpillpress;
            to.BcgovIntendtosellencapsulator = from.intendtosellencapsulator;
            to.BcgovIntendtoselldiemouldorpunch = from.intendtoselldiemouldorpunch;
            to.BcgovIntendtosellpharmaceuticalmixerorblender = from.intendtosellpharmaceuticalmixerorblender;
            // to.BcgovIntendtosellother  = from.intendtosellothercheck;
            to.BcgovIntendtosellother = from.intendtosellother;

            to.BcgovAdditionalbusinessinformationaboutseller = from.additionalbusinessinformationaboutseller;
        }

    }
}
