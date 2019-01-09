namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class MicrosoftDynamicsCRMcontact
    {
        
        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "parentcustomerid_account@odata.bind")]
        public string ParentCustomerIdAccountODataBind { get; set; }

        [JsonProperty(PropertyName = "bcgov_contact_bcgov_businesscontact_Contact@odata.bind")]
        public string BusinessContactODataBind { get; set; }

    }
}
