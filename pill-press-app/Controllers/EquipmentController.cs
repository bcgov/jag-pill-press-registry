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

            if (!string.IsNullOrEmpty(id))
            {
                Guid ApplicationId = Guid.Parse(id);
                // query the Dynamics system to get the Equipment record.
                MicrosoftDynamicsCRMequipment equipment = _dynamicsClient.GetEquipmentByIdWithChildren(ApplicationId);
                
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
        /// Update a legal entity
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipment([FromBody] ViewModels.Equipment item, string id)
        {
            if (id != null && item.Id != null && id != item.Id)
            {
                return BadRequest();
            }

            // get the Equipment
            Guid ApplicationId = Guid.Parse(id);

            MicrosoftDynamicsCRMincident equipment = _dynamicsClient.GetApplicationByIdWithChildren(ApplicationId);
            if (equipment == null)
            {
                return new NotFoundResult();
            }

            // get UserSettings from the session
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

            // Get the current account
            var account = await _dynamicsClient.GetAccountById(Guid.Parse(userSettings.AccountId));

            MicrosoftDynamicsCRMincident patchApplication = new MicrosoftDynamicsCRMincident();
            patchApplication.CopyValues(item);

            // allow the user to change the status to Pending if it is Draft.
            if (equipment.Statuscode != null && equipment.Statuscode == (int?) ViewModels.ApplicationStatusCodes.Draft && item.statuscode == ViewModels.ApplicationStatusCodes.Pending)
            {
                patchApplication.Statuscode = (int?)ViewModels.ApplicationStatusCodes.Pending;
            }

            
            try
            {
                await _dynamicsClient.Incidents.UpdateAsync(ApplicationId.ToString(), patchApplication);
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError("Error updating Equipment");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
            }

            // determine if there are any changes to the contacts.

            if (item.BusinessContacts != null)
            {
                foreach (var businessContactViewModel in item.BusinessContacts)
                {
                    if (businessContactViewModel.contact != null)
                    {
                        // create the contact if necessary.
                        var contact = businessContactViewModel.contact.ToModel();
                        if (string.IsNullOrEmpty(businessContactViewModel.contact.id))
                        {
                            // create a contact.                        
                            try
                            {
                                contact = _dynamicsClient.Contacts.Create(contact);
                                businessContactViewModel.contact.id = contact.Contactid;
                            }
                            catch (OdataerrorException odee)
                            {
                                _logger.LogError(LoggingEvents.Error, "Error creating contact");
                                _logger.LogError("Request:");
                                _logger.LogError(odee.Request.Content);
                                _logger.LogError("Response:");
                                _logger.LogError(odee.Response.Content);
                                throw new OdataerrorException("Error creating contact");
                            }
                        }
                        else
                        {
                            // update
                            try
                            {
                                _dynamicsClient.Contacts.Update(businessContactViewModel.contact.id, contact);
                            }
                            catch (OdataerrorException odee)
                            {
                                _logger.LogError(LoggingEvents.Error, "Error updating contact");
                                _logger.LogError("Request:");
                                _logger.LogError(odee.Request.Content);
                                _logger.LogError("Response:");
                                _logger.LogError(odee.Response.Content);
                                throw new OdataerrorException("Error updating the contact.");
                            }
                        }

                        // force the account to be the current account
                        businessContactViewModel.account = account.ToViewModel();
                        
                        MicrosoftDynamicsCRMbcgovBusinesscontact businessContact = businessContactViewModel.ToModel(_dynamicsClient);

                        if (string.IsNullOrEmpty(businessContactViewModel.id))
                        {
                            
                            try
                            {
                                businessContact = _dynamicsClient.Businesscontacts.Create(businessContact);
                                businessContactViewModel.id = businessContact.BcgovBusinesscontactid;
                            }
                            catch (OdataerrorException odee)
                            {
                                _logger.LogError(LoggingEvents.Error, "Error creating business contact");
                                _logger.LogError("Request:");
                                _logger.LogError(odee.Request.Content);
                                _logger.LogError("Response:");
                                _logger.LogError(odee.Response.Content);
                                throw new OdataerrorException("Error creating business contact");
                            }
                        }
                        else
                        {
                            // update
                            try
                            {
                                _dynamicsClient.Businesscontacts.Update(businessContactViewModel.id, businessContact);
                            }
                            catch (OdataerrorException odee)
                            {
                                _logger.LogError(LoggingEvents.Error, "Error updating business contact");
                                _logger.LogError("Request:");
                                _logger.LogError(odee.Request.Content);
                                _logger.LogError("Response:");
                                _logger.LogError(odee.Response.Content);
                                throw new OdataerrorException("Error updating the business contact.");
                            }
                        }
                    }

                }


                //List<MicrosoftDynamicsCRMbcgovBusinesscontact> itemsToRemove = new List<MicrosoftDynamicsCRMbcgovBusinesscontact>();
                
                foreach (var businessContact in item.BusinessContacts)
                {

                    // TODO: Handle deletes as well as additions.

                    // determine if this item needs to be added.
                    bool notFound = true;

                    // don't bind the record twice.
                    if (equipment.BcgovIncidentBusinesscontact != null && equipment.BcgovIncidentBusinesscontact.Count > 0)
                    {
                        foreach (var bc in equipment.BcgovIncidentBusinesscontact)
                        {
                            if (bc.BcgovBusinesscontactid != null && businessContact.id == bc.BcgovBusinesscontactid)
                            {
                                notFound = false;
                            }
                        }

                    }


                    if (notFound)
                    {
                        OdataId odataId = new OdataId()
                        {
                            OdataIdProperty = _dynamicsClient.GetEntityURI("bcgov_businesscontacts", businessContact.id)
                        };

                        try
                        {
                            await _dynamicsClient.Incidents.AddReferenceAsync(id, "bcgov_incident_businesscontact", odataId);
                        }
                        catch (OdataerrorException odee)
                        {
                            _logger.LogError(LoggingEvents.Error, "Error updating business contacts");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                            throw new OdataerrorException("Error updating the business contacts.");
                        }

                    }

                }

                // check for any businesscontacts that have to be removed.
                if (equipment.BcgovIncidentBusinesscontact != null)
                {
                    foreach (var ibc in equipment.BcgovIncidentBusinesscontact)
                    {
                        bool notFound = true;
                        foreach (var bc in item.BusinessContacts)
                        {
                            if (ibc.BcgovBusinesscontactid == bc.id)
                            {
                                notFound = false;
                            }
                        }

                        if (notFound)
                        {
                            // remove the item.                            
                            try
                            {
                                await _dynamicsClient.Incidents.RemoveReferenceAsync(id, "bcgov_incident_businesscontact", ibc.BcgovBusinesscontactid);
                            }
                            catch (OdataerrorException odee)
                            {
                                _logger.LogError(LoggingEvents.Error, "Error removing business contacts");
                                _logger.LogError("Request:");
                                _logger.LogError(odee.Request.Content);
                                _logger.LogError("Response:");
                                _logger.LogError(odee.Response.Content);
                                throw new OdataerrorException("Error removing a business contact.");
                            }
                        }

                        
                    }
                }

            }

            

            equipment = _dynamicsClient.GetApplicationByIdWithChildren(ApplicationId);
            return Json(equipment.ToViewModel());
        }

        /// <summary>
        /// Create a Equipment
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("{applicationType}")]
        public async Task<IActionResult> CreateApplication(string applicationType, [FromBody] ViewModels.Equipment item)
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
            MicrosoftDynamicsCRMincident equipment = new MicrosoftDynamicsCRMincident();
            equipment.CopyValues(item);

            if(string.IsNullOrEmpty(applicationType)){ // Default to waiver
                applicationType = "Waiver";
            }

            if (string.IsNullOrEmpty(equipment._bcgovApplicationtypeidValue) || string.IsNullOrEmpty(equipment.ApplicationTypeIdODataBind)) // set to Waiver if it is blank.
            {
                string applicationTypeId = _dynamicsClient.GetApplicationTypeIdByName(applicationType);
                if (applicationTypeId != null)
                {
                    equipment.ApplicationTypeIdODataBind = _dynamicsClient.GetEntityURI("bcgov_applicationtypes", applicationTypeId);
                }                
            }

            // set the author based on the current user.
            equipment.SubmitterODataBind = _dynamicsClient.GetEntityURI("contacts", userSettings.ContactId);

            // Also setup the customer.
            var account = await _dynamicsClient.GetAccountById(Guid.Parse(userSettings.AccountId));
            //equipment.CustomeridAccount = account;
            equipment.CustomerIdAccountODataBind = _dynamicsClient.GetEntityURI("accounts", userSettings.AccountId);
            equipment.Statuscode = (int?) ViewModels.ApplicationStatusCodes.Draft;
            try
            {
                equipment = await _dynamicsClient.Incidents.CreateAsync(equipment);
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
        public async Task<IActionResult> DeleteApplication(string id)
        {
            _logger.LogDebug(LoggingEvents.HttpPost, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);

            // verify the currently logged in user has access to this account
            Guid applicationId = new Guid(id);

            MicrosoftDynamicsCRMincident equipment = _dynamicsClient.GetApplicationById(applicationId);
            if (equipment == null)
            {
                _logger.LogWarning(LoggingEvents.NotFound, "Equipment NOT found.");
                return new NotFoundResult();
            }

            if (!UserDynamicsExtensions.CurrentUserHasAccessToApplication(applicationId, _httpContextAccessor, _dynamicsClient))
            {
                _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to the equipment.");
                return new NotFoundResult();
            }

            // get the account            
            try
            {
                await _dynamicsClient.Incidents.DeleteAsync(applicationId.ToString());
                _logger.LogDebug(LoggingEvents.HttpDelete, "Equipment deleted: " + applicationId.ToString());
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError(LoggingEvents.Error, "Error deleting the equipment: " + applicationId.ToString());
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
                throw new OdataerrorException("Error deleting the account: " + applicationId.ToString());
            }

            _logger.LogDebug(LoggingEvents.HttpDelete, "No content returned.");
            return NoContent(); // 204 
        }

    }
}
