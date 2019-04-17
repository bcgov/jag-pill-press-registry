using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Models;
using System;
using System.Collections.Generic;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class EquipmentlocationDynamicsExtensions
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

        /// <summary>
        /// Getthe Equipment Location record by Equipment Id and Location Id
        /// </summary>
        /// <param name="system"></param>
        /// <param name="equipmentId"></param>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public static MicrosoftDynamicsCRMbcgovEquipmentlocation GetEquipmentLocationByBothIds(this IDynamicsClient system, Guid equipmentId, Guid locationId)
        {
            MicrosoftDynamicsCRMbcgovEquipmentlocation result = null;
            try
            {
                // fetch from Dynamics.
                string filter = $"_bcgov_equipment_value eq {equipmentId} and _bcgov_location_value eq {locationId}";
                List<string> expand = new List<string> { "bcgov_Equipment", "bcgov_Location" };
                var queryResult = system.Equipmentlocations.Get(filter: filter, expand: expand);
                foreach (var item in queryResult.Value)
                {
                    result = item;
                }
            }
            catch (OdataerrorException)
            {
                result = null;
            }

            // add the address to the location
            if (result.BcgovLocation != null && result.BcgovLocation._bcgovLocationaddressValue != null)
            {
                result.BcgovLocation.BcgovLocationAddress = system.GetCustomAddressById(result.BcgovLocation._bcgovLocationaddressValue);
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
                    "bcgov_Equipment", "bcgov_Location"
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
