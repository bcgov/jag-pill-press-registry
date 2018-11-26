using Gov.Jag.PillPressRegistry.Interfaces;
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Authentication;
using Gov.Jag.PillPressRegistry.Public.Models;
using Gov.Jag.PillPressRegistry.Public.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Public.Controllers
{
    [Route("api/[controller]")]
    public class CustomProductController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IDynamicsClient _dynamicsClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;

        public CustomProductController(IConfiguration configuration, IDynamicsClient dynamicsClient, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            Configuration = configuration;
            _dynamicsClient = dynamicsClient;
            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger(typeof(ContactController));
            this._env = env;
        }


        /// <summary>
        /// Get a specific Custom Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public  IActionResult GetCustomProduct(string id)
        {
            ViewModels.CustomProduct result = null;

            if (!string.IsNullOrEmpty(id))
            {
                Guid customProductGuid = Guid.Parse(id);
                // query the Dynamics system to get the contact record.
                MicrosoftDynamicsCRMbcgovCustomproduct contact = _dynamicsClient.GetCustomProductById(customProductGuid);

                if (contact != null)
                {
                    result = contact.ToViewModel();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            else
            {
                return BadRequest();
            }

            return Json(result);
        }


        /// <summary>
        /// Update a Custom Product
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomProduct([FromBody] ViewModels.CustomProduct item, string id)
        {
            if (id != null && item.id != null && id != item.id)
            {
                return BadRequest();
            }

            // get the customProduct
            Guid customProductGuid = Guid.Parse(id);

            MicrosoftDynamicsCRMbcgovCustomproduct customProduct =  _dynamicsClient.GetCustomProductById(customProductGuid);
            if (customProduct == null)
            {
                return new NotFoundResult();
            }
            MicrosoftDynamicsCRMbcgovCustomproduct patchCustomProduct = new MicrosoftDynamicsCRMbcgovCustomproduct();
            patchCustomProduct.CopyValues(item);
            try
            {
                 _dynamicsClient.Customproducts.Update(customProductGuid.ToString(), patchCustomProduct);
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError("Error updating Custom Product");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
            }

            customProduct =  _dynamicsClient.GetCustomProductById(customProductGuid);
            return Json(customProduct.ToViewModel());
        }

        /// <summary>
        /// Create a contact
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateCustomProduct([FromBody] ViewModels.CustomProduct item)
        {

            // get UserSettings from the session
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);


            // create a new custom product.
            MicrosoftDynamicsCRMbcgovCustomproduct customProduct = new MicrosoftDynamicsCRMbcgovCustomproduct();
            customProduct.CopyValues(item);
            
            try
            {
                customProduct = await _dynamicsClient.Customproducts.CreateAsync(customProduct);
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError("Error updating custom product");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
            }
            

            return Json(customProduct.ToViewModel());
        }

        /// <summary>
        /// Delete a custom product.  Using a HTTP Post to avoid Siteminder issues with DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteCustomProduct(string id)
        {
            _logger.LogDebug(LoggingEvents.HttpPost, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);

            // verify the currently logged in user has access to this account
            Guid customProductGuid = new Guid(id);

            // get the custom product
            MicrosoftDynamicsCRMbcgovCustomproduct customProduct = _dynamicsClient.GetCustomProductById(customProductGuid);
            if (customProduct == null)
            {
                _logger.LogWarning(LoggingEvents.NotFound, "Custom product NOT found.");
                return new NotFoundResult();
            }

            try
            {
                await _dynamicsClient.Customproducts.DeleteAsync(customProductGuid.ToString());
                _logger.LogDebug(LoggingEvents.HttpDelete, "Custom product deleted: " + customProductGuid.ToString());
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError(LoggingEvents.Error, "Error deleting the custom product: " + customProductGuid.ToString());
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
                throw new OdataerrorException("Error deleting the custom product: " + customProductGuid.ToString());
            }

            _logger.LogDebug(LoggingEvents.HttpDelete, "No content returned.");
            return NoContent(); // 204 
        }
    }
}
