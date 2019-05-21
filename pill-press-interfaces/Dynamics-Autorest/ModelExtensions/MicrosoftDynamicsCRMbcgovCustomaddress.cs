using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Newtonsoft.Json;

    public partial class MicrosoftDynamicsCRMbcgovCustomaddress
    {
        [JsonProperty(PropertyName = "bcgov_BusinessProfile@odata.bind")]
        public string BusinessProfileODataBind { get; set; }
    }
}
