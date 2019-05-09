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
                 string.IsNullOrEmpty(customAddress.StreetLine1) &&
                 string.IsNullOrEmpty(customAddress.StreetLine2) &&
                 string.IsNullOrEmpty(customAddress.StreetLine3)
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
                    BcgovAddresstype = (int?)customAddress.BcgovAddresstype,
                    BcgovName = customAddress.StreetLine1,
                    BcgovStreetline2 = customAddress.StreetLine2,
                    BcgovStreetline3 = customAddress.StreetLine3,
                    BcgovCity = customAddress.City,
                    BcgovProvince = customAddress.Province,
                    BcgovPostalcode = customAddress.Postalcode,
                    BcgovCountrytxt = customAddress.Country
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
                    BcgovAddresstype = (AddressTypes?)customAddress.BcgovAddresstype,
                    StreetLine1 = customAddress.BcgovName,
                    StreetLine2 = customAddress.BcgovStreetline2,
                    StreetLine3 = customAddress.BcgovStreetline3,
                    City = customAddress.BcgovCity,
                    Province = customAddress.BcgovProvince,
                    Postalcode = customAddress.BcgovPostalcode,
                    Country = customAddress.BcgovCountrytxt
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
            to.BcgovAddresstype = (int?)from.BcgovAddresstype;
            to.BcgovName = from.StreetLine1;
            to.BcgovStreetline2 = from.StreetLine2;
            to.BcgovStreetline3 = from.StreetLine3;
            to.BcgovCity = from.City;
            to.BcgovProvince = from.Province;
            to.BcgovPostalcode = from.Postalcode;
            to.BcgovCountrytxt = from.Country;  
        }
    }
}
