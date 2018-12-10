
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class ApplicationDynamicsExtensions
    {
        
        public static MicrosoftDynamicsCRMincident GetApplicationById(this IDynamicsClient system, Guid id)
        {
            MicrosoftDynamicsCRMincident result;
            try
            {
                // fetch from Dynamics.
                result = system.Incidents.GetByKey(id.ToString());
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            return result;
        }

        public static MicrosoftDynamicsCRMincident GetApplicationByIdWithChildren(this IDynamicsClient system, Guid id)
        {
            MicrosoftDynamicsCRMincident result;
            try
            {
                List<string> expand = new List<string>()
                {
                    "bcgov_incident_customproduct_RelatedApplication","customerid_account","bcgov_incident_businesscontact"
                };
                // fetch from Dynamics.
                result = system.Incidents.GetByKey(incidentid: id.ToString(), expand: expand);

               
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            
            if (result != null && result.BcgovIncidentBusinesscontact != null)
            {
                for (int i = 0; i < result.BcgovIncidentBusinesscontact.Count; i++)
                {
                    try
                    {
                        result.BcgovIncidentBusinesscontact[i] = system.GetBusinessContactById(result.BcgovIncidentBusinesscontact[i].BcgovBusinesscontactid);
                    }
                    catch (OdataerrorException)
                    {
                        // ignore the exception.
                    }
                }
            }


            return result;
        }

    }
}
