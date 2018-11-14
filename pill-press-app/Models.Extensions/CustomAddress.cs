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
    public static class CustomAddressExtensions
    {
        /// <summary>
        /// Convert a given voteQuestion to a ViewModel
        /// </summary>        
        public static ViewModels.CustomAddress ToViewModel(this MicrosoftDynamicsCRMbcgovCustomaddress customAddress)
        {
            ViewModels.CustomAddress result = null;
            if (customAddress != null)
            {                
                result = new ViewModels.CustomAddress()
                {
                    Emailaddress = customAddress.Emailaddress,
                    BcgovAddresstype = customAddress.BcgovAddresstype,
                    Name = customAddress.BcgovName,
                    Streetline2 = customAddress.BcgovStreetline2,
                    Streetline3 = customAddress.BcgovStreetline3,
                    City = customAddress.BcgovCity,
                    Province = customAddress.BcgovProvince,
                    Postalcode = customAddress.BcgovPostalcode,
                    Country = customAddress.BcgovCountry
                };

                if (customAddress.BcgovCustomaddressid != null)
                {
                    result.Id = customAddress.BcgovCustomaddressid;
                }
                                
            }
            return result;
        }

        public static void CopyValues(this MicrosoftDynamicsCRMbcgovCustomaddress to, ViewModels.CustomAddress from)
        {
            to.Emailaddress = from.Emailaddress;
            to.BcgovAddresstype = from.BcgovAddresstype;
            to.BcgovName = from.Name;
            to.BcgovStreetline2 = from.Streetline2;
            to.BcgovStreetline3 = from.Streetline3;
            to.BcgovCity = from.City;
            to.BcgovProvince = from.Province;
            to.BcgovPostalcode = from.Postalcode;
            to.BcgovCountry = from.Country;  
        }
    }
}
