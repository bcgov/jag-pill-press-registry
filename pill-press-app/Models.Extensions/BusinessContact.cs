using Gov.Jag.PillPressRegistry.Interfaces;
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
    public static class BusinessContactExtensions
    {
        /// <summary>
        /// Convert a given BusinessContact to a ViewModel
        /// </summary>        
        public static ViewModels.BusinessContact ToViewModel(this MicrosoftDynamicsCRMbcgovBusinesscontact businessContact)
        {
            ViewModels.BusinessContact result = null;
            if (businessContact != null)
            {
                result = new ViewModels.BusinessContact();
                if (businessContact.BcgovBusinesscontactid != null)
                {
                    result.id = businessContact.BcgovBusinesscontactid;
                }
                result.jobTitle = businessContact.BcgovJobtitle;

                if (businessContact.BcgovContacttype != null)
                {
                    result.contactType = (ContactTypeCodes) businessContact.BcgovContacttype;
                }

                if (businessContact.BcgovRegisteredsellerownermanager != null)
                {
                    result.registeredSellerOwnerManager = (OwnerManagerCodes)businessContact.BcgovRegisteredsellerownermanager;
                }

                if (businessContact.BcgovContact != null)
                {
                    result.contact = businessContact.BcgovContact.ToViewModel();
                }

                if (businessContact.BcgovBusinessProfile != null)
                {
                    result.account = businessContact.BcgovBusinessProfile.ToViewModel();
                }

            }
            return result;
        }

        public static void CopyValues(this MicrosoftDynamicsCRMbcgovBusinesscontact to, ViewModels.BusinessContact from)
        {
            to.BcgovJobtitle = from.jobTitle;
            to.BcgovContacttype = (int?) from.contactType;
            to.BcgovRegisteredsellerownermanager = (int?)from.registeredSellerOwnerManager;
                        
        }


        public static MicrosoftDynamicsCRMbcgovBusinesscontact ToModel(this ViewModels.BusinessContact from, IDynamicsClient system)
        {
            MicrosoftDynamicsCRMbcgovBusinesscontact result = new MicrosoftDynamicsCRMbcgovBusinesscontact()
            {

                BcgovContacttype = (int?)from.contactType,
                ContactODataBind = system.GetEntityURI("contacts", from.contact.id),
                AccountODataBind = system.GetEntityURI("accounts", from.account.id),
                BcgovJobtitle = from.jobTitle,
                BcgovRegisteredsellerownermanager = (int?)from.registeredSellerOwnerManager
            };
            if (!string.IsNullOrEmpty(from.id))
            {
                result.BcgovBusinesscontactid = from.id;
            } 
            
            return result;
        }
        
    }
}
