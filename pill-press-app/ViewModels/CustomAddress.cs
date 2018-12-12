using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Public.ViewModels
{    

    public enum AddressTypes
    {
        Location = 931490002
    }

    public class CustomAddress
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Emailaddress { get; set; }

        [JsonProperty(PropertyName = "addresstype")]
        public int? BcgovAddresstype { get; set; }

        [JsonProperty(PropertyName = "streetLine1")]
        public string StreetLine1 { get; set; }

        [JsonProperty(PropertyName = "streetLine2")]
        public string StreetLine2 { get; set; }

        [JsonProperty(PropertyName = "streetLine3")]
        public string StreetLine3 { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "province")]
        public string Province { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string Postalcode { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

    }
}
