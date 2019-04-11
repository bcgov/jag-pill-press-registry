using Gov.Jag.PillPressRegistry.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class EquipmentLocationDynamicsExtensions
    {

        public static MicrosoftDynamicsCRMbcgovEquipmentlocation GetEquipmentLocationById(this IDynamicsClient system, Guid id)
        {
            MicrosoftDynamicsCRMbcgovEquipmentlocation result;
            try
            {
                // fetch from Dynamics.
                result = system.Equipmentlocations.GetByKey(id.ToString());
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            return result;
        }

        public static MicrosoftDynamicsCRMbcgovEquipmentlocation GetEquipmentLocationByIdWithChildren(this IDynamicsClient system, Guid id)
        {
            MicrosoftDynamicsCRMbcgovEquipmentlocation result;
            try
            {
                List<string> expand = new List<string>()
                {
                    //"bcgov_incident_customproduct_RelatedApplication","customerid_account","bcgov_incident_businesscontact"
                };
                // fetch from Dynamics.
                result = system.Equipmentlocations.GetByKey(bcgovEquipmentlocationid: id.ToString(), expand: expand);
            }
            catch (OdataerrorException)
            {
                result = null;
            }

            return result;
        }

    }
}
