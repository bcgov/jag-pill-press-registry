
namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    

    public partial class MicrosoftDynamicsCRMbcgovBusinesscontact
    {
        
        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_contact@odata.bind")]
        public string ContactODataBind { get; set; }

        [JsonProperty(PropertyName = "bcgov_businessprofile@odata.bind")]
        public string AccountODataBind { get; set; }

    }
}
