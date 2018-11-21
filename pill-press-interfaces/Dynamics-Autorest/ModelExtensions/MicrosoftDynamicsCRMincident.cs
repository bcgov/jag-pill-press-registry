namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class MicrosoftDynamicsCRMincident
    {

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "customerid_account@odata.bind")]
        public string CustomerIdAccountODataBind { get; set; }

        
        [JsonProperty(PropertyName = "bcgov_Submitter@odata.bind")]
        public string SubmitterODataBind { get; set; }
    }
}
