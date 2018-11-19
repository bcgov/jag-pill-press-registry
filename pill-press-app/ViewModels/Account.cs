using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gov.Jag.PillPressRegistry.Public.ViewModels
{

    public enum Adoxio_stateprovince
    {
        [Display(Name = "Alberta")]
        AB = 845280000,
        [Display(Name = "British Columbia")]
        BC,
        [Display(Name = "Manitoba")]
        MN,
        [Display(Name = "New Brunswick")]
        NB,
        [Display(Name = "Newfoundland and Labrador")]
        NL,
        [Display(Name = "Northwest Territories")]
        NT,
        [Display(Name = "Nova Scotia")]
        NS,
        [Display(Name = "Nunavut")]
        NU,
        [Display(Name = "Ontario")]
        ON,
        [Display(Name = "Prince Edward Island")]
        PE,
        [Display(Name = "Quebec")]
        QC,
        [Display(Name = "Saskatchewan")]
        SK,
        [Display(Name = "Yukon")]
        YT
    }

    public enum BusinessTypeEnum
    {
        [Display(Name = "Sole Proprietor")]
        SoleProprietor = 1,
        [Display(Name = "Society")]
        Society = 931490000,
        [Display(Name = "Partnership")]
        Partnership = 931490001,
        [Display(Name = "Company")]
        Company = 931490002
    }

    public class Account
    {
        public string id { get; set; } // Guid

        public string clientId { get; set; } // dynamics = accountnumber

        public string businessLegalName { get; set; } // dynamics = name
        public string doingBusinessAs { get; set; } // dynamics = bcgov_doingbusinessasdbaname
        public string businessNumber { get; set; } // dynamics = bcgov_businessnumber
        public string businessType { get; set; } // dynamics = businesstypecode

        public string description { get; set; } //dynamics = description
        public string externalId { get; set; }
                
        public string businessEmail { get; set; } //dynamics = emailaddress1

        public bool? consentForEmailCommunication { get; set; }
        public string businessPhone { get; set; } //dynamics = telephone1
        public string mailingAddressName { get; set; } //dynamics = address1_name
        public string mailingAddressStreet { get; set; } //dynamics = address1_line1
        public string mailingAddressCity { get; set; } //dynamics = address1_city
        public string mailingAddressCountry { get; set; } //dynamics = address1_country
        public string mailingAddressProvince { get; set; } //dynamics = address1_stateorprovince
        public string mailingAddresPostalCode { get; set; } //dynamics = address1_postalcode

        public ViewModels.Contact primaryContact { get; set; }
        public ViewModels.Contact additionalContact { get; set; }

        public ViewModels.CustomAddress physicalAddress { get; set; }
        public ViewModels.CustomAddress mailingAddress { get; set; }

    }
}