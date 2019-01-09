
namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class MicrosoftDynamicsCRMaccount
    {
        
        /// <summary>
        /// </summary>
        
        [JsonProperty(PropertyName = "primarycontactid@odata.bind")]
        public string PrimaryContactidODataBind { get; set; }

        [JsonProperty(PropertyName = "bcgov_AdditionalContact@odata.bind")]
        public string AdditionalContactODataBind { get; set; }

        [JsonProperty(PropertyName = "bcgov_CurrentBusinessMailingAddress@odata.bind")]
        public string CurrentBusinessMailingAddressODataBind { get; set; }

        [JsonProperty(PropertyName = "bcgov_CurrentBusinessPhysicalAddress@odata.bind")]
        public string CurrentBusinessPhysicalAddressODataBind { get; set; }

        [JsonProperty(PropertyName = "bcgov_account_bcgov_businesscontact_BusinessProfile@odata.bind")]
        public string BusinessContactODataBind { get; set; }

        

    }
}
