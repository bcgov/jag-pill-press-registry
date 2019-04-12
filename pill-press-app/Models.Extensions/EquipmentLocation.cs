﻿using Gov.Jag.PillPressRegistry.Interfaces.Models;

namespace Gov.Jag.PillPressRegistry.Public.Models
{
    /// <summary>
    /// ViewModel transforms.
    /// </summary>
    public static class EquipmentLocationExtensions
    {
        /// <summary>
        /// Convert a given BusinessContact to a ViewModel
        /// </summary>        
        public static ViewModels.EquipmentLocation ToViewModel(this MicrosoftDynamicsCRMbcgovEquipmentlocation equipmentLocation)
        {
            ViewModels.EquipmentLocation result = null;
            if (equipmentLocation != null)
            {
                result = new ViewModels.EquipmentLocation()
                {
                    Id = equipmentLocation.BcgovEquipmentlocationid,
                    Name = equipmentLocation.BcgovName,
                    SettingDescription = equipmentLocation.BcgovSettingdescription,
                    FromWhen = equipmentLocation.BcgovFromwhen
                };
                

                if (equipmentLocation.BcgovLocation != null)
                {
                    result.Location = equipmentLocation.BcgovLocation.ToViewModel();
                }

            }
            return result;
        }

        public static void CopyValues(this MicrosoftDynamicsCRMbcgovEquipmentlocation to, ViewModels.EquipmentLocation from)
        {
            to.BcgovName = from.Name;
            to.BcgovFromwhen = from.FromWhen;
            to.BcgovSettingdescription = from.SettingDescription;
        }

    
        
    

    }
}
