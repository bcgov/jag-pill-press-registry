
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class CustomProductDynamicsExtensions
    {


        public static MicrosoftDynamicsCRMbcgovCustomproduct GetCustomProductById(this IDynamicsClient system, Guid id)
        {
            MicrosoftDynamicsCRMbcgovCustomproduct result;
            try
            {
                // fetch from Dynamics.
                result = system.Customproducts.GetByKey(id.ToString());
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            return result;
        }

        

    }
}
