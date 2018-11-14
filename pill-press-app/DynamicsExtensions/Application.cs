
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
                    "bcgov_incident_customproduct_RelatedApplication"
                };
                // fetch from Dynamics.
                result = system.Incidents.GetByKey(incidentid: id.ToString(), expand: expand);
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            return result;
        }

    }
}
