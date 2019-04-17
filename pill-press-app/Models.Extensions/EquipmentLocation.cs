using Gov.Jag.PillPressRegistry.Interfaces.Models;

namespace Gov.Jag.PillPressRegistry.Public.Models

{
    /// <summary>
    /// ViewModel transforms.
    /// </summary>
    public static class EquipmentlocationExtensions
    {
        /// <summary>
        /// Convert a given Equipmentlocation to a ViewModel
        /// </summary>        
        public static ViewModels.Equipmentlocation ToViewModel(this MicrosoftDynamicsCRMbcgovEquipmentlocation equipmentLocation)
        {
            ViewModels.Equipmentlocation result = null;
            if (equipmentLocation != null)
            {
                result = new ViewModels.Equipmentlocation()
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

                if (equipmentLocation.BcgovEquipment != null)
                {
                    result.Equipment = equipmentLocation.BcgovEquipment.ToViewModel();
                }

            }
            return result;
        }

        public static void CopyValues(this MicrosoftDynamicsCRMbcgovEquipmentlocation to, ViewModels.Equipmentlocation from)
        {
            to.BcgovName = from.Name;
            to.BcgovFromwhen = from.FromWhen;
            to.BcgovSettingdescription = from.SettingDescription;
        }

    }
}
