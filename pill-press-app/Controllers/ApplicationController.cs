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
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
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
        private readonly SharePointFileManager _sharePointFileManager;

        public ApplicationController(SharePointFileManager sharePointFileManager, IConfiguration configuration, IDynamicsClient dynamicsClient, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            Configuration = configuration;
            _dynamicsClient = dynamicsClient;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger(typeof(ApplicationController));
            _sharePointFileManager = sharePointFileManager;
            
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
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid applicationId))
            {             
                // query the Dynamics system to get the Application record.
                MicrosoftDynamicsCRMincident application = _dynamicsClient.GetApplicationByIdWithChildren(applicationId);
                
                if (application != null)
                {
                    // verify the currently logged in user has access to this account                    

                    if (!UserDynamicsExtensions.CurrentUserHasAccessToApplication(applicationId, _httpContextAccessor, _dynamicsClient))
                    {
                        _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to the application.");
                        return new NotFoundResult();
                    }
                    else
                    {
                        result = application.ToViewModel();
                    }
                    
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
        private List<ViewModels.Application> GetApplicationsByApplicant(string applicantId)
        {
            List<ViewModels.Application> result = new List<ViewModels.Application>();
            IEnumerable<MicrosoftDynamicsCRMincident> dynamicsApplicationList = null;

            var filter = $"_customerid_value eq {applicantId}";
            // filter += $" and statuscode ne {(int)AdoxioApplicationStatusCodes.Denied}";
            // fields will be expanded on individual calls.
            //var expand = new List<string> { "customerid_account", "bcgov_ApplicationTypeId" };
            try
            {
                dynamicsApplicationList = _dynamicsClient.Incidents.Get(filter: filter, orderby: new List<string> { "modifiedon desc" }).Value;
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError(LoggingEvents.Error, "Error getting Application");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
                dynamicsApplicationList = null;
            }
            

            if (dynamicsApplicationList != null)
            {
                foreach (MicrosoftDynamicsCRMincident dynamicsApplication in dynamicsApplicationList)
                {
                    string id = dynamicsApplication.Incidentid;
                    var application = _dynamicsClient.GetApplicationByIdWithChildren(id);
                    result.Add(application.ToViewModel());                      
                }
            }
            return result;
        }

        /// GET all applications in Dynamics for the current user
        [HttpGet("current")]
        public JsonResult GetCurrentUserDyanamicsApplications()
        {
            // get the current user.
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

            // GET all applications in Dynamics by applicant using the account Id assigned to the user logged in
            List<ViewModels.Application> applications = GetApplicationsByApplicant(userSettings.AccountId);
            return Json(applications);
        }

        /// <summary>
        /// Update a legal entity
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateApplication([FromBody] ViewModels.Application item, string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid applicationId))
            {
                // get the Application

                MicrosoftDynamicsCRMincident application = _dynamicsClient.GetApplicationByIdWithChildren(applicationId);
                if (application == null)
                {
                    return new NotFoundResult();
                }

                // get UserSettings from the session
                string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
                UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

                // Get the current account
                var account = _dynamicsClient.GetAccountByIdWithChildren(Guid.Parse(userSettings.AccountId));

                // verify the current user has access to the given application
                if (!UserDynamicsExtensions.CurrentUserHasAccessToApplication(applicationId, _httpContextAccessor, _dynamicsClient))
                {
                    _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to the application.");
                    return new NotFoundResult();
                }

                // setup custom addresses.
                var BCSellersAddress = CreateOrUpdateAddress(item.BCSellersAddress);
                var OutsideBCSellersAddress = CreateOrUpdateAddress(item.OutsideBCSellersAddress);
                var ImportersAddress = CreateOrUpdateAddress(item.ImportersAddress);
                var OriginatingSellersAddress = CreateOrUpdateAddress(item.OriginatingSellersAddress);
                var AddressofBusinessthathasGivenorLoaned = CreateOrUpdateAddress(item.AddressofBusinessthathasGivenorLoaned);
                var AddressofBusinessThatHasRentedorLeased = CreateOrUpdateAddress(item.AddressofBusinessThatHasRentedorLeased);
                var AddressofPersonBusiness = CreateOrUpdateAddress(item.AddressofPersonBusiness);
                var AddressWhereEquipmentWasDestroyed = CreateOrUpdateAddress(item.AddressWhereEquipmentWasDestroyed);
                var CivicAddressOfPurchaser = CreateOrUpdateAddress(item.civicAddressOfPurchaser);
                var PurchasersCivicAddress = CreateOrUpdateAddress(item.purchasersCivicAddress);
                var PurchasersBusinessAddress = CreateOrUpdateAddress(item.purchasersBusinessAddress);

                var EquipmentLocation = CreateOrUpdateLocation(id, item.EquipmentLocation, userSettings.AccountId);

                MicrosoftDynamicsCRMincident patchApplication = new MicrosoftDynamicsCRMincident();
                patchApplication.CopyValues(item);

                // allow the user to change the status to Pending if it is Draft.
                if (application.Statuscode != null && application.Statuscode == (int?)ViewModels.ApplicationStatusCodes.Draft && item.statuscode == ViewModels.ApplicationStatusCodes.Pending)
                {
                    patchApplication.Statuscode = (int?)ViewModels.ApplicationStatusCodes.Pending;
                    // force submitted date if we are changing from Draft to Pending.
                    patchApplication.BcgovSubmitteddate = DateTimeOffset.Now;

                }

                // patch the data bindings
                if (BCSellersAddress.HasValue() && BCSellersAddress.BcgovCustomaddressid != null &&
                    (application._bcgovBcsellersaddressValue == null || application._bcgovBcsellersaddressValue != BCSellersAddress.BcgovCustomaddressid))
                {
                    if (application._bcgovBcsellersaddressValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_BCSellersAddress", null);
                    }
                    patchApplication.BCSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", BCSellersAddress.BcgovCustomaddressid);
                }

                if (AddressWhereEquipmentWasDestroyed.HasValue() && AddressWhereEquipmentWasDestroyed.BcgovCustomaddressid != null &&
                    (application._bcgovAddresswhereequipmentwasdestroyedValue == null || application._bcgovAddresswhereequipmentwasdestroyedValue != AddressWhereEquipmentWasDestroyed.BcgovCustomaddressid))
                {
                    if (application._bcgovAddresswhereequipmentwasdestroyedValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_AddressWhereEquipmentWasDestroyed", null);
                    }
                    patchApplication.BcgovAddressWhereEquipmentWasDestroyedODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressWhereEquipmentWasDestroyed.BcgovCustomaddressid);
                }

                if (AddressofPersonBusiness.HasValue() && AddressofPersonBusiness.BcgovCustomaddressid != null &&
                    (application._bcgovAddressofpersonbusinessValue == null || application._bcgovAddressofpersonbusinessValue != AddressofPersonBusiness.BcgovCustomaddressid))
                {
                    if (application._bcgovAddressofpersonbusinessValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_AddressofPersonBusiness", null);
                    }
                    patchApplication.AddressofPersonBusinessODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressofPersonBusiness.BcgovCustomaddressid);
                }

                if (OutsideBCSellersAddress.HasValue() && OutsideBCSellersAddress.BcgovCustomaddressid != null &&
                    (application._bcgovOutsidebcsellersaddressValue == null || application._bcgovOutsidebcsellersaddressValue != OutsideBCSellersAddress.BcgovCustomaddressid))
                {
                    if (application._bcgovOutsidebcsellersaddressValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_OutsideBCSellersAddress", null);
                    }
                    patchApplication.OutsideBCSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", OutsideBCSellersAddress.BcgovCustomaddressid);
                }

                if (ImportersAddress.HasValue() && ImportersAddress.BcgovCustomaddressid != null &&
                    (application._bcgovImportersaddressValue == null || application._bcgovImportersaddressValue != ImportersAddress.BcgovCustomaddressid))
                {
                    if (application._bcgovImportersaddressValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_ImportersAddress", null);
                    }

                    patchApplication.ImportersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", ImportersAddress.BcgovCustomaddressid);
                }

                if (OriginatingSellersAddress.HasValue() && OriginatingSellersAddress.BcgovCustomaddressid != null &&
                    (application._bcgovOriginatingsellersaddressValue == null || application._bcgovOriginatingsellersaddressValue != OriginatingSellersAddress.BcgovCustomaddressid))
                {
                    if (application._bcgovOriginatingsellersaddressValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_OriginatingSellersAddress", null);
                    }

                    patchApplication.OriginatingSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", OriginatingSellersAddress.BcgovCustomaddressid);
                }

                if (AddressofBusinessthathasGivenorLoaned.HasValue() &&
                    (application._bcgovAddressofbusinessthathasgivenorloanedValue == null || application._bcgovAddressofbusinessthathasgivenorloanedValue != AddressofBusinessthathasGivenorLoaned.BcgovCustomaddressid))
                {
                    if (application._bcgovAddressofbusinessthathasgivenorloanedValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_AddressofBusinessthathasGivenorLoaned", null);
                    }
                    patchApplication.AddressofBusinessthathasGivenorLoanedODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressofBusinessthathasGivenorLoaned.BcgovCustomaddressid);
                }

                if (AddressofBusinessThatHasRentedorLeased.HasValue() &&
                    (application._bcgovAddressofbusinessthathasrentedorleasedValue == null || application._bcgovAddressofbusinessthathasrentedorleasedValue != AddressofBusinessThatHasRentedorLeased.BcgovCustomaddressid))
                {
                    if (application._bcgovAddressofbusinessthathasrentedorleasedValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_AddressofBusinessthathasRentedorLeased", null);
                    }
                    patchApplication.AddressofBusinessThatHasRentedorLeasedODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressofBusinessThatHasRentedorLeased.BcgovCustomaddressid);
                }


                if (PurchasersCivicAddress.HasValue() &&
                    (application._bcgovCivicaddressofpurchaserValue == null || application._bcgovCivicaddressofpurchaserValue != PurchasersCivicAddress.BcgovCustomaddressid))
                {
                    if (application._bcgovCivicaddressofpurchaserValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_PurchasersCivicAddress", null);
                    }
                    patchApplication.BcgovPurchasersCivicAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", PurchasersCivicAddress.BcgovCustomaddressid);
                }


                if (CivicAddressOfPurchaser.HasValue() &&
                    (application._bcgovCivicaddressofpurchaserValue == null || application._bcgovCivicaddressofpurchaserValue != CivicAddressOfPurchaser.BcgovCustomaddressid))
                {
                    if (application._bcgovCivicaddressofpurchaserValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_CivicAddressofPurchaser", null);
                    }
                    patchApplication.BcgovCivicAddressofPurchaserODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", CivicAddressOfPurchaser.BcgovCustomaddressid);
                }


                if (PurchasersBusinessAddress.HasValue() &&
                    (application._bcgovPurchasersbusinessaddressValue == null || application._bcgovPurchasersbusinessaddressValue != PurchasersBusinessAddress.BcgovCustomaddressid))
                {
                    if (application._bcgovPurchasersbusinessaddressValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_PurchasersBusinessAddress", null);
                    }
                    patchApplication.BcgovPurchasersBusinessAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", PurchasersBusinessAddress.BcgovCustomaddressid);
                }

                if (EquipmentLocation.HasValue() &&
                    (application._bcgovEquipmentlocationValue == null || application._bcgovEquipmentlocationValue != EquipmentLocation.BcgovLocationid))
                {
                    if (application._bcgovEquipmentlocationValue != null)
                    {
                        // delete an existing reference.
                        _dynamicsClient.Incidents.RemoveReference(id, "bcgov_EquipmentLocation", null);
                    }
                    patchApplication.EquipmentLocationODataBind = _dynamicsClient.GetEntityURI("bcgov_locations", EquipmentLocation.BcgovLocationid);
                }

                try
                {
                    _dynamicsClient.Incidents.Update(applicationId.ToString(), patchApplication);
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
                                _dynamicsClient.Incidents.AddReference(id, "bcgov_incident_businesscontact", odataId);
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
                                    _dynamicsClient.Incidents.RemoveReference(id, "bcgov_incident_businesscontact", ibc.BcgovBusinesscontactid);
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
                application = _dynamicsClient.GetApplicationByIdWithChildren(applicationId);
                return Json(application.ToViewModel());
            }
            else
            {
                return BadRequest();
            }
        }

        private MicrosoftDynamicsCRMbcgovLocation CreateOrUpdateLocation(string incidentId, ViewModels.Location item, string accountId)
        {
            MicrosoftDynamicsCRMbcgovLocation location = null;
            // Primary Contact
            if (item != null)
            {
                location = item.ToModel();
                // handle the address.
                var address = CreateOrUpdateAddress(item.Address);
                item.Address = address.ToViewModel();

                // There are cases where only the child address has a value
                if (location != null && (location.HasValue() || address.HasValue()))
                {                    
                    if (string.IsNullOrEmpty(item.Id))
                    {
                        if (address != null)
                        {
                            // bind the address.
                            location.LocationAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", address.BcgovCustomaddressid);
                        }

                        // bind the current account.
                        location.BusinessProfileODataBind = _dynamicsClient.GetEntityURI("accounts", accountId);
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
                        // now bind the incident to the location.    
                        try
                        {
                            OdataId odataId = new OdataId()
                            {
                                OdataIdProperty = _dynamicsClient.GetEntityURI("incidents", incidentId)
                            };

                            _dynamicsClient.Locations.AddReference(location.BcgovLocationid, "bcgov_location_incident_EquipmentLocation", odataId);

                        }
                        catch (OdataerrorException odee)
                        {
                            _logger.LogError(LoggingEvents.Error, "Error binding location");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                            throw new OdataerrorException("Error binding the location");
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
                    if (address != null)
                    {
                        location.BcgovLocationAddress = address;
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
            var AddressofPersonBusiness = CreateOrUpdateAddress(item.AddressofPersonBusiness);
            var AddressWhereEquipmentWasDestroyed = CreateOrUpdateAddress(item.AddressWhereEquipmentWasDestroyed);
            var CivicAddressOfPurchaser = CreateOrUpdateAddress(item.civicAddressOfPurchaser);
            var PurchasersCivicAddress = CreateOrUpdateAddress(item.purchasersCivicAddress);
            var PurchasersBusinessAddress = CreateOrUpdateAddress(item.purchasersBusinessAddress);

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

            if (item?.EquipmentRecord?.Id != null)
            {
                application.EquipmentRecordODataBind = _dynamicsClient.GetEntityURI("bcgov_equipments", item?.EquipmentRecord?.Id);
            }

            // set the author based on the current user.
            application.SubmitterODataBind = _dynamicsClient.GetEntityURI("contacts", userSettings.ContactId);
        
            // Also setup the customer.
            var account = _dynamicsClient.GetAccountByIdWithChildren(Guid.Parse(userSettings.AccountId));
            
            application.CustomerIdAccountODataBind = _dynamicsClient.GetEntityURI("accounts", userSettings.AccountId);
            
            // bind the addresses. 
            if (BCSellersAddress.HasValue())
            {
                application.BCSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", BCSellersAddress.BcgovCustomaddressid);
            }
            
            if (OutsideBCSellersAddress.HasValue())
            {
                application.OutsideBCSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", OutsideBCSellersAddress.BcgovCustomaddressid);
            }
            
            if (ImportersAddress.HasValue())
            {
                application.ImportersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", ImportersAddress.BcgovCustomaddressid);
            }
            
            if (OriginatingSellersAddress.HasValue())
            {
                application.OriginatingSellersAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", OriginatingSellersAddress.BcgovCustomaddressid);
            }
            if (AddressofBusinessthathasGivenorLoaned.HasValue())
            {
                application.AddressofBusinessthathasGivenorLoanedODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressofBusinessthathasGivenorLoaned.BcgovCustomaddressid);
            }
            
            if (AddressofBusinessThatHasRentedorLeased.HasValue())
            {
                application.AddressofBusinessThatHasRentedorLeasedODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressofBusinessThatHasRentedorLeased.BcgovCustomaddressid);
            }

            if (AddressofPersonBusiness.HasValue())
            {
                application.AddressofPersonBusinessODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressofPersonBusiness.BcgovCustomaddressid);
            }

            if (AddressWhereEquipmentWasDestroyed.HasValue())
            {
                application.BcgovAddressWhereEquipmentWasDestroyedODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", AddressWhereEquipmentWasDestroyed.BcgovCustomaddressid);
            }

            if (CivicAddressOfPurchaser.HasValue())
            {
                application.BcgovCivicAddressofPurchaserODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", CivicAddressOfPurchaser.BcgovCustomaddressid);
            }

            if (PurchasersCivicAddress.HasValue())
            {
                application.BcgovPurchasersCivicAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", PurchasersCivicAddress.BcgovCustomaddressid);
            }

            if (PurchasersBusinessAddress.HasValue())
            {
                application.BcgovPurchasersBusinessAddressODataBind = _dynamicsClient.GetEntityURI("bcgov_customaddresses", PurchasersBusinessAddress.BcgovCustomaddressid);
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
            // bind equipment location

            if (item.EquipmentLocation != null)
            {
                var EquipmentLocation = CreateOrUpdateLocation(application.Incidentid, item.EquipmentLocation, userSettings.AccountId);
                var patchApplication = new MicrosoftDynamicsCRMincident()
                {
                    EquipmentLocationODataBind = _dynamicsClient.GetEntityURI("bcgov_locations", EquipmentLocation.BcgovLocationid)
                };
                try
                {
                    _dynamicsClient.Incidents.Update(application.Incidentid, patchApplication);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError("Error creating Application");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                    throw new OdataerrorException("Error updating Application");
                }
        }
            string serverRelativeUrl = "";

            if (!string.IsNullOrEmpty(_sharePointFileManager.WebName))
            {
                serverRelativeUrl += "/sites/" + _sharePointFileManager.WebName;
            }
            serverRelativeUrl += _sharePointFileManager.GetServerRelativeURL(SharePointFileManager.ApplicationDocumentListTitle, application.GetSharePointFolderName());

            string folderName = application.GetSharePointFolderName();


            // create a SharePointDocumentLocation link

            string name = application.Title + " Application Files";

            // Create the folder
            bool folderExists = await _sharePointFileManager.FolderExists(SharePointFileManager.ApplicationDocumentListTitle, folderName);
            if (!folderExists)
            {
                try
                {
                    var folder = await _sharePointFileManager.CreateFolder(SharePointFileManager.ApplicationDocumentListTitle, folderName);
                }
                catch (Exception e)
                {
                    _logger.LogError("Error creating Sharepoint Folder");
                    _logger.LogError($"List is: {SharePointFileManager.ApplicationDocumentListTitle}");
                    _logger.LogError($"FolderName is: {folderName}");
                    throw e;
                }

            }

            // Create the SharePointDocumentLocation entity
            MicrosoftDynamicsCRMsharepointdocumentlocation mdcsdl = new MicrosoftDynamicsCRMsharepointdocumentlocation()
            {
                Relativeurl = folderName,
                Description = "Application Files",
                Name = name
            };


            try
            {
                mdcsdl = _dynamicsClient.Sharepointdocumentlocations.Create(mdcsdl);
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError("Error creating SharepointDocumentLocation");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
                mdcsdl = null;
            }
            if (mdcsdl != null)
            {
                // add a regardingobjectid.
                //string accountReference = _dynamicsClient.GetEntityURI("accounts", userSettings.AccountId);
                //var patchSharePointDocumentLocation = new MicrosoftDynamicsCRMsharepointdocumentlocation();
                //patchSharePointDocumentLocation.RegardingobjectIdAccountODataBind = accountReference;
                
                // set the parent document library.
                string parentDocumentLibraryReference = GetDocumentLocationReferenceByRelativeURL("incident");

                string incidentURI = _dynamicsClient.GetEntityURI("incidents", applicationId.ToString());
                var patchSharePointDocumentLocationIncident = new MicrosoftDynamicsCRMsharepointdocumentlocation()
                {
                    RegardingobjectIdIncidentODataBind = incidentURI,
                    ParentsiteorlocationSharepointdocumentlocationODataBind = _dynamicsClient.GetEntityURI("sharepointdocumentlocations", parentDocumentLibraryReference)
                };
                

                try
                {
                    _dynamicsClient.Sharepointdocumentlocations.Update(mdcsdl.Sharepointdocumentlocationid, patchSharePointDocumentLocationIncident);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError("Error adding reference SharepointDocumentLocation to application");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                }
                
                string sharePointLocationData = _dynamicsClient.GetEntityURI("sharepointdocumentlocations", mdcsdl.Sharepointdocumentlocationid);                               
                    
                OdataId oDataId = new OdataId()
                {
                    OdataIdProperty = sharePointLocationData
                };
                try
                {
                    _dynamicsClient.Incidents.AddReference(applicationId.ToString(), "incident_SharePointDocumentLocations", oDataId);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError("Error adding reference to SharepointDocumentLocation");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                }
                
            }


            application = _dynamicsClient.GetApplicationByIdWithChildren(applicationId);
            
            return Json(application.ToViewModel());
        }

        [HttpGet("{id}/download-certificate")]
        public async Task<IActionResult> DownloadCertificate(string id)
        {
            _logger.LogDebug(LoggingEvents.HttpPost, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);

            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid applicationId))
            {

                string serverRelativeUrl = getCertificateServerRelativeUrl(applicationId);

                if (serverRelativeUrl == null)
                {
                    return new NotFoundResult();
                }
                string fileName = "";

                Regex regex = new Regex(@"/([^/]+)$");
                Match match = regex.Match(serverRelativeUrl);
                if (match.Success)
                {
                    fileName = match.Groups[1].Value;
                }
                 

                try
                {
                    byte[] fileContents = await _sharePointFileManager.DownloadFile(serverRelativeUrl);
                    return new FileContentResult(fileContents, "application/octet-stream")
                    {
                        FileDownloadName = fileName
                    };
                }
                catch (Exception e)
                {
                    _logger.LogError(LoggingEvents.HttpGet, "Error downloading certificate for application: ");
                    _logger.LogError(LoggingEvents.HttpGet, e.Message);
                    _logger.LogError(LoggingEvents.HttpGet, serverRelativeUrl);
                    return new NotFoundResult();
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("{id}/certificate-exists")]
        public async Task<IActionResult> CertificateExists(string id)
        {
            _logger.LogDebug(LoggingEvents.HttpPost, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);
            bool fileExists = false;

            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid applicationId))
            {
                string serverRelativeUrl = getCertificateServerRelativeUrl(applicationId);

                try
                {
                    byte[] fileContents = await _sharePointFileManager.DownloadFile(serverRelativeUrl);
                    if (fileContents.Length > 0)
                    {
                        fileExists = true;
                    }
                    return new JsonResult(fileExists);
                }
                catch (Exception e)
                {
                    _logger.LogError(LoggingEvents.HttpGet, "Error downloading certificate for application: ");
                    _logger.LogError(LoggingEvents.HttpGet, e.Message);
                    _logger.LogError(LoggingEvents.HttpGet, serverRelativeUrl);
                    return new JsonResult(false);
                }

            }
            else
            {
                _logger.LogError(LoggingEvents.HttpGet, "Unable to get account from application.");
                return BadRequest();
            }
        }

        private string getCertificateServerRelativeUrl(Guid applicationId)
        {
            string relativeUrl = null;

            // verify the currently logged in user has access to this account

            MicrosoftDynamicsCRMincident application = _dynamicsClient.GetApplicationByIdWithChildren(applicationId);
            if (application == null)
            {
                _logger.LogWarning(LoggingEvents.NotFound, "Application NOT found.");
                return null;
            }

            if (!UserDynamicsExtensions.CurrentUserHasAccessToApplication(applicationId, _httpContextAccessor, _dynamicsClient))
            {
                _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to the application.");
                return null;
            }


            string applicationTypeName = application.BcgovApplicationTypeId.BcgovName;
            string filePrefix = "";

            /*             
             * File name format is
             * WA_CERTIFICATE_<CERTIFICATE_NUMBER> (for Waiver)
             * RS_CERTIFICATE_<CERTIFICATE_NUMBER> (for Registered Seller)
             * EC_CERTIFICATE_<CERTIFICATE_NUMBER> (For Equipment Notification)
             * 
             */

            if (Guid.TryParse(application._customeridValue, out Guid accountId))
            {
                var account = _dynamicsClient.GetAccountByIdWithChildren(accountId);

                switch (applicationTypeName)
                {
                    case "Waiver":
                        filePrefix = "WA_CERTIFICATE_";
                        break;
                    case "Registered Seller":
                        filePrefix = "RS_CERTIFICATE_";
                        break;
                    case "Equipment Notification":
                        filePrefix = "EC_CERTIFICATE_";
                        break;
                }

                // get the latest certificate

                DateTimeOffset? dto = null;
                string certificateName = "";

                foreach (var certificate in application.BcgovIncidentBcgovCertificateApplication)
                {
                    if (dto == null || dto < certificate.BcgovIssueddate)
                    {
                        dto = certificate.BcgovIssueddate;
                        certificateName = certificate.BcgovName;
                    }
                }

                relativeUrl = account.GetServerUrl(_sharePointFileManager);


                relativeUrl += $"/{filePrefix}{certificateName}.pdf";
            }
            return relativeUrl;
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

            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid applicationId))
            {

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
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get a document location by reference
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        private string GetDocumentLocationReferenceByRelativeURL(string relativeUrl)
        {
            string result = null;
            string sanitized = relativeUrl.Replace("'", "''");
            // first see if one exists.
            var locations = _dynamicsClient.Sharepointdocumentlocations.Get(filter: "relativeurl eq '" + sanitized + "'");

            var location = locations.Value.FirstOrDefault();

            if (location == null)
            {
                var parentSite = _dynamicsClient.Sharepointsites.Get().Value.FirstOrDefault();
                var parentSiteRef = _dynamicsClient.GetEntityURI("sharepointsites", parentSite.Sharepointsiteid);
                MicrosoftDynamicsCRMsharepointdocumentlocation newRecord = new MicrosoftDynamicsCRMsharepointdocumentlocation()
                {
                    Relativeurl = relativeUrl,
                    Name = "Application",
                    ParentsiteorlocationSharepointdocumentlocationODataBind = parentSiteRef
                };
                // create a new document location.
                try
                {
                    location = _dynamicsClient.Sharepointdocumentlocations.Create(newRecord);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError("Error creating document location");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                }
            }

            if (location != null)
            {
                result = location.Sharepointdocumentlocationid;
            }

            return result;
        }


    }
}
