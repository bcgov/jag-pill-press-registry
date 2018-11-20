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
        public static int? GetCountry (string data)
        {
            return 931490000;
        }

        public static string GetCountryText(int? data)
        {
            return "Canada";
        }

        public static bool HasValue(this ViewModels.CustomAddress customAddress)
        {
            bool result = customAddress != null &&
                !(string.IsNullOrEmpty(customAddress.City) &&
                 // Do not check 
                 string.IsNullOrEmpty(customAddress.Emailaddress) &&
                 string.IsNullOrEmpty(customAddress.Id) &&
                 string.IsNullOrEmpty(customAddress.Postalcode) &&
                 string.IsNullOrEmpty(customAddress.Streetline1) &&
                 string.IsNullOrEmpty(customAddress.Streetline2) &&
                 string.IsNullOrEmpty(customAddress.Streetline3)
                 );
            return result;
        }

        public static MicrosoftDynamicsCRMbcgovCustomaddress ToModel(this ViewModels.CustomAddress customAddress)
        {
            MicrosoftDynamicsCRMbcgovCustomaddress result = null;
            if (customAddress != null)
            {
                result = new MicrosoftDynamicsCRMbcgovCustomaddress()
                {
                    Emailaddress = customAddress.Emailaddress,
                    BcgovAddresstype = customAddress.BcgovAddresstype,
                    BcgovName = customAddress.Streetline1,
                    BcgovStreetline2 = customAddress.Streetline2,
                    BcgovStreetline3 = customAddress.Streetline3,
                    BcgovCity = customAddress.City,
                    BcgovProvince = customAddress.Province,
                    BcgovPostalcode = customAddress.Postalcode,
                    BcgovCountry = customAddress.Country
                };

                if (customAddress.Id != null)
                {
                    result.BcgovCustomaddressid = customAddress.Id;
                }

            }
            return result;
        }

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
                    Streetline1 = customAddress.BcgovName,
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
            to.BcgovName = from.Streetline1;
            to.BcgovStreetline2 = from.Streetline2;
            to.BcgovStreetline3 = from.Streetline3;
            to.BcgovCity = from.City;
            to.BcgovProvince = from.Province;
            to.BcgovPostalcode = from.Postalcode;
            to.BcgovCountry = from.Country;  
        }
    }
}
