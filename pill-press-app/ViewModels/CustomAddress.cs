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

    public class CustomAddress
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "emailaddress")]
        public string Emailaddress { get; set; }

        [JsonProperty(PropertyName = "addresstype")]
        public int? BcgovAddresstype { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "streetline1")]
        public string Streetline1 { get; set; }

        [JsonProperty(PropertyName = "streetline2")]
        public string Streetline2 { get; set; }

        [JsonProperty(PropertyName = "streetline3")]
        public string Streetline3 { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "province")]
        public string Province { get; set; }

        [JsonProperty(PropertyName = "postalcode")]
        public string Postalcode { get; set; }

        [JsonProperty(PropertyName = "country")]
        public int? Country { get; set; }

    }
}
