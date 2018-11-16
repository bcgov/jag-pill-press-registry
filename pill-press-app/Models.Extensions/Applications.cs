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

                // CustomerID

                if (incident.CustomeridAccount != null)
                {
                    result.applicant = incident.CustomeridAccount.ToViewModel();
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

            // Declarations and Consent
            to.BcgovDeclarationofcorrectinformation = from.declarationofcorrectinformation;
            to.BcgovFoippaconsent = from.foippaconsent;
    }

    }
}
