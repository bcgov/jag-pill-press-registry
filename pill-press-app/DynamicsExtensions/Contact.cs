
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class ContactDynamicsExtensions
    {


        public static MicrosoftDynamicsCRMcontact GetContactById(this IDynamicsClient system, Guid id)
        {
            MicrosoftDynamicsCRMcontact result;
            try
            {
                // fetch from Dynamics.
                result =  system.Contacts.GetByKey(id.ToString());
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


        public static MicrosoftDynamicsCRMcontact GetContactByName(this IDynamicsClient system, string firstname, string lastname)
        {
            firstname = firstname.Replace("'", "''");
            lastname = lastname.Replace("'", "''");
            MicrosoftDynamicsCRMcontact result = null;
            var contactsResponse = system.Contacts.Get(filter: $"firstname eq '{firstname}' and lastname eq '{lastname}'");
            result = contactsResponse.Value.FirstOrDefault();
            return result;
        }

    }
}
