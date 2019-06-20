using Gov.Jag.PillPressRegistry.Interfaces;
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Authentication;
using Gov.Jag.PillPressRegistry.Public.Models;
using Gov.Jag.PillPressRegistry.Public.Utils;
using Gov.Jag.PillPressRegistry.Public.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Business-User")]
    public class AccountController : Controller
    {
        private readonly BCeIDBusinessQuery _bceid;
        private readonly IConfiguration Configuration;
        private readonly IDynamicsClient _dynamicsClient;
        private readonly SharePointFileManager _sharePointFileManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public AccountController(SharePointFileManager sharePointFileManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, BCeIDBusinessQuery bceid, ILoggerFactory loggerFactory, IDynamicsClient dynamicsClient)
        {
            Configuration = configuration;
            _bceid = bceid;
            _dynamicsClient = dynamicsClient;
            _httpContextAccessor = httpContextAccessor;
            //_sharePointFileManager = sharePointFileManager;
            _logger = loggerFactory.CreateLogger(typeof(AccountController));
            _sharePointFileManager = sharePointFileManager;
        }

        /// GET account in Dynamics for the current user
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentAccount()
        {
            _logger.LogInformation(LoggingEvents.HttpGet, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);
            ViewModels.Account result = null;

            // get the current user.
            string sessionSettings = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(sessionSettings);
            _logger.LogDebug(LoggingEvents.HttpGet, "UserSettings: " + JsonConvert.SerializeObject(userSettings));

            // query the Dynamics system to get the account record.
            if (userSettings.AccountId != null && userSettings.AccountId.Length > 0)
            {
                var accountId = GuidUtility.SanitizeGuidString(userSettings.AccountId);
                MicrosoftDynamicsCRMaccount account = _dynamicsClient.GetAccountByIdWithChildren(new Guid(accountId));
                _logger.LogDebug(LoggingEvents.HttpGet, "Dynamics Account: " + JsonConvert.SerializeObject(account));

                if (account == null)
                {
                    // Sometimes we receive the siteminderbusinessguid instead of the account id. 
                    account = await _dynamicsClient.GetAccountBySiteminderBusinessGuid(accountId);
                    if (account == null)
                    {
                        _logger.LogWarning(LoggingEvents.NotFound, "No Account Found.");
                        return new NotFoundResult();
                    }
                }
                result = account.ToViewModel();
            }
            else
            {
                _logger.LogWarning(LoggingEvents.NotFound, "No Account Found.");
                return new NotFoundResult();
            }

            _logger.LogDebug(LoggingEvents.HttpGet, "Current Account Result: " +
               JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Json(result);
        }

        /// GET account in Dynamics for the current user
        [HttpGet("bceid")]
        public async Task<IActionResult> GetCurrentBCeIDBusiness()
        {
            _logger.LogInformation(LoggingEvents.HttpGet, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);

            // get the current user.
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);
            _logger.LogDebug(LoggingEvents.HttpGet, "UserSettings: " + JsonConvert.SerializeObject(userSettings));

            // query the BCeID API to get the business record.
            var business = await _bceid.ProcessBusinessQuery(userSettings.SiteMinderGuid);

             var cleanNumber = BusinessNumberSanitizer.SanitizeNumber(business?.businessNumber);
            if (cleanNumber != null)
            {
                business.businessNumber = cleanNumber;
            }

            if (business == null)
            {
                _logger.LogWarning(LoggingEvents.NotFound, "No Business Found.");
                return new NotFoundResult();
            }

            _logger.LogDebug(LoggingEvents.HttpGet, "BCeID business record: " +
                JsonConvert.SerializeObject(business, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Json(business);
        }

        /// <summary>
        /// Get a specific legal entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetAccount(string id)
        {
            _logger.LogInformation(LoggingEvents.HttpGet, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);
            _logger.LogDebug(LoggingEvents.HttpGet, "id: " + id);

            Boolean userAccessToAccount = false;
            ViewModels.Account result = null;

            // query the Dynamics system to get the account record.
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid accountId))
            {
                // verify the currently logged in user has access to this account
                try
                {
                    userAccessToAccount = UserDynamicsExtensions.CurrentUserHasAccessToAccount(accountId, _httpContextAccessor, _dynamicsClient);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError(LoggingEvents.Error, "Error while checking if current user has access to account.");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                }

                if (!userAccessToAccount)
                {
                    _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to account.");
                    return new NotFoundResult();
                }
                List<string> expand = new List<string> { "bcgov_CurrentBusinessPhysicalAddress",
                    "bcgov_CurrentBusinessMailingAddress", "bcgov_AdditionalContact", "primarycontactid" };
                try
                {
                    MicrosoftDynamicsCRMaccount account = _dynamicsClient.Accounts.GetByKey(accountId.ToString(), expand: expand);                    
                    result = account.ToViewModel();
                }
                catch (OdataerrorException)
                {
                    return new NotFoundResult();
                }


            }
            else
            {
                _logger.LogWarning(LoggingEvents.BadRequest, "Bad Request.");
                return BadRequest();
            }

            _logger.LogDebug(LoggingEvents.HttpGet, "Account result: " +
                JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Json(result);
        }

        /// <summary>
        /// Get the locations associated to an account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/locations")]
        public IActionResult GetAccountLocations(string id)
        {
            _logger.LogInformation(LoggingEvents.HttpGet, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);
            _logger.LogDebug(LoggingEvents.HttpGet, "id: " + id);

            Boolean userAccessToAccount = false;
            List<ViewModels.Location> result = new List<Location>();

            // query the Dynamics system to get the account record.
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid accountId))
            {
                // verify the currently logged in user has access to this account
                try
                {
                    userAccessToAccount = UserDynamicsExtensions.CurrentUserHasAccessToAccount(accountId, _httpContextAccessor, _dynamicsClient);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError(LoggingEvents.Error, "Error while checking if current user has access to account.");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                }

                if (!userAccessToAccount)
                {
                    _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to account.");
                    return new NotFoundResult();
                }
                List<string> expand = new List<string> { "bcgov_LocationAddress" };
                try
                {
                    string filter = $"_bcgov_businessprofile_value eq {id}";
                    var query = _dynamicsClient.Locations.Get(filter: filter, expand: expand);

                    foreach (var item in query.Value)
                    {
                        result.Add(item.ToViewModel());
                    }                    
                }
                catch (OdataerrorException)
                {
                    _logger.LogDebug("Error occured obtaining incident locations.");
                    return new NotFoundResult();
                }

            }
            else
            {
                _logger.LogWarning(LoggingEvents.BadRequest, "Bad Request.");
                return BadRequest();
            }

            _logger.LogDebug(LoggingEvents.HttpGet, "Account result: " +
                JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Json(result);
        }

        /// <summary>
        /// Get the business contacts associated to an account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ownermanagercode"></param>
        /// <returns></returns>
        [HttpGet("{id}/businesscontacts/{ownermanagercode}")]
        public IActionResult GetAccountBusinessContacts(string id, int ownermanagercode = 0)
        {
            _logger.LogInformation(LoggingEvents.HttpGet, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);
            _logger.LogDebug(LoggingEvents.HttpGet, "id: " + id);

            Boolean userAccessToAccount = false;
            List<ViewModels.BusinessContact> result = new List<BusinessContact>();

            // query the Dynamics system to get the account record.
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid accountId))
            {
                // verify the currently logged in user has access to this account
                try
                {
                    userAccessToAccount = UserDynamicsExtensions.CurrentUserHasAccessToAccount(accountId, _httpContextAccessor, _dynamicsClient);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError(LoggingEvents.Error, "Error while checking if current user has access to account.");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                }

                if (!userAccessToAccount)
                {
                    _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to account.");
                    return new NotFoundResult();
                }
                List<string> expand = new List<string> { "bcgov_Contact" };
                try
                {
                    string filter = $"_bcgov_businessprofile_value eq {id}";
                    if (ownermanagercode > 0) 
                    {
                        filter = filter + $" and bcgov_registeredsellerownermanager eq {ownermanagercode}";
                    }
                        var query = _dynamicsClient.Businesscontacts.Get(filter: filter, expand: expand); //, expand: expand

                    foreach (var item in query.Value)
                    {
                        result.Add(item.ToViewModel());
                    }
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogDebug("Error occured obtaining account business contacts.");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                    return new NotFoundResult();
                }

            }
            else
            {
                _logger.LogWarning(LoggingEvents.BadRequest, "Bad Request.");
                return BadRequest();
            }

            _logger.LogDebug(LoggingEvents.HttpGet, "Account Business Contacts result: " +
                JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Json(result);
        }

        /// <summary>
        /// Update contacts
        /// </summary>
        /// <param name="item"></param>
        private void UpdateContacts (ViewModels.Account item)
        {
            // Primary Contact
            if (item.primaryContact.HasValue())
            {
                var primaryContact = item.primaryContact.ToModel();
                if (string.IsNullOrEmpty(item.primaryContact.id))
                {
                    // create an account.                        
                    try
                    {
                        primaryContact = _dynamicsClient.Contacts.Create(primaryContact);
                        item.primaryContact.id = primaryContact.Contactid;
                    }
                    catch (OdataerrorException odee)
                    {
                        _logger.LogError(LoggingEvents.Error, "Error creating primary contact");
                        _logger.LogError("Request:");
                        _logger.LogError(odee.Request.Content);
                        _logger.LogError("Response:");
                        _logger.LogError(odee.Response.Content);
                        throw new OdataerrorException("Error updating the account.");
                    }
                }
                else
                {
                    // update
                    try
                    {
                        _dynamicsClient.Contacts.Update(item.primaryContact.id, primaryContact);
                    }
                    catch (OdataerrorException odee)
                    {
                        _logger.LogError(LoggingEvents.Error, "Error updating primary contact");
                        _logger.LogError("Request:");
                        _logger.LogError(odee.Request.Content);
                        _logger.LogError("Response:");
                        _logger.LogError(odee.Response.Content);
                        throw new OdataerrorException("Error updating the account.");
                    }
                }
            }

            // Additional Contact 
            if (item.additionalContact.HasValue())
            {
                var additionalContact = item.additionalContact.ToModel();
                if (string.IsNullOrEmpty(item.additionalContact.id))
                {
                    // create an account.                        
                    try
                    {
                        additionalContact = _dynamicsClient.Contacts.Create(additionalContact);
                        item.additionalContact.id = additionalContact.Contactid;
                    }
                    catch (OdataerrorException odee)
                    {
                        _logger.LogError(LoggingEvents.Error, "Error creating additional contact");
                        _logger.LogError("Request:");
                        _logger.LogError(odee.Request.Content);
                        _logger.LogError("Response:");
                        _logger.LogError(odee.Response.Content);
                        throw new OdataerrorException("Error updating the account.");
                    }
                }
                else
                {
                    // update
                    try
                    {
                        _dynamicsClient.Contacts.Update(item.additionalContact.id, additionalContact);
                    }
                    catch (OdataerrorException odee)
                    {
                        _logger.LogError(LoggingEvents.Error, "Error updating additional contact");
                        _logger.LogError("Request:");
                        _logger.LogError(odee.Request.Content);
                        _logger.LogError("Response:");
                        _logger.LogError(odee.Response.Content);
                        throw new OdataerrorException("Error updating the account.");
                    }
                }
            }
        }

        /// <summary>
        /// Update an account
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount([FromBody] ViewModels.Account item, string id)
        {            
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid accountId))
            {
                _logger.LogInformation(LoggingEvents.HttpPut, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);
                _logger.LogDebug(LoggingEvents.HttpPut, "Account parameter: " + JsonConvert.SerializeObject(item));
                _logger.LogDebug(LoggingEvents.HttpPut, "id parameter: " + id);

         
                if (!UserDynamicsExtensions.CurrentUserHasAccessToAccount(accountId, _httpContextAccessor, _dynamicsClient))
                {
                    _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to the account.");
                    return NotFound();
                }

                MicrosoftDynamicsCRMaccount account = _dynamicsClient.GetAccountByIdWithChildren(accountId);
                if (account == null)
                {
                    _logger.LogWarning(LoggingEvents.NotFound, "Account NOT found.");
                    return new NotFoundResult();
                }

                // handle the contacts.

                UpdateContacts(item);

                // we are doing a patch, so wipe out the record.
                MicrosoftDynamicsCRMaccount patchAccount = new MicrosoftDynamicsCRMaccount();

                // copy values over from the data provided
                patchAccount.CopyValues(item);
                if (item.primaryContact != null && item.primaryContact.id != null && 
                    (account._primarycontactidValue == null || account._primarycontactidValue != item.primaryContact.id))
                {
                    patchAccount.PrimaryContactidODataBind = _dynamicsClient.GetEntityURI("contacts", item.primaryContact.id);
                }
                else
                {
                    if (account._primarycontactidValue != null && ! item.primaryContact.HasValue())
                    {
                        // remove the reference.
                        try
                        {
                            // pass null as recordId to remove the single value navigation property
                            _dynamicsClient.Accounts.RemoveReference(accountId.ToString(), "primarycontactid", null);
                        }
                        catch (OdataerrorException odee)
                        {
                            _logger.LogError(LoggingEvents.Error, "Error updating the account.");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                            throw new OdataerrorException("Error updating the account.");
                        }

                        // delete the contact.
                        try
                        {                            
                            _dynamicsClient.Contacts.Delete(account._primarycontactidValue);
                        }
                        catch (OdataerrorException odee)
                        {
                            _logger.LogError(LoggingEvents.Error, "Error removing primary contact");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                            throw new OdataerrorException("Error updating the account.");
                        }

                    }
                }

                
                if (item.additionalContact != null && item.additionalContact.id != null &&
                    (account._bcgovAdditionalcontactValue == null || account._bcgovAdditionalcontactValue != item.additionalContact.id))
                {
                    patchAccount.AdditionalContactODataBind = _dynamicsClient.GetEntityURI("contacts", item.additionalContact.id);
                }
                else
                {
                    if (account._bcgovAdditionalcontactValue != null && ! item.additionalContact.HasValue())
                    {
                        // remove the reference.
                        try
                        {
                            // pass null as recordId to remove the single value navigation property
                            _dynamicsClient.Accounts.RemoveReference(accountId.ToString(), "bcgov_AdditionalContact", null);
                        }
                        catch (OdataerrorException odee)
                        {
                            _logger.LogError(LoggingEvents.Error, "Error updating the account.");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                            throw new OdataerrorException("Error updating the account.");
                        }

                        // delete the contact.
                        try
                        {
                            _dynamicsClient.Contacts.Delete(account._bcgovAdditionalcontactValue);
                        }
                        catch (OdataerrorException odee)
                        {
                            _logger.LogError(LoggingEvents.Error, "Error removing additional contact");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                            throw new OdataerrorException("Error updating the account.");
                        }
                    }
                }


                try
                {
                    await _dynamicsClient.Accounts.UpdateAsync(accountId.ToString(), patchAccount);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError(LoggingEvents.Error, "Error updating the account.");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                    throw new OdataerrorException("Error updating the account.");
                }

                // commenting out the purge of any existing non bceid accounts. They should remain  as part of business contacts history
                //_dynamicsClient.DeleteNonBceidBusinessContactLinkForAccount(_logger, accountId.ToString());

                // create the business contact links.
                if (item.primaryContact != null)
                {
                    _dynamicsClient.CreateBusinessContactLink(_logger, item.primaryContact.id, accountId.ToString(), null, (int?)ContactTypeCodes.Primary, item.primaryContact.title);
                }
                if (item.additionalContact != null)
                {
                    _dynamicsClient.CreateBusinessContactLink(_logger, item.additionalContact.id, accountId.ToString(), null, (int?)ContactTypeCodes.Additional, item.additionalContact.title);
                }
                

                // populate child items in the account.
                patchAccount = _dynamicsClient.GetAccountByIdWithChildren(accountId);

                var updatedAccount = patchAccount.ToViewModel();
                _logger.LogDebug(LoggingEvents.HttpPut, "updatedAccount: " +
                    JsonConvert.SerializeObject(updatedAccount, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

                return Json(updatedAccount);
            }
            else
            {
                return new BadRequestResult();
            }

        }


        [HttpPost()]
        public async Task<IActionResult> CreateAccount([FromBody] ViewModels.Account item)
        {
            _logger.LogInformation(LoggingEvents.HttpPost, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);
            _logger.LogDebug(LoggingEvents.HttpPost, "Account parameters: " + JsonConvert.SerializeObject(item));

            ViewModels.Account result = null;

            // get UserSettings from the session
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);
            _logger.LogDebug(LoggingEvents.HttpPost, "UserSettings: " + JsonConvert.SerializeObject(userSettings));

            // get account Siteminder GUID
            string accountSiteminderGuid = userSettings.SiteMinderBusinessGuid;
            if (accountSiteminderGuid == null || accountSiteminderGuid.Length == 0)
            {
                _logger.LogError(LoggingEvents.Error, "No account Siteminder Guid exernal id");
                throw new Exception("Error. No accountSiteminderGuid exernal id");
            }

            // validate contact Siteminder GUID
            string contactSiteminderGuid = userSettings.SiteMinderGuid;
            if (contactSiteminderGuid == null || contactSiteminderGuid.Length == 0)
            {
                _logger.LogError(LoggingEvents.Error, "No Contact Siteminder Guid exernal id");
                throw new Exception("Error. No ContactSiteminderGuid exernal id");
            }

            // get BCeID record for the current user
            Gov.Jag.PillPressRegistry.Interfaces.BCeIDBusiness bceidBusiness = await _bceid.ProcessBusinessQuery(userSettings.SiteMinderGuid);
            var cleanNumber = BusinessNumberSanitizer.SanitizeNumber(bceidBusiness?.businessNumber);
            if (cleanNumber != null)
            {
                bceidBusiness.businessNumber = cleanNumber;
            }

            _logger.LogDebug(LoggingEvents.HttpGet, "BCeId business: " + JsonConvert.SerializeObject(bceidBusiness));

            MicrosoftDynamicsCRMcontact userContact = null;

            // see if the contact exists.
            try
            {
                userContact = _dynamicsClient.GetContactByExternalId(contactSiteminderGuid);
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError(LoggingEvents.Error, "Error getting contact by Siteminder Guid.");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
                throw new OdataerrorException("Error getting contact by Siteminder Guid");
            }

            if (userContact == null)
            {
                // create the user contact record.
                userContact = new MicrosoftDynamicsCRMcontact();
                // Adoxio_externalid is where we will store the guid from siteminder.
                string sanitizedContactSiteminderId = GuidUtility.SanitizeGuidString(contactSiteminderGuid);
                userContact.Externaluseridentifier = sanitizedContactSiteminderId;
                userContact.BcgovBceiduserguid = sanitizedContactSiteminderId;

                userContact.Fullname = userSettings.UserDisplayName;
                userContact.Nickname = userSettings.UserDisplayName;

                // ENABLE FOR BC SERVICE CARD SUPPORT
                /*
                if (! Guid.TryParse(userSettings.UserId, out tryParseOutGuid)) 
                {                    
                    userContact.Externaluseridentifier = userSettings.UserId;
                }
                */

                if (bceidBusiness != null)
                {
                    // set contact according to item
                    userContact.Firstname = bceidBusiness.individualFirstname;
                    userContact.Middlename = bceidBusiness.individualMiddlename;
                    userContact.Lastname = bceidBusiness.individualSurname;
                    userContact.Emailaddress1 = bceidBusiness.contactEmail;
                    userContact.Telephone1 = bceidBusiness.contactPhone;
                    userContact.BcgovBceid = bceidBusiness.userId;
                    userContact.BcgovBceidemail = bceidBusiness.contactEmail;
                }
                else
                {
                    userContact.Firstname = userSettings.UserDisplayName.GetFirstName();
                    userContact.Lastname = userSettings.UserDisplayName.GetLastName();
                }

                userContact.Statuscode = 1;

                _logger.LogDebug(LoggingEvents.HttpGet, "Account is NOT null. Only a new user.");
                try
                {
                    userContact = await _dynamicsClient.Contacts.CreateAsync(userContact);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError(LoggingEvents.Error, "Error creating user contact.");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                    throw new OdataerrorException("Error creating user contact.");
                }

            }
            // this may be an existing account, as this service is used during the account confirmation process.
            MicrosoftDynamicsCRMaccount account = await _dynamicsClient.GetAccountBySiteminderBusinessGuid(accountSiteminderGuid);
            _logger.LogDebug(LoggingEvents.HttpGet, "Account by siteminder business guid: " + JsonConvert.SerializeObject(account));

            if (account == null)
            {
                _logger.LogDebug(LoggingEvents.HttpGet, "Creating account");
                // create a new account
                account = new MicrosoftDynamicsCRMaccount();
                account.CopyValues(item);
                // business type must be set only during creation, not in update (removed from copyValues() )

                // by convention we strip out any dashes present in the guid, and force it to uppercase.
                string sanitizedAccountSiteminderId = GuidUtility.SanitizeGuidString(accountSiteminderGuid);

                account.BcgovBceid = sanitizedAccountSiteminderId;

                UpdateContacts(item);

                // For Pill Press the Primary Contact is not set to default to the first user.  
                if (item.primaryContact != null && !(string.IsNullOrEmpty(item.primaryContact.id)))
                {
                    // add as a reference.
                    account.PrimaryContactidODataBind = _dynamicsClient.GetEntityURI("contacts", item.primaryContact.id);
                }

                // Additional Contact 
                if (item.additionalContact != null && !(string.IsNullOrEmpty(item.additionalContact.id)))
                {
                    // add as a reference.
                    account.AdditionalContactODataBind = _dynamicsClient.GetEntityURI("contacts", item.additionalContact.id);
                }

                if (bceidBusiness != null)
                {
                    account.Name = bceidBusiness.legalName;
                    account.BcgovDoingbusinessasname = bceidBusiness.legalName;
                    account.Emailaddress1 = bceidBusiness.contactEmail;
                    account.Telephone1 = bceidBusiness.contactPhone;

                    // do not set the address from BCeID for Pill Press.
                    /*
                    account.Address1City = bceidBusiness.addressCity;
                    account.Address1Postalcode = bceidBusiness.addressPostal;
                    account.Address1Line1 = bceidBusiness.addressLine1;
                    account.Address1Line2 = bceidBusiness.addressLine2;
                    account.Address1Postalcode = bceidBusiness.addressPostal;
                    */
                }
                else // likely a dev login.
                {
                    account.Name = userSettings.BusinessLegalName;
                    account.BcgovDoingbusinessasname = userSettings.BusinessLegalName;
                }

                // set the Province and Country if they are not set.
                if (string.IsNullOrEmpty(account.Address1Stateorprovince))
                {
                    account.Address1Stateorprovince = "British Columbia";
                }
                if (string.IsNullOrEmpty(account.Address1Country))
                {
                    account.Address1Country = "Canada";
                }

                string accountString = JsonConvert.SerializeObject(account);
                _logger.LogDebug("Account before creation in dynamics --> " + accountString);

                try
                {
                    account = await _dynamicsClient.Accounts.CreateAsync(account);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError(LoggingEvents.Error, "Error creating Account.");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                    throw new OdataerrorException("Error creating Account");
                }

                // create a document location

                await CreateAccountDocumentLocation(account);

                // populate child elements.
                account = _dynamicsClient.GetAccountByIdWithChildren(Guid.Parse(account.Accountid));

                accountString = JsonConvert.SerializeObject(accountString);
                _logger.LogDebug("Account Entity after creation in dynamics --> " + accountString);

            }


            // always patch the userContact so it relates to the account.
            _logger.LogDebug(LoggingEvents.Save, "Patching the userContact so it relates to the account.");
            // parent customer id relationship will be created using the method here:
            //https://msdn.microsoft.com/en-us/library/mt607875.aspx
            MicrosoftDynamicsCRMcontact patchUserContact = new MicrosoftDynamicsCRMcontact();
            patchUserContact.ParentCustomerIdAccountODataBind = _dynamicsClient.GetEntityURI("accounts", account.Accountid);
            try
            {
                await _dynamicsClient.Contacts.UpdateAsync(userContact.Contactid, patchUserContact);
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError(LoggingEvents.Error, "Error binding contact to account");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
                throw new OdataerrorException("Error binding contact to account");
            }

            // if we have not yet authenticated, then this is the new record for the user.
            if (userSettings.IsNewUserRegistration)
            {
                userSettings.AccountId = account.Accountid.ToString();
                userSettings.ContactId = userContact.Contactid.ToString();

                // we can now authenticate.
                if (userSettings.AuthenticatedUser == null)
                {
                    Models.User user = new Models.User();
                    user.Active = true;
                    user.AccountId = Guid.Parse(userSettings.AccountId);
                    user.ContactId = Guid.Parse(userSettings.ContactId);
                    user.UserType = userSettings.UserType;
                    user.SmUserId = userSettings.UserId;
                    userSettings.AuthenticatedUser = user;
                }

                // create the bridge entity for the BCeID user
                _dynamicsClient.CreateBusinessContactLink(_logger, userSettings.ContactId, userSettings.AccountId, null, (int?)ContactTypeCodes.BCeID, "BCeID");

                userSettings.IsNewUserRegistration = false;

                string userSettingsString = JsonConvert.SerializeObject(userSettings);
                _logger.LogDebug("userSettingsString --> " + userSettingsString);

                // add the user to the session.
                _httpContextAccessor.HttpContext.Session.SetString("UserSettings", userSettingsString);
                _logger.LogDebug("user added to session. ");
            }
            else
            {
                _logger.LogError(LoggingEvents.Error, "Invalid user registration.");
                throw new Exception("Invalid user registration.");
            }

            // create the business contact links.
            if (item.primaryContact != null)
            {
                _dynamicsClient.CreateBusinessContactLink(_logger, item.primaryContact.id, account.Accountid, null, (int?)ContactTypeCodes.Primary, item.primaryContact.title);
            }
            if (item.additionalContact != null)
            {
                _dynamicsClient.CreateBusinessContactLink(_logger, item.additionalContact.id, account.Accountid, null, (int?)ContactTypeCodes.Additional, item.additionalContact.title);
            }

            //account.Accountid = id;
            result = account.ToViewModel();

            _logger.LogDebug(LoggingEvents.HttpPost, "result: " +
                JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Json(result);
        }

        private async Task CreateAccountDocumentLocation(MicrosoftDynamicsCRMaccount account)
        {
            string serverRelativeUrl = account.GetServerUrl(_sharePointFileManager);
            string name = "";

            if (string.IsNullOrEmpty(account.BcgovDoingbusinessasname))
            {
                name = account.Accountid;
            }
            else
            {
                name = account.BcgovDoingbusinessasname;
            }

            name += " Account Files";


            // create a SharePointDocumentLocation link
            string folderName = "_" + account.Accountid;


            // Create the folder
            bool folderExists = await _sharePointFileManager.FolderExists(SharePointFileManager.AccountDocumentListTitle, folderName);
            if (!folderExists)
            {
                try
                {
                    var folder = await _sharePointFileManager.CreateFolder(SharePointFileManager.AccountDocumentListTitle, folderName);
                }
                catch (Exception e)
                {
                    _logger.LogError("Error creating Sharepoint Folder");
                    _logger.LogError($"List is: {SharePointFileManager.AccountDocumentListTitle}");
                    _logger.LogError($"FolderName is: {folderName}");
                    throw e;
                }

            }

            // now create a document location to link them.

            // Create the SharePointDocumentLocation entity
            MicrosoftDynamicsCRMsharepointdocumentlocation mdcsdl = new MicrosoftDynamicsCRMsharepointdocumentlocation()
            {
                Relativeurl = folderName,
                Description = "Account Files",
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

                // set the parent document library.
                string parentDocumentLibraryReference = GetDocumentLocationReferenceByRelativeURL("account");

                string accountUri = _dynamicsClient.GetEntityURI("accounts", account.Accountid);
                // add a regardingobjectid.
                var patchSharePointDocumentLocationIncident = new MicrosoftDynamicsCRMsharepointdocumentlocation()
                {
                    RegardingobjectIdAccountODataBind = accountUri,
                    ParentsiteorlocationSharepointdocumentlocationODataBind = _dynamicsClient.GetEntityURI("sharepointdocumentlocations", parentDocumentLibraryReference),
                    Relativeurl = folderName,
                    Description = "Account Files",
                };

                try
                {
                    _dynamicsClient.Sharepointdocumentlocations.Update(mdcsdl.Sharepointdocumentlocationid, patchSharePointDocumentLocationIncident);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError("Error adding reference SharepointDocumentLocation to account");
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
                    _dynamicsClient.Accounts.AddReference(account.Accountid, "Account_SharepointDocumentLocation", oDataId);
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
        }


        /// <summary>
        /// Delete a legal entity.  Using a HTTP Post to avoid Siteminder issues with DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteDynamicsAccount(string id)
        {
            _logger.LogInformation(LoggingEvents.HttpPost, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);

            // verify the currently logged in user has access to this account
            Guid accountId = new Guid(id);
            if (!UserDynamicsExtensions.CurrentUserHasAccessToAccount(accountId, _httpContextAccessor, _dynamicsClient))
            {
                _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to the account.");
                return new NotFoundResult();
            }

            // get the account
            MicrosoftDynamicsCRMaccount account = _dynamicsClient.GetAccountByIdWithChildren(accountId);
            if (account == null)
            {
                _logger.LogWarning(LoggingEvents.NotFound, "Account NOT found.");
                return new NotFoundResult();
            }
            
            try
            {
                await _dynamicsClient.Accounts.DeleteAsync(accountId.ToString());
                _logger.LogDebug(LoggingEvents.HttpDelete, "Account deleted: " + accountId.ToString());
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError(LoggingEvents.Error, "Error deleting the account: " + accountId.ToString());
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
                throw new OdataerrorException("Error deleting the account: " + accountId.ToString());
            }

            _logger.LogDebug(LoggingEvents.HttpDelete, "No content returned.");
            return NoContent(); // 204 
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
                    Name = "Account",
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

