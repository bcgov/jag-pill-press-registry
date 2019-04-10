
namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Newtonsoft.Json;

    public partial class MicrosoftDynamicsCRMbcgovEquipmentlocation
    {

        [JsonProperty(PropertyName = "bcgov_Equipment@odata.bind")]
        public string EquipmentODataBind { get; set; }

        [JsonProperty(PropertyName = "bcgov_Location@odata.bind")]
        public string LocationODataBind { get; set; }
    }
}
