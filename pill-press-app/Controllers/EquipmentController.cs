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
        /// Get an Equipment record by Id
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

        [HttpGet("{id}/{locaId}")]
        public IActionResult GetEquipmentLocation(string id, string locaId)
        {
            ViewModels.Equipmentlocation result = null;

            if ((!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid equipmentId)) && (!string.IsNullOrEmpty(locaId) && Guid.TryParse(locaId, out Guid locationId)))
            {
                // query the Dynamics system to get the Equipment Location record.
                //MicrosoftDynamicsCRMbcgovEquipment equipment = _dynamicsClient.GetEquipmentByIdWithChildren(equipmentId);
                MicrosoftDynamicsCRMbcgovEquipmentlocation equipmentlocation = _dynamicsClient.GetEquipmentLocationByBothIds(equipmentId, locationId);

                if (equipmentlocation != null)
                {
                    result = equipmentlocation.ToViewModel();
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
        /// Get the Equipment location record based on the current locaion of an equipment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/currentequipmentlocation")]
        public IActionResult GetEquipmentLocationFromEquipmentCurrentLocation(string id)
        {
            ViewModels.Equipmentlocation result = null;

            //validate parameter
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out Guid equipmentId))
            {
                return BadRequest();
            }

            //first get the current location of the equipment
            MicrosoftDynamicsCRMbcgovEquipment equipment = _dynamicsClient.GetEquipmentByIdWithChildren(equipmentId);
            var currentLocaId = equipment.BcgovCurrentLocation.BcgovLocationid;
            
            //validate current location id
            if (string.IsNullOrEmpty(currentLocaId) || !Guid.TryParse(currentLocaId, out Guid currentLocationId))
            {
                return new NotFoundResult();
            }

            // get the Equipmentlocation record
            MicrosoftDynamicsCRMbcgovEquipmentlocation equipmentlocation = _dynamicsClient.GetEquipmentLocationByBothIds(equipmentId, currentLocationId);
            if (equipmentlocation != null)
            {
                result = equipmentlocation.ToViewModel();
            }
            else
            {
                return new NotFoundResult();
            }

            return Json(result);
        }

        /// <summary>
        /// Get the current location of an Equipment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/currentlocation")]
        public IActionResult GetEquipmentCurrentLocation(string id)
        {
            ViewModels.Location result = null;

            //validate parameter
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out Guid equipmentId))
            {
                return BadRequest();
            }

            // get the equipment
            MicrosoftDynamicsCRMbcgovEquipment equipment = _dynamicsClient.GetEquipmentByIdWithChildren(equipmentId);

            if (equipment.BcgovCurrentLocation != null)
            {
                // get the view model of the current location of the equipment
                result = equipment.BcgovCurrentLocation.ToViewModel();
            }
            else
            {
                return new NotFoundResult();
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
        [HttpPut("{id}/changeEquipmentLocation")]
        public IActionResult ChangeEquipmentLocation([FromBody] ViewModels.Application applicationVM, string id)
        {
            MicrosoftDynamicsCRMbcgovLocation location = null;
            MicrosoftDynamicsCRMbcgovEquipmentlocation equipmentLocation = null;

            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid equipmentId))
            {
                // Get the Equipment passed as parameter
                MicrosoftDynamicsCRMbcgovEquipment equipment = _dynamicsClient.GetEquipmentById(equipmentId);
                if (equipment == null)
                {
                    return new NotFoundResult();
                }

                // Create or Get the location passed as parameter
                if (string.IsNullOrEmpty(applicationVM.EquipmentLocation.Id))
                {
                    // Create a new address and location
                    location = applicationVM.EquipmentLocation.ToModel();
                    try
                    {
                        var address = CreateOrUpdateAddress(applicationVM.EquipmentLocation.Address);
                        //applicationVM.EquipmentLocation.Address = address.ToViewModel();

                        if (address != null)
                        {
                            // bind the address.
                            location.LocationAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", address.BcgovCustomaddressid);
                        }

                        // bind the address to the current account.
                        location.BusinessProfileODataBind = _dynamicsClient.GetEntityURI("accounts", applicationVM.applicant.id);

                        location = _dynamicsClient.Locations.Create(location);
                        applicationVM.EquipmentLocation.Id = location.BcgovLocationid;
                    }
                    catch (OdataerrorException odee)
                    {
                        _logger.LogError(LoggingEvents.Error, "Error creating location");
                        _logger.LogError("Request:");
                        _logger.LogError(odee.Request.Content);
                        _logger.LogError("Response:");
                        _logger.LogError(odee.Response.Content);
                        throw new OdataerrorException("Error creating the location");
                    }
                }
                else
                {
                    // Get existing location
                    Guid.TryParse(applicationVM.EquipmentLocation.Id, out Guid locationId);
                    location = _dynamicsClient.GetLocationById(locationId);
                    if (location == null)
                    {
                        return new NotFoundResult();
                    }
                }

                // Equipment Location record creation
                // set values
                equipmentLocation = new MicrosoftDynamicsCRMbcgovEquipmentlocation();
                equipmentLocation.BcgovName = applicationVM.EquipmentLocation.Name;
                equipmentLocation.BcgovFromwhen = DateTime.Now;   // should come from applicationVM.EquipmentLocation.FromWhen;
                if (string.IsNullOrEmpty(applicationVM.EquipmentLocation.SettingDescription))
                {
                    equipmentLocation.BcgovSettingdescription = "Setting Description value was null - " + DateTime.Now.ToLocalTime();
                }
                else
                {
                    equipmentLocation.BcgovSettingdescription = applicationVM.EquipmentLocation.SettingDescription;
                }
                //bind Equipment and Location records
                equipmentLocation.EquipmentODataBind = _dynamicsClient.GetEntityURI("bcgov_equipments", equipment.BcgovEquipmentid);
                equipmentLocation.LocationODataBind = _dynamicsClient.GetEntityURI("bcgov_locations", location.BcgovLocationid);
                // create new Equipment Location record
                try
                {
                    equipmentLocation = _dynamicsClient.Equipmentlocations.Create(equipmentLocation);
                    applicationVM.EquipmentLocation.Id = location.BcgovLocationid;
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError(LoggingEvents.Error, "Error creating Equipment location record");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                    throw new OdataerrorException("Error creating the Equipment location record");
                }

                // Set current location of the Equipment record
                try
                {
                    OdataId odataId = new OdataId()
                    {
                        OdataIdProperty = _dynamicsClient.GetEntityURI("bcgov_equipments", applicationVM.EquipmentRecord.Id)
                    };

                    _dynamicsClient.Locations.AddReference(location.BcgovLocationid, "bcgov_location_equipment_CurrentLocation", odataId);

                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError(LoggingEvents.Error, "Error binding current location of the Equipment record");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                    throw new OdataerrorException("Error binding current location of the Equipment record");
                }

                return Json(equipmentLocation.ToViewModel());
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ca"></param>
        /// <returns></returns>
        private MicrosoftDynamicsCRMbcgovCustomaddress CreateOrUpdateAddress(ViewModels.CustomAddress ca)
        {
            MicrosoftDynamicsCRMbcgovCustomaddress address = null;
            if (ca != null)
            {
                address = ca.ToModel();
                if (address.HasValue())
                {
                    if (string.IsNullOrEmpty(ca.Id))
                    {
                        // create an address.                        
                        try
                        {
                            address = _dynamicsClient.Customaddresses.Create(address);
                            ca.Id = address.BcgovCustomaddressid;
                        }
                        catch (OdataerrorException odee)
                        {
                            _logger.LogError(LoggingEvents.Error, "Error creating custom address");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                            throw new OdataerrorException("Error creating the custom address");
                        }
                    }
                    else
                    {
                        // update
                        try
                        {
                            _dynamicsClient.Customaddresses.Update(ca.Id, address);
                        }
                        catch (OdataerrorException odee)
                        {
                            _logger.LogError(LoggingEvents.Error, "Error updating custom address");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                            throw new OdataerrorException("Error updating the custom address");
                        }
                    }
                }
            }
            else
            {
                _logger.LogError(LoggingEvents.Error, "ViewModels.CustomAddress object cannot be null");
                throw new Exception("ViewModels.CustomAddress object cannot be null");
            }

            return address;
        }
    }
}
