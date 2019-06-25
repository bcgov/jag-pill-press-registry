using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Public.ViewModels
{

    public enum ContactTypeCodes
    {
        Primary = 931490000,
        Additional = 931490001,
        BCeID = 931490002
    }

    public enum OwnerManagerCodes
    {
        Owner = 931490000,
        Manager = 931490001
    }

    public class BusinessContact
    {
        public string id { get; set; }

        public Contact contact { get; set; }

        public Account account { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ContactTypeCodes? contactType { get; set; }

        public string jobTitle { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OwnerManagerCodes registeredSellerOwnerManager { get; set; }

        public DateTimeOffset? fromdate { get; set; }

        public DateTimeOffset? enddate { get; set; }

    }
}
