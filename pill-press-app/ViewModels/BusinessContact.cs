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

    public class BusinessContact
    {
        public string id { get; set; }

        public Contact contact { get; set; }

        public Account account { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ContactTypeCodes? contacttype { get; set; }

        public string jobtitle { get; set; }

    }
}
