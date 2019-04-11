
namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Newtonsoft.Json;

    public partial class MicrosoftDynamicsCRMbcgovEquipment
    {
        [JsonProperty(PropertyName = "bcgov_CurrentLocationt@odata.bind")]
        public string CurrentLocationODataBind { get; set; }
    }
}
