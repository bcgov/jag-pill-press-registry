using Gov.Jag.PillPressRegistry.Interfaces;
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Authentication;
using Gov.Jag.PillPressRegistry.Public.Models;
using Gov.Jag.PillPressRegistry.Public.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;

        public static readonly string ENTITY_NAME = "bcgov_customproduct";

        public CustomProductController(IConfiguration configuration, IDynamicsClient dynamicsClient, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory, IHostingEnvironment env, IMemoryCache memoryCache)
        {
            Configuration = configuration;
            _dynamicsClient = dynamicsClient;
            _cache = memoryCache;

            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger(typeof(CustomProductController));
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

            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid customProductGuid))
            {                
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
        public IActionResult UpdateCustomProduct([FromBody] ViewModels.CustomProduct item, string id)
        {
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid customProductGuid))
            {
                int? maxLength = ControllerUtility.GetAttributeMaxLength((DynamicsClient)_dynamicsClient, _cache, _logger, ENTITY_NAME, "bcgov_productdescriptionandintendeduse");
                if (maxLength != null && item.productdescriptionandintendeduse.Length > maxLength)
                {
                    return BadRequest("productdescriptionandintendeduse exceeds maximum field length");
                }

                // get the customProduct
                MicrosoftDynamicsCRMbcgovCustomproduct customProduct = _dynamicsClient.GetCustomProductById(customProductGuid);
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

                customProduct = _dynamicsClient.GetCustomProductById(customProductGuid);
                return Json(customProduct.ToViewModel());
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Create a Custom Product
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateCustomProduct([FromBody] ViewModels.CustomProduct item)
        {

            if (string.IsNullOrEmpty(item.incidentId))
            {
                return BadRequest("IncidentId missing");
            }

            int? maxLength = ControllerUtility.GetAttributeMaxLength((DynamicsClient) _dynamicsClient, _cache, _logger, ENTITY_NAME, "bcgov_productdescriptionandintendeduse");
            if(maxLength != null && item.productdescriptionandintendeduse.Length > maxLength)
            {
                return BadRequest("productdescriptionandintendeduse exceeds maximum field length");
            }

            // get UserSettings from the session
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);


            // create a new custom product.
            MicrosoftDynamicsCRMbcgovCustomproduct customProduct = new MicrosoftDynamicsCRMbcgovCustomproduct();
            customProduct.CopyValues(item);
            customProduct.RelatedApplicationODataBind = _dynamicsClient.GetEntityURI("incidents", item.incidentId);

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
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid customProductGuid))
            {

                _logger.LogDebug(LoggingEvents.HttpPost, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);

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
            else
            {
                return BadRequest();
            }
        }
    }
}
