using Gov.Jag.PillPressRegistry.Interfaces;
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    Name = equipmentLocation.BcgovName
                };
                

                if (equipmentLocation.BcgovLocation != null)
                {
                    result.Location = equipmentLocation.BcgovLocation.ToViewModel();
                }

            }
            return result;
        }

        public static void CopyValues(this MicrosoftDynamicsCRMbcgovEquipmentlocation to, ViewModels.Location from)
        {
            to.BcgovName = from.Name;                
        }

    
        
    

    }
}
