namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class MicrosoftDynamicsCRMsharepointdocumentlocation
    {

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_account@odata.bind")]
        public string RegardingobjectIdAccountODataBind { get; set; }


        [JsonProperty(PropertyName = "regardingobjectid_incident@odata.bind")]
        public string RegardingobjectIdIncidentODataBind { get; set; }

        [JsonProperty(PropertyName = "parentsiteorlocation_sharepointdocumentlocation@odata.bind")]        
        public string ParentsiteorlocationSharepointdocumentlocationODataBind { get; set; }

    }
}
