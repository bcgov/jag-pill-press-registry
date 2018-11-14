
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class ContactDynamicsExtensions
    {


        public static async Task<MicrosoftDynamicsCRMcontact> GetContactById(this IDynamicsClient system, Guid id)
        {
            MicrosoftDynamicsCRMcontact result;
            try
            {
                // fetch from Dynamics.
                result = await system.Contacts.GetByKeyAsync(id.ToString());
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Get a contact by their Siteminder ID
        /// </summary>
        /// <param name="system"></param>
        /// <param name="siteminderId"></param>
        /// <returns></returns>
        public static MicrosoftDynamicsCRMcontact GetContactByExternalId(this IDynamicsClient system, string siteminderId)
        {
            string sanitizedSiteminderId = GuidUtility.SanitizeGuidString(siteminderId);
            MicrosoftDynamicsCRMcontact result = null;
            var contactsResponse = system.Contacts.Get(filter: "externaluseridentifier eq '" + sanitizedSiteminderId + "'");
            result = contactsResponse.Value.FirstOrDefault();
            return result;
        }

    }
}
