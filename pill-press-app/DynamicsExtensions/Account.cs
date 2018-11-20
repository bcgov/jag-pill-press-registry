
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
                result.Primarycontactid = await system.GetContactById(Guid.Parse(result._primarycontactidValue));
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
                result.Primarycontactid = await system.GetContactById(Guid.Parse(result._primarycontactidValue));
            }

            return result;

        }


        /// <summary>
        /// Get a Account by their Guid
        /// </summary>
        /// <param name="system"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<MicrosoftDynamicsCRMaccount> GetAccountById(this IDynamicsClient system, Guid id)
        {
            List<string> expand = new List<string>()
            {
                "primarycontactid",
                "bcgov_AdditionalContact",
                "bcgov_CurrentBusinessPhysicalAddress",
                "bcgov_CurrentBusinessMailingAddress"
            };

            MicrosoftDynamicsCRMaccount result;
            try
            {
                // fetch from Dynamics.
                result = await system.Accounts.GetByKeyAsync(accountid: id.ToString(), expand: expand);
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
                    result.Primarycontactid = await system.GetContactById(Guid.Parse(result._primarycontactidValue));
                }
                catch (OdataerrorException)
                {
                    result.Primarycontactid = null;
                }
            }
            return result;
        }


    }
}
