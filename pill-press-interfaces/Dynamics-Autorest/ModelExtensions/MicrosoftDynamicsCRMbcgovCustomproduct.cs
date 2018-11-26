
namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class MicrosoftDynamicsCRMbcgovCustomproduct
    {
        
        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_RelatedApplication@odata.bind")]
        public string RelatedApplicationODataBind { get; set; }
       

    }
}
