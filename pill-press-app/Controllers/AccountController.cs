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
        //private readonly SharePointFileManager _sharePointFileManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public AccountController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, BCeIDBusinessQuery bceid, ILoggerFactory loggerFactory, IDynamicsClient dynamicsClient)
        {
            Configuration = configuration;
            _bceid = bceid;
            _dynamicsClient = dynamicsClient;
            _httpContextAccessor = httpContextAccessor;
            //_sharePointFileManager = sharePointFileManager;
            _logger = loggerFactory.CreateLogger(typeof(AccountController));
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
                MicrosoftDynamicsCRMaccount account = await _dynamicsClient.GetAccountById(new Guid(accountId));
                _logger.LogDebug(LoggingEvents.HttpGet, "Dynamics Account: " + JsonConvert.SerializeObject(account));

                if (account == null)
                {
                    // Sometimes we receive the siteminderbusienssguid instead of the account id. 
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
            if (id != null)
            {
                // verify the currently logged in user has access to this account
                Guid accountId = new Guid(id);

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
                MicrosoftDynamicsCRMaccount account = _dynamicsClient.Accounts.GetByKey(accountId.ToString(), expand: expand);
                if (account == null)
                {
                    _logger.LogWarning(LoggingEvents.NotFound, "Account NOT found.");
                    return new NotFoundResult();
                }
                result = account.ToViewModel();
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

        [HttpPost()]
        public async Task<IActionResult> CreateDynamicsAccount([FromBody] ViewModels.Account item)
        {
            _logger.LogInformation(LoggingEvents.HttpPost, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);
            _logger.LogDebug(LoggingEvents.HttpPost, "Account parameters: " + JsonConvert.SerializeObject(item));

            ViewModels.Account result = null;
            Boolean updateIfNull = true;
            Guid tryParseOutGuid;

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

            // first check to see that a contact exists.
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

            // get the contact record.
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
                userContact.Fullname = userSettings.UserDisplayName;
                userContact.Nickname = userSettings.UserDisplayName;
                if (Guid.TryParse(userSettings.UserId, out tryParseOutGuid)) // BCeid id goes here
                {
                    userContact.Employeeid = userSettings.UserId;
                }
                else // Store the BC service card id here
                {
                    userContact.Externaluseridentifier = userSettings.UserId;
                }

                if (bceidBusiness != null)
                {
                    // set contact according to item
                    userContact.Firstname = bceidBusiness.individualFirstname;
                    userContact.Middlename = bceidBusiness.individualMiddlename;
                    userContact.Lastname = bceidBusiness.individualSurname;
                    userContact.Emailaddress1 = bceidBusiness.contactEmail;
                    userContact.Telephone1 = bceidBusiness.contactPhone;
                }
                else
                {
                    userContact.Firstname = userSettings.UserDisplayName.GetFirstName();
                    userContact.Lastname = userSettings.UserDisplayName.GetLastName();
                }
                userContact.Statuscode = 1;
                userContact.BcgovContacttype = 931490002; // BCeID contact type
            }
            // this may be an existing account, as this service is used during the account confirmation process.
            MicrosoftDynamicsCRMaccount account = await _dynamicsClient.GetAccountBySiteminderBusinessGuid(accountSiteminderGuid);
            _logger.LogDebug(LoggingEvents.HttpGet, "Account by siteminder business guid: " + JsonConvert.SerializeObject(account));

            if (account == null) // do a deep create.  create 3 objects at once.
            {
                _logger.LogDebug(LoggingEvents.HttpGet, "Account is null. Do a deep create of 3 objects at once.");
                // create a new account
                account = new MicrosoftDynamicsCRMaccount();
                account.CopyValues(item, updateIfNull);
                // business type must be set only during creation, not in update (removed from copyValues() )
                
                // by convention we strip out any dashes present in the guid, and force it to uppercase.
                string sanitizedAccountSiteminderId = GuidUtility.SanitizeGuidString(accountSiteminderGuid);

                account.BcgovBceid = sanitizedAccountSiteminderId;
                account.Primarycontactid = userContact;                

                if (bceidBusiness != null)
                {
                    account.Emailaddress1 = bceidBusiness.contactEmail;
                    account.Telephone1 = bceidBusiness.contactPhone;
                    account.Address1City = bceidBusiness.addressCity;
                    account.Address1Postalcode = bceidBusiness.addressPostal;
                    account.Address1Line1 = bceidBusiness.addressLine1;
                    account.Address1Line2 = bceidBusiness.addressLine2;
                    account.Address1Postalcode = bceidBusiness.addressPostal;
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
                


                userContact.Contactid = account._primarycontactidValue;

                accountString = JsonConvert.SerializeObject(accountString);
                _logger.LogDebug("Legal Entity after creation in dynamics --> " + accountString);


            }
            else // it is a new user only.
            {
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

            //account.accountId = id;
            result = account.ToViewModel();

            _logger.LogDebug(LoggingEvents.HttpPost, "result: " +
                JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return Json(result);
        }

        /// <summary>
        /// Update a legal entity
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDynamicsAccount([FromBody] ViewModels.Account item, string id)
        {
            _logger.LogInformation(LoggingEvents.HttpPut, "Begin method " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().ReflectedType.Name);
            _logger.LogDebug(LoggingEvents.HttpPut, "Account parameter: " + JsonConvert.SerializeObject(item));
            _logger.LogDebug(LoggingEvents.HttpPut, "id parameter: " + id);

            if (id != item.id)
            {
                _logger.LogWarning(LoggingEvents.BadRequest, "Bad Request. Id doesn't match the account id.");
                return BadRequest();
            }

            // get the legal entity.
            Guid accountId = new Guid(id);

            if (!UserDynamicsExtensions.CurrentUserHasAccessToAccount(accountId, _httpContextAccessor, _dynamicsClient))
            {
                _logger.LogWarning(LoggingEvents.NotFound, "Current user has NO access to the account.");
                return NotFound();
            }

            MicrosoftDynamicsCRMaccount adoxioAccount = await _dynamicsClient.GetAccountById(accountId);
            if (adoxioAccount == null)
            {
                _logger.LogWarning(LoggingEvents.NotFound, "Account NOT found.");
                return new NotFoundResult();
            }

            // we are doing a patch, so wipe out the record.
            adoxioAccount = new MicrosoftDynamicsCRMaccount();

            // copy values over from the data provided
            adoxioAccount.CopyValues(item);

            try
            {
                await _dynamicsClient.Accounts.UpdateAsync(accountId.ToString(), adoxioAccount);
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

            var updatedAccount = adoxioAccount.ToViewModel();
            _logger.LogDebug(LoggingEvents.HttpPut, "updatedAccount: " +
                JsonConvert.SerializeObject(updatedAccount, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

            return Json(updatedAccount);
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
            MicrosoftDynamicsCRMaccount account = await _dynamicsClient.GetAccountById(accountId);
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
    }
}
