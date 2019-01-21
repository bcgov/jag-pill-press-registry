
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class AccountDynamicsExtensions
    {

        /// <summary>
        /// Get a Account by their Guid
        /// </summary>
        /// <param name="system"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<MicrosoftDynamicsCRMaccount> GetAccountBySiteminderBusinessGuid(this IDynamicsClient system, string siteminderId)
        {
            // ensure that the siteminderId does not have any dashes.
            string sanitizedSiteminderId = GuidUtility.SanitizeGuidString(siteminderId);

            MicrosoftDynamicsCRMaccount result = null;
            try
            {
                var accountResponse = await system.Accounts.GetAsync(filter: "bcgov_bceid eq '" + sanitizedSiteminderId + "'");
                result = accountResponse.Value.FirstOrDefault();
            }
            catch (Exception)
            {

                result = null;
            }

            // get the primary contact.
            if (result != null && result.Primarycontactid == null && result._primarycontactidValue != null)
            {
                result.Primarycontactid = system.GetContactById(Guid.Parse(result._primarycontactidValue));
            }

            return result;

        }

        public static async Task<MicrosoftDynamicsCRMaccount> GetAccountByLegalName(this IDynamicsClient system, string legalName)
        {
            legalName = legalName.Replace("'", "''");

            MicrosoftDynamicsCRMaccount result = null;
            try
            {
                var accountResponse = await system.Accounts.GetAsync(filter: $"name eq '{legalName}'");
                result = accountResponse.Value.FirstOrDefault();
            }
            catch (Exception)
            {

                result = null;
            }

            // get the primary contact.
            if (result != null && result.Primarycontactid == null && result._primarycontactidValue != null)
            {
                result.Primarycontactid = system.GetContactById(Guid.Parse(result._primarycontactidValue));
            }

            return result;

        }


        /// <summary>
        /// Get a Account by their Guid
        /// </summary>
        /// <param name="system"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MicrosoftDynamicsCRMaccount GetAccountByIdWithChildren(this IDynamicsClient system, Guid id)
        {
            List<string> expand = new List<string>()
            {
                "primarycontactid",
                "bcgov_AdditionalContact",
                "Account_SharepointDocumentLocation"
            };

            MicrosoftDynamicsCRMaccount result;
            try
            {
                // fetch from Dynamics.
                result = system.Accounts.GetByKey(accountid: id.ToString(), expand: expand);
            }
            catch (OdataerrorException)
            {
                result = null;
            }

            // get the primary contact.
            if (result != null && result.Primarycontactid == null && result._primarycontactidValue != null)
            {
                try
                {
                    result.Primarycontactid = system.GetContactById(Guid.Parse(result._primarycontactidValue));
                }
                catch (OdataerrorException)
                {
                    result.Primarycontactid = null;
                }
            }
            return result;
        }

        public static string GetServerUrl(this MicrosoftDynamicsCRMaccount account, SharePointFileManager _sharePointFileManager)
        {
            string result = "";
            // use the account document location if it exists.
            if (account.AccountSharepointDocumentLocation != null && account.AccountSharepointDocumentLocation.Count > 0)
            {
                var location = account.AccountSharepointDocumentLocation.FirstOrDefault();
                if (location != null)
                {
                    if (string.IsNullOrEmpty(location.Relativeurl))
                    {
                        if (!string.IsNullOrEmpty(location.Absoluteurl))
                        {
                            result = location.Absoluteurl;
                        }
                    }
                    else
                    {
                        result = location.Relativeurl;
                    }
                }                
            }
            if(string.IsNullOrEmpty(result))
            {
                string serverRelativeUrl = "";

                if (!string.IsNullOrEmpty(_sharePointFileManager.WebName))
                {
                    serverRelativeUrl += "/sites/" + _sharePointFileManager.WebName;
                }
                string accountIdCleaned = account.Accountid.ToString().ToUpper().Replace("-", "");
                string folderName = $"_{accountIdCleaned}";

                serverRelativeUrl += "/" + _sharePointFileManager.GetServerRelativeURL(SharePointFileManager.AccountDocumentListTitle, folderName);

                result = serverRelativeUrl;
                
            }
            return result;
        }

    }
}
