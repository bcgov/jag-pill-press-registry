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
        /// Convert a given BusinessContact to a ViewModel
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
                    //PrivateDwelling = location.BcgovPrivatedwelling,
                    SettingDescription = location.BcgovSettingdescription
                };
                
                if (location.BcgovLocationAddress != null)
                {
                    result.Address = location.BcgovLocationAddress.ToViewModel();
                }
            }
            return result;
        }

        public static void CopyValues(this MicrosoftDynamicsCRMbcgovLocation to, ViewModels.Location from)
        {
            to.BcgovName = from.Name;
            //to.BcgovPrivatedwelling = from.PrivateDwelling;
            to.BcgovSettingdescription = from.SettingDescription;
        }

        public static MicrosoftDynamicsCRMbcgovLocation ToModel(this ViewModels.Location location)
        {
            MicrosoftDynamicsCRMbcgovLocation result = null;
            if (location != null)
            {
                result = new MicrosoftDynamicsCRMbcgovLocation()
                {
                    BcgovLocationid = location.Id,
                    //BcgovPrivatedwelling = location.PrivateDwelling,
                    BcgovSettingdescription = location.SettingDescription,
                };    
                                
            }
            return result;
        }
    }
}
