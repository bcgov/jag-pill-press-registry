
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class CustomAddressDynamicsExtensions
    {


        public static MicrosoftDynamicsCRMbcgovCustomaddress GetCustomAddressById(this IDynamicsClient system, Guid id)
        {            
            return system.GetCustomAddressById(id.ToString());
        }


        public static MicrosoftDynamicsCRMbcgovCustomaddress GetCustomAddressById(this IDynamicsClient system, string id)
        {
            MicrosoftDynamicsCRMbcgovCustomaddress result;
            try
            {
                // fetch from Dynamics.
                result = system.Customaddresses.GetByKey(id);
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            return result;
        }



    }
}
