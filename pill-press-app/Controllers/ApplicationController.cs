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
    public class ApplicationController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IDynamicsClient _dynamicsClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;

        public ApplicationController(IConfiguration configuration, IDynamicsClient dynamicsClient, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            Configuration = configuration;
            _dynamicsClient = dynamicsClient;
            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger(typeof(ApplicationController));
            this._env = env;
        }


        /// <summary>
        /// Get a specific legal entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetApplication(string id)
        {
            ViewModels.Application result = null;

            if (!string.IsNullOrEmpty(id))
            {
                Guid ApplicationId = Guid.Parse(id);
                // query the Dynamics system to get the Application record.
                MicrosoftDynamicsCRMincident application = _dynamicsClient.GetApplicationByIdWithChildren(ApplicationId);
                
                if (application != null)
                {
                    result = application.ToViewModel();
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
        /// Get a license application by applicant id
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        private async Task<List<ViewModels.Application>> GetApplicationsByApplicant(string applicantId)
        {
            List<ViewModels.Application> result = new List<ViewModels.Application>();
            IEnumerable<MicrosoftDynamicsCRMincident> dynamicsApplicationList = null;

            var filter = $"_customerid_value eq {applicantId}";
            // filter += $" and statuscode ne {(int)AdoxioApplicationStatusCodes.Denied}";
            var expand = new List<string> { "customerid_account", "bcgov_ApplicationTypeId" };
            try
            {
                dynamicsApplicationList = _dynamicsClient.Incidents.Get(filter: filter, expand: expand, orderby: new List<string> { "modifiedon desc" }).Value;
            }
            catch (OdataerrorException)
            {
                dynamicsApplicationList = null;
            }
            

            if (dynamicsApplicationList != null)
            {
                foreach (MicrosoftDynamicsCRMincident dynamicsApplication in dynamicsApplicationList)
                {                    
                    result.Add(dynamicsApplication.ToViewModel());                    
                }
            }
            return result;
        }

        /// GET all applications in Dynamics for the current user
        [HttpGet("current")]
        public async Task<JsonResult> GetCurrentUserDyanamicsApplications()
        {
            // get the current user.
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

            // GET all applications in Dynamics by applicant using the account Id assigned to the user logged in
            List<ViewModels.Application> applications = await GetApplicationsByApplicant(userSettings.AccountId);
            return Json(applications);
        }

        /// <summary>
        /// Update a legal entity
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication([FromBody] ViewModels.Application item, string id)
        {
            if (id != null && item.id != null && id != item.id)
            {
                return BadRequest();
            }

            // get the Application
            Guid ApplicationId = Guid.Parse(id);

            MicrosoftDynamicsCRMincident application = _dynamicsClient.GetApplicationByIdWithChildren(ApplicationId);
            if (application == null)
            {
                return new NotFoundResult();
            }

            // get UserSettings from the session
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

            // Get the current account
            var account = await _dynamicsClient.GetAccountById(Guid.Parse(userSettings.AccountId));

            // setup custom addresses.
            var BCSellersAddress = CreateOrUpdateAddress(item.BCSellersAddress);
            var OutsideBCSellersAddress = CreateOrUpdateAddress(item.OutsideBCSellersAddress);
            var ImportersAddress = CreateOrUpdateAddress(item.ImportersAddress);
            var OriginatingSellersAddress = CreateOrUpdateAddress(item.OriginatingSellersAddress);
            var AddressofBusinessthathasGivenorLoaned = CreateOrUpdateAddress(item.AddressofBusinessthathasGivenorLoaned);
            var AddressofBusinessThatHasRentedorLeased = CreateOrUpdateAddress(item.AddressofBusinessThatHasRentedorLeased);


            MicrosoftDynamicsCRMincident patchApplication = new MicrosoftDynamicsCRMincident();
            patchApplication.CopyValues(item);

            // allow the user to change the status to Pending if it is Draft.
            if (application.Statuscode != null && application.Statuscode == (int?) ViewModels.ApplicationStatusCodes.Draft && item.statuscode == ViewModels.ApplicationStatusCodes.Pending)
            {
                patchApplication.Statuscode = (int?)ViewModels.ApplicationStatusCodes.Pending;
            }

            // patch the data bindings
            if (BCSellersAddress != null && 
                (application._bcgovBcsellersaddressValue == null || application._bcgovBcsellersaddressValue != BCSellersAddress.BcgovCustomaddressid))
            {
                if (application._bcgovBcsellersaddressValue != null)
                {
                    // delete an existing reference.
                    _dynamicsClient.Incidents.RemoveReference(id, "bcgov_BCSellersAddress", null);
                }
                patchApplication.BCSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", BCSellersAddress.BcgovCustomaddressid);
            }

            if (OutsideBCSellersAddress != null &&
                (application._bcgovOutsidebcsellersaddressValue == null || application._bcgovOutsidebcsellersaddressValue != OutsideBCSellersAddress.BcgovCustomaddressid))
            {
                if (application._bcgovOutsidebcsellersaddressValue != null)
                {
                    // delete an existing reference.
                    _dynamicsClient.Incidents.RemoveReference(id, "bcgov_OutsideBCSellersAddress", null);
                }
                patchApplication.OutsideBCSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", OutsideBCSellersAddress.BcgovCustomaddressid);
            }

            if (ImportersAddress != null &&
                (application._bcgovImportersaddressValue == null || application._bcgovImportersaddressValue != ImportersAddress.BcgovCustomaddressid))
            {
                if (application._bcgovImportersaddressValue != null)
                {
                    // delete an existing reference.
                    _dynamicsClient.Incidents.RemoveReference(id, "bcgov_ImportersAddress", null);
                }

                patchApplication.ImportersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", ImportersAddress.BcgovCustomaddressid);
            }

            if (OriginatingSellersAddress != null &&
                (application._bcgovOriginatingsellersaddressValue == null || application._bcgovOriginatingsellersaddressValue != OriginatingSellersAddress.BcgovCustomaddressid))
            {
                if (application._bcgovOriginatingsellersaddressValue != null)
                {
                    // delete an existing reference.
                    _dynamicsClient.Incidents.RemoveReference(id, "bcgov_OriginatingSellersAddress", null);
                }

                patchApplication.OriginatingSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", OriginatingSellersAddress.BcgovCustomaddressid);
            }

            if (AddressofBusinessthathasGivenorLoaned != null &&
                (application._bcgovAddressofbusinessthathasgivenorloanedValue == null || application._bcgovAddressofbusinessthathasgivenorloanedValue != AddressofBusinessthathasGivenorLoaned.BcgovCustomaddressid))
            {
                if (application._bcgovAddressofbusinessthathasgivenorloanedValue != null)
                {
                    // delete an existing reference.
                    _dynamicsClient.Incidents.RemoveReference(id, "bcgov_AddressofBusinessthathasGivenorLoaned", null);
                }
                patchApplication.AddressofBusinessthathasGivenorLoanedODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressofBusinessthathasGivenorLoaned.BcgovCustomaddressid);
            }

            if (AddressofBusinessThatHasRentedorLeased != null &&
                (application._bcgovAddressofbusinessthathasrentedorleasedValue == null || application._bcgovAddressofbusinessthathasrentedorleasedValue != AddressofBusinessThatHasRentedorLeased.BcgovCustomaddressid))
            {
                if (application._bcgovAddressofbusinessthathasrentedorleasedValue != null)
                {
                    // delete an existing reference.
                    _dynamicsClient.Incidents.RemoveReference(id, "bcgov_AddressofBusinessthathasRentedorLeased", null);
                }
                patchApplication.AddressofBusinessThatHasRentedorLeasedODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressofBusinessThatHasRentedorLeased.BcgovCustomaddressid);
            }

            try
            {
                await _dynamicsClient.Incidents.UpdateAsync(ApplicationId.ToString(), patchApplication);
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError("Error updating Application");
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
                    if (application.BcgovIncidentBusinesscontact != null && application.BcgovIncidentBusinesscontact.Count > 0)
                    {
                        foreach (var bc in application.BcgovIncidentBusinesscontact)
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
                if (application.BcgovIncidentBusinesscontact != null)
                {
                    foreach (var ibc in application.BcgovIncidentBusinesscontact)
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

            

            application = _dynamicsClient.GetApplicationByIdWithChildren(ApplicationId);
            return Json(application.ToViewModel());
        }

        private MicrosoftDynamicsCRMbcgovLocation CreateOrUpdateLocation(ViewModels.Location item)
        {
            MicrosoftDynamicsCRMbcgovLocation location = null;
            // Primary Contact
            if (item != null)
            {
                location = item.ToModel();

                // handle the address.
                var address = CreateOrUpdateAddress(item.Address);
                item.Address = address.ToViewModel();

                if (string.IsNullOrEmpty(item.Id))
                {
                    if (address != null)
                    {
                        // bind the address.
                        location.LocationAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", address.BcgovCustomaddressid);
                    }                    

                    // create a location                        
                    try
                    {
                        location = _dynamicsClient.Locations.Create(location);
                        item.Id = location.BcgovLocationid;
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
                    // update
                    try
                    {
                        _dynamicsClient.Locations.Update(item.Id, location);
                    }
                    catch (OdataerrorException odee)
                    {
                        _logger.LogError(LoggingEvents.Error, "Error updating address");
                        _logger.LogError("Request:");
                        _logger.LogError(odee.Request.Content);
                        _logger.LogError("Response:");
                        _logger.LogError(odee.Response.Content);
                        throw new OdataerrorException("Error updating the address");
                    }
                }
            }
            return location;
        }


        private MicrosoftDynamicsCRMbcgovCustomaddress CreateOrUpdateAddress(ViewModels.CustomAddress ca)
        {
            MicrosoftDynamicsCRMbcgovCustomaddress address = null;
            // Primary Contact
            if (ca != null)
            {
                address = ca.ToModel();
                if (string.IsNullOrEmpty(ca.Id))
                {
                    // create an account.                        
                    try
                    {
                        address = _dynamicsClient.Customaddresses.Create(address);
                        ca.Id = address.BcgovCustomaddressid;
                    }
                    catch (OdataerrorException odee)
                    {
                        _logger.LogError(LoggingEvents.Error, "Error creating address");
                        _logger.LogError("Request:");
                        _logger.LogError(odee.Request.Content);
                        _logger.LogError("Response:");
                        _logger.LogError(odee.Response.Content);
                        throw new OdataerrorException("Error creating the address");
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
                        _logger.LogError(LoggingEvents.Error, "Error updating address");
                        _logger.LogError("Request:");
                        _logger.LogError(odee.Request.Content);
                        _logger.LogError("Response:");
                        _logger.LogError(odee.Response.Content);
                        throw new OdataerrorException("Error updating the address");
                    }
                }
            }
            return address;
        }

        /// <summary>
        /// Create a Application
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("{applicationType}")]
        public async Task<IActionResult> CreateApplication(string applicationType, [FromBody] ViewModels.Application item)
        {

            // get UserSettings from the session
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

            // first check to see that a Application exists.
            string ApplicationSiteminderGuid = userSettings.SiteMinderGuid;
            if (ApplicationSiteminderGuid == null || ApplicationSiteminderGuid.Length == 0)
            {
                _logger.LogError(LoggingEvents.Error, "No Application Siteminder Guid exernal id");
                throw new Exception("Error. No ApplicationSiteminderGuid exernal id");
            }

            // setup custom addresses.
            var BCSellersAddress = CreateOrUpdateAddress(item.BCSellersAddress);
            var OutsideBCSellersAddress = CreateOrUpdateAddress(item.OutsideBCSellersAddress);
            var ImportersAddress = CreateOrUpdateAddress(item.ImportersAddress);
            var OriginatingSellersAddress = CreateOrUpdateAddress(item.OriginatingSellersAddress);
            var AddressofBusinessthathasGivenorLoaned = CreateOrUpdateAddress(item.AddressofBusinessthathasGivenorLoaned);
            var AddressofBusinessThatHasRentedorLeased = CreateOrUpdateAddress(item.AddressofBusinessThatHasRentedorLeased);

            var EquipmentLocation = CreateOrUpdateLocation(item.EquipmentLocation);

            // create a new Application.
            MicrosoftDynamicsCRMincident application = new MicrosoftDynamicsCRMincident();
            application.CopyValues(item);

            if(string.IsNullOrEmpty(applicationType)){ // Default to waiver
                applicationType = "Waiver";
            }

            if (string.IsNullOrEmpty(application._bcgovApplicationtypeidValue) || string.IsNullOrEmpty(application.ApplicationTypeIdODataBind)) // set to Waiver if it is blank.
            {
                string applicationTypeId = _dynamicsClient.GetApplicationTypeIdByName(applicationType);
                if (applicationTypeId != null)
                {
                    application.ApplicationTypeIdODataBind = _dynamicsClient.GetEntityURI("bcgov_applicationtypes", applicationTypeId);
                }                
            }

            // set the author based on the current user.
            application.SubmitterODataBind = _dynamicsClient.GetEntityURI("contacts", userSettings.ContactId);
        
            // Also setup the customer.
            var account = await _dynamicsClient.GetAccountById(Guid.Parse(userSettings.AccountId));
            
            application.CustomerIdAccountODataBind = _dynamicsClient.GetEntityURI("accounts", userSettings.AccountId);

            // bind the addresses. 
            if (BCSellersAddress != null)
            {
                application.BCSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", BCSellersAddress.BcgovCustomaddressid);
            }
            
            if (OutsideBCSellersAddress != null)
            {
                application.OutsideBCSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", OutsideBCSellersAddress.BcgovCustomaddressid);
            }
            
            if (ImportersAddress != null)
            {
                application.ImportersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", ImportersAddress.BcgovCustomaddressid);
            }
            
            if (OriginatingSellersAddress != null)
            {
                application.OriginatingSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", OriginatingSellersAddress.BcgovCustomaddressid);
            }
            if (AddressofBusinessthathasGivenorLoaned != null)
            {
                application.AddressofBusinessthathasGivenorLoanedODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressofBusinessthathasGivenorLoaned.BcgovCustomaddressid);
            }
            
            if (AddressofBusinessThatHasRentedorLeased != null)
            {
                application.AddressofBusinessThatHasRentedorLeasedODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressofBusinessThatHasRentedorLeased.BcgovCustomaddressid);
            }
            


            application.Statuscode = (int?) ViewModels.ApplicationStatusCodes.Draft;
            try
            {
                application = await _dynamicsClient.Incidents.CreateAsync(application);
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError("Error creating Application");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
                throw new OdataerrorException("Error creating Application");
            }
            Guid applicationId = Guid.Parse(application.Incidentid);

            
           


            application = _dynamicsClient.GetApplicationByIdWithChildren(applicationId);

            return Json(application.ToViewModel());
        }

        /// <summary>
        /// Delete an application.  Using a HTTP Post to avoid Siteminder issues with DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteApplication(string id)
        {
            _logger.LogDebug(LoggingEvents.HttpPost, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);

            // verify the currently logged in user has access to this account
            Guid applicationId = Guid.Parse(id);

            MicrosoftDynamicsCRMincident application = _dynamicsClient.GetApplicationById(applicationId);
            if (application == null)
            {
                _logger.LogWarning(LoggingEvents.NotFound, "Application NOT found.");
                return new NotFoundResult();
            }

            if (!UserDynamicsExtensions.CurrentUserHasAccessToApplication(applicationId, _httpContextAccessor, _dynamicsClient))
            {
                _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to the application.");
                return new NotFoundResult();
            }

            // get the account            
            try
            {
                await _dynamicsClient.Incidents.DeleteAsync(applicationId.ToString());
                _logger.LogDebug(LoggingEvents.HttpDelete, "Application deleted: " + applicationId.ToString());
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError(LoggingEvents.Error, "Error deleting the application: " + applicationId.ToString());
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
