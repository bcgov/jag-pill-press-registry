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
    public static class LocationExtensions
    {
        /// <summary>
        /// Convert a Location to a ViewModel
        /// </summary>        
        public static ViewModels.Location ToViewModel(this MicrosoftDynamicsCRMbcgovLocation location)
        {
            ViewModels.Location result = null;
            if (location != null)
            {
                result = new ViewModels.Location()
                {
                    Id = location.BcgovLocationid,
                    Name = location.BcgovName,
                    PrivateDwelling = (PrivateDwellingOptions?)location.BcgovPrvtdwelling,
                    //SettingDescription = location.BcgovSettingdescription
                };

                if (location.BcgovLocationAddress != null)
                {
                    result.Address = location.BcgovLocationAddress.ToViewModel();
                }
            }
            return result;
        }

        /// <summary>
        /// copy values from a view model location to a model location
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        public static void CopyValues(this MicrosoftDynamicsCRMbcgovLocation to, ViewModels.Location from)
        {
            to.BcgovName = from.Name;
            to.BcgovPrvtdwelling = (int?)from.PrivateDwelling;
            //to.BcgovSettingdescription = from.SettingDescription;
        }

        /// <summary>
        /// Convert a view model location to a model location
        /// </summary>
        /// <param name="locationVM"></param>
        /// <returns></returns>
        public static MicrosoftDynamicsCRMbcgovLocation ToModel(this ViewModels.Location locationVM)
        {
            MicrosoftDynamicsCRMbcgovLocation result = null;
            if (locationVM != null)
            {
                result = new MicrosoftDynamicsCRMbcgovLocation()
                {
                    BcgovLocationid = locationVM.Id,
                    BcgovPrvtdwelling = (int?)locationVM.PrivateDwelling
                    //BcgovSettingdescription = location.SettingDescription,
                };

                result.BcgovPrvtdwelling = (int?)locationVM.PrivateDwelling;

            }
            return result;
        }
    }
}
