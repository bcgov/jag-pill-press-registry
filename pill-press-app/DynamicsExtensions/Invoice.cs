
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using System;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class InvoiceDynamicsExtensions
    {

        /// <summary>
        /// Get an Invoice by the Id
        /// </summary>
        /// <param name="system">Re</param>
        /// <param name="id"></param>
        /// <returns>The Invoice, or null if it does not exist</returns>
        public static async Task<MicrosoftDynamicsCRMinvoice> GetInvoiceById(this IDynamicsClient system, Guid id)
        {
            MicrosoftDynamicsCRMinvoice result;
            try
            {
                // fetch from Dynamics.
                result = await system.Invoices.GetByKeyAsync(id.ToString());
            }
            catch (OdataerrorException)
            {
                result = null;
            }
            return result;
        }        

    }
}
