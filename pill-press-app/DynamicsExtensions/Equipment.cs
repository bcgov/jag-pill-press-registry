
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class EquipmentDynamicsExtensions
    {
        
        public static MicrosoftDynamicsCRMbcgovEquipment GetEquipmentById(this IDynamicsClient system, Guid id)
        {
            MicrosoftDynamicsCRMbcgovEquipment result;
            try
            {
                // fetch from Dynamics.
                result = system.Equipments.GetByKey(id.ToString());
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            return result;
        }

        public static MicrosoftDynamicsCRMbcgovEquipment GetEquipmentByIdWithChildren(this IDynamicsClient system, Guid id)
        {
            MicrosoftDynamicsCRMbcgovEquipment result;
            try
            {
                List<string> expand = new List<string>()
                {
                    "bcgov_CurrentBusinessOwner", "bcgov_CurrentLocation"
                };
                // fetch from Dynamics.
                result = system.Equipments.GetByKey(bcgovEquipmentid: id.ToString(), expand: expand);

               
            }
            catch (OdataerrorException)
            {
                result = null;
            }
                        

            return result;
        }

    }
}
