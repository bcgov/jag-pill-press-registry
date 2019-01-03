
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class LocationDynamicsExtensions
    {
        public static bool HasValue(this MicrosoftDynamicsCRMbcgovLocation location)
        {
            bool result = !location.IsNullOrEmpty();
            return result;
        }

        public static bool IsNullOrEmpty(this MicrosoftDynamicsCRMbcgovLocation location)
        {
            bool result = true;
            if (location != null)
            {
                if (!string.IsNullOrEmpty(location.BcgovSettingdescription) || location.BcgovLocationAddress.HasValue())
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
