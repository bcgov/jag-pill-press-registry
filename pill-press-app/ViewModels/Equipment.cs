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
    public class Equipment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "equipmentType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Equipmenttype? EquipmentType { get; set; }

        [JsonProperty(PropertyName = "equipmentTypeOther")]
        public string EquipmentTypeOther { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "pillpressEncapsulatorsize")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Pillpressencapsulatorsize? PillpressEncapsulatorSize { get; set; }

        [JsonProperty(PropertyName = "pillpressEncapsulatorSizeOther")]
        public string PillpressEncapsulatorSizeOther { get; set; }

        [JsonProperty(PropertyName = "levelOfEquipmentAutomation")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Levelofequipmentautomation? LevelOfEquipmentAutomation { get; set; }

        [JsonProperty(PropertyName = "pillpressmaxcapacity")]
        public int? PillpressMaxCapacity { get; set; }

        [JsonProperty(PropertyName = "howWasEquipmentBuilt")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Howwasequipmentbuilt? HowWasEquipmentBuilt { get; set; }

        [JsonProperty(PropertyName = "howWasEquipmentBuiltOther")]
        public string HowWasEquipmentBuiltOther { get; set; }

        [JsonProperty(PropertyName = "nameOfManufacturer")]
        public string NameOfManufacturer { get; set; }

        [JsonProperty(PropertyName = "equipmentMake")]
        public string EquipmentMake { get; set; }

        [JsonProperty(PropertyName = "equipmentModel")]
        public string EquipmentModel { get; set; }

        [JsonProperty(PropertyName = "serialNumber")]
        public string SerialNumber { get; set; }

        [JsonProperty(PropertyName = "encapsulatorMaxCapacity")]
        public int? EncapsulatorMaxCapacity { get; set; }

        [JsonProperty(PropertyName = "customBuiltSerialNumber")]
        public string CustomBuiltSerialNumber { get; set; }

        public string EmailAddress { get; set; }

    }
}
