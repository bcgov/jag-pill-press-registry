
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class ApplicationTypeDynamicsExtensions
    {
        
        public static string GetApplicationTypeIdByName(this IDynamicsClient system, string name)
        {
            string result = null;
            name = name.Replace("'", "''");

            MicrosoftDynamicsCRMbcgovApplicationtype applicationType = null;

            try
            {
                var accountResponse = system.Applicationtypes.Get(filter: $"bcgov_name eq '{name}'");
                applicationType = accountResponse.Value.FirstOrDefault();
            }
            catch (Exception)
            {
                applicationType = null;
            }

            // get the primary contact.
            if (applicationType != null)
            {
                result = applicationType.BcgovApplicationtypeid;
            }

            return result;
        }


    }
}
