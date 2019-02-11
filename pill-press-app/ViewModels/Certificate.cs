using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Public.ViewModels
{
    public class Certificate
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "approvedIntendedUse")]
        public string ApprovedIntendedUse { get; set; }

        [JsonProperty(PropertyName = "issueDate")]
        public DateTimeOffset? IssueDate { get; set; }

        [JsonProperty(PropertyName = "issueDateString")]
        public string IssueDateString { get; set; }

        [JsonProperty(PropertyName = "expiryDate")]
        public DateTimeOffset? ExpiryDate { get; set; }

        [JsonProperty(PropertyName = "expiryDateString")]
        public string ExpiryDateString { get; set; }
    }
}
