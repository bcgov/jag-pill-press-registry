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
    public static class EquipmentExtensions
    {
        /// <summary>
        /// Convert a given Incident to an Application ViewModel
        /// </summary>        
        public static ViewModels.Equipment ToViewModel(this MicrosoftDynamicsCRMbcgovEquipment equipment)
        {
            ViewModels.Equipment result = null;
            if (equipment != null)
            {
                result = new ViewModels.Equipment()
                {

                    Id = equipment.BcgovEquipmentid,
                    EquipmentType = (Equipmenttype?)equipment.BcgovEquipmenttype,
                    EquipmentTypeOther = equipment.BcgovEquipmenttypeother,
                    Name = equipment.BcgovName,
                    PillpressEncapsulatorSize = (Pillpressencapsulatorsize?)equipment.BcgovPillpressencapsulatorsize,
                    PillpressEncapsulatorSizeOther = equipment.BcgovPillpressencapsulatorsizeother,
                    LevelOfEquipmentAutomation = (Levelofequipmentautomation?)equipment.BcgovLevelofautomation,
                    PillpressMaxCapacity = equipment.BcgovPillpressmaxcapacity,
                    HowWasEquipmentBuilt = (Howwasequipmentbuilt?)equipment.BcgovHowwasequipmentbuilt,
                    // HowWasEquipmentBuiltOther = equipment.BcgovHowwasequipmentbuiltother,
                    NameOfManufacturer = equipment.BcgovNameofmanufacturer,
                    EquipmentMake = equipment.BcgovMake,
                    EquipmentModel = equipment.BcgovModel,
                    SerialNumber = equipment.BcgovSerialnumber,
                    EncapsulatorMaxCapacity = equipment.BcgovEncapsulatormaxcapacity,
                    CustomBuiltSerialNumber = equipment.BcgovCustombuiltorkeypartserialnumber,
                };

            }
            return result;
        }

        public static void CopyValues(this MicrosoftDynamicsCRMbcgovEquipment to, ViewModels.Equipment from)
        {
            // Equipment Information
            to.BcgovEquipmenttype = (int?)from.EquipmentType;
            to.BcgovEquipmenttypeother = from.EquipmentTypeOther;
            to.BcgovName = from.Name;
            to.BcgovPillpressencapsulatorsize = (int?)from.PillpressEncapsulatorSize;
            to.BcgovPillpressencapsulatorsizeother = from.PillpressEncapsulatorSizeOther;
            to.BcgovLevelofautomation = (int?)from.LevelOfEquipmentAutomation;
            to.BcgovPillpressmaxcapacity = from.PillpressMaxCapacity;
            to.BcgovHowwasequipmentbuilt = (int?)from.HowWasEquipmentBuilt;
            // to.BcgovHowwasequipmentbuiltother = from.HowWasEquipmentBuiltOther;
            to.BcgovNameofmanufacturer = from.NameOfManufacturer;
            to.BcgovMake = from.EquipmentMake;
            to.BcgovModel = from.EquipmentModel;
            to.BcgovSerialnumber = from.SerialNumber;
            to.BcgovEncapsulatormaxcapacity = from.EncapsulatorMaxCapacity;
            to.BcgovCustombuiltorkeypartserialnumber = from.CustomBuiltSerialNumber;
        }

    }
}
