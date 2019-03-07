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
    public class Location
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "privateDwelling")]
        public bool? PrivateDwelling { get; set; }

        [JsonProperty(PropertyName = "settingDescription")]
        public string SettingDescription { get; set; }

        [JsonProperty(PropertyName = "address")]
        public CustomAddress Address { get; set; }

        //[JsonProperty(PropertyName = "application")]
        //public Application Application { get; set; }

    }
}
