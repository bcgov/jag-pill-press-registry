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
    public enum ProductPurpose
    {
        [EnumMember(Value = "Producing Own Product")]
        ProducingOwnProduct = 931490000,
        
        [EnumMember(Value = "Manufacturing For Others")]
        ManufacturingForOthers = 931490001,
    }

    public class CustomProduct
    {
        public string id { get; set; }

        public string productdescriptionandintendeduse { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProductPurpose Purpose { get; set; }

        public string incidentId { get; set; }
    }
}
