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
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Public.Controllers
{
    [Route("api/[controller]")]
    public class EquipmentController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IDynamicsClient _dynamicsClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;

        public EquipmentController(IConfiguration configuration, IDynamicsClient dynamicsClient, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            Configuration = configuration;
            _dynamicsClient = dynamicsClient;
            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger(typeof(EquipmentController));
            this._env = env;
        }

        /// <summary>
        /// Get a specific legal entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetEquipment(string id)
        {
            ViewModels.Equipment result = null;

            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid equipmentId))
            {
                // query the Dynamics system to get the Equipment record.
                MicrosoftDynamicsCRMbcgovEquipment equipment = _dynamicsClient.GetEquipmentByIdWithChildren(equipmentId);
                
                if (equipment != null)
                {
                    result = equipment.ToViewModel();
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
        /// Update an Equipment entity
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipment([FromBody] ViewModels.Equipment item, string id)
        {
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid equipmentId))
            {
                // get the Equipment
                MicrosoftDynamicsCRMbcgovEquipment equipment = _dynamicsClient.GetEquipmentByIdWithChildren(equipmentId);
                if (equipment == null)
                {
                    return new NotFoundResult();
                }

                // get UserSettings from the session
                string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
                UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

                // Get the current account
                var account = _dynamicsClient.GetAccountByIdWithChildren(Guid.Parse(userSettings.AccountId));

                MicrosoftDynamicsCRMbcgovEquipment patchEquipment = new MicrosoftDynamicsCRMbcgovEquipment();
                patchEquipment.CopyValues(item);

                try
                {
                    await _dynamicsClient.Equipments.UpdateAsync(equipmentId.ToString(), patchEquipment);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError("Error updating Equipment");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                }

                equipment = _dynamicsClient.GetEquipmentByIdWithChildren(equipmentId);
                return Json(equipment.ToViewModel());
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Change Equipment Location
        /// </summary>
        /// <param name="applicationVM"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeEquipmentLocation([FromBody] ViewModels.Application applicationVM, string id)
        {
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid equipmentId))
            {
                // get the Equipment
                MicrosoftDynamicsCRMbcgovEquipment equipment = _dynamicsClient.GetEquipmentByIdWithChildren(equipmentId);
                if (equipment == null)
                {
                    return new NotFoundResult();
                }

                // get UserSettings from the session
                string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
                UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

                // Get the current account
                var account = _dynamicsClient.GetAccountByIdWithChildren(Guid.Parse(userSettings.AccountId));

                MicrosoftDynamicsCRMbcgovEquipment patchEquipment = new MicrosoftDynamicsCRMbcgovEquipment();
                patchEquipment.CopyValues(applicationVM.EquipmentRecord);

                try
                {
                    await _dynamicsClient.Equipments.UpdateAsync(equipmentId.ToString(), patchEquipment);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError("Error updating Equipment");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                }

                equipment = _dynamicsClient.GetEquipmentByIdWithChildren(equipmentId);
                return Json(equipment.ToViewModel());
            }
            else
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Create an Equipment
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEquipment(string applicationType, [FromBody] ViewModels.Equipment item)
        {

            // get UserSettings from the session
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

            // first check to see that a Equipment exists.
            string ApplicationSiteminderGuid = userSettings.SiteMinderGuid;
            if (ApplicationSiteminderGuid == null || ApplicationSiteminderGuid.Length == 0)
            {
                _logger.LogError(LoggingEvents.Error, "No Equipment Siteminder Guid exernal id");
                throw new Exception("Error. No ApplicationSiteminderGuid exernal id");
            }

            // create a new Equipment.
            MicrosoftDynamicsCRMbcgovEquipment equipment = new MicrosoftDynamicsCRMbcgovEquipment();
            equipment.CopyValues(item);

            try
            {
                equipment = await _dynamicsClient.Equipments.CreateAsync(equipment);
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError("Error creating Equipment");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
                throw new OdataerrorException("Error creating Equipment");
            }           
            return Json(equipment.ToViewModel());
        }

        /// <summary>
        /// Delete an equipment.  Using a HTTP Post to avoid Siteminder issues with DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public IActionResult DeleteEquipment(string id)
        {
            _logger.LogDebug(LoggingEvents.HttpPost, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);

            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid equipmentId))
            {

                MicrosoftDynamicsCRMbcgovEquipment equipment = _dynamicsClient.GetEquipmentById(equipmentId);
                if (equipment == null)
                {
                    _logger.LogWarning(LoggingEvents.NotFound, "Equipment NOT found.");
                    return new NotFoundResult();
                }

                // TODO - add this routine.
                /*
                if (!UserDynamicsExtensions.CurrentUserHasAccessToEquipment(equipmentId, _httpContextAccessor, _dynamicsClient))
                {
                    _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to the equipment.");
                    return new NotFoundResult();
                }
                */

                // delete the equipment            
                try
                {
                    _dynamicsClient.Equipments.Delete(equipmentId.ToString());
                    _logger.LogDebug(LoggingEvents.HttpDelete, "Equipment deleted: " + equipmentId.ToString());
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError(LoggingEvents.Error, "Error deleting the equipment: " + equipmentId.ToString());
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                    throw new OdataerrorException("Error deleting the account: " + equipmentId.ToString());
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
