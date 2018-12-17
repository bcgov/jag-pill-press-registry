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
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Public.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IDynamicsClient _dynamicsClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;

        public ContactController(IConfiguration configuration, IDynamicsClient dynamicsClient, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            Configuration = configuration;
            _dynamicsClient = dynamicsClient;
            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger(typeof(ContactController));
            this._env = env;
        }


        /// <summary>
        /// Get a specific legal entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetContact(string id)
        {
            ViewModels.Contact result = null;

            if (!string.IsNullOrEmpty(id))
            {
                Guid contactId = Guid.Parse(id);
                // query the Dynamics system to get the contact record.
                MicrosoftDynamicsCRMcontact contact =  _dynamicsClient.GetContactById(contactId);

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
        /// Update a legal entity
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact([FromBody] ViewModels.Contact item, string id)
        {
            if (id != null && item.id != null && id != item.id)
            {
                return BadRequest();
            }

            // get the contact
            Guid contactId = Guid.Parse(id);

            MicrosoftDynamicsCRMcontact contact = _dynamicsClient.GetContactById(contactId);
            if (contact == null)
            {
                return new NotFoundResult();
            }
            MicrosoftDynamicsCRMcontact patchContact = new MicrosoftDynamicsCRMcontact();
            patchContact.CopyValues(item);
            try
            {
                await _dynamicsClient.Contacts.UpdateAsync(contactId.ToString(), patchContact);
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError("Error updating contact");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
            }

            contact = _dynamicsClient.GetContactById(contactId);
            return Json(contact.ToViewModel());
        }

        /// <summary>
        /// Create a contact
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateContact([FromBody] ViewModels.Contact item)
        {

            // get UserSettings from the session
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

            // first check to see that a contact exists.
            string contactSiteminderGuid = userSettings.SiteMinderGuid;
            if (contactSiteminderGuid == null || contactSiteminderGuid.Length == 0)
            {
                _logger.LogError(LoggingEvents.Error, "No Contact Siteminder Guid exernal id");
                throw new Exception("Error. No ContactSiteminderGuid exernal id");
            }

            // get the contact record.
            MicrosoftDynamicsCRMcontact userContact = null;

            // see if the contact exists.
            try
            {
                userContact = _dynamicsClient.GetContactByExternalId(contactSiteminderGuid);
                if (userContact != null)
                {
                    throw new Exception("Contact already Exists");
                }
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

            // create a new contact.
            MicrosoftDynamicsCRMcontact contact = new MicrosoftDynamicsCRMcontact();
            contact.CopyValues(item);
            

            if (userSettings.IsNewUserRegistration)
            {
                // get additional information from the service card headers.
                contact.CopyHeaderValues( _httpContextAccessor );
            }        

            contact.Externaluseridentifier = UserDynamicsExtensions.GetServiceCardID(contactSiteminderGuid);
            try
            {
                contact = await _dynamicsClient.Contacts.CreateAsync(contact);
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError("Error updating contact");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
            }

            // if we have not yet authenticated, then this is the new record for the user.
            if (userSettings.IsNewUserRegistration)
            {
                userSettings.ContactId = contact.Contactid.ToString();

                // we can now authenticate.
                if (userSettings.AuthenticatedUser == null)
                {
                    Models.User user = new Models.User();
                    user.Active = true;
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

            return Json(contact.ToViewModel());
        }

        /// <summary>
        /// Create a contact
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("worker")]
        public IActionResult CreateWorkerContact([FromBody] ViewModels.Contact item)
        {
            // get UserSettings from the session
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

            // first check to see that a contact exists.
            string contactSiteminderGuid = userSettings.SiteMinderGuid;
            if (contactSiteminderGuid == null || contactSiteminderGuid.Length == 0)
            {
                _logger.LogError(LoggingEvents.Error, "No Contact Siteminder Guid exernal id");
                throw new Exception("Error. No ContactSiteminderGuid exernal id");
            }

            // get the contact record.
            MicrosoftDynamicsCRMcontact userContact = null;

            // see if the contact exists.
            try
            {
                userContact = _dynamicsClient.GetContactByExternalId(contactSiteminderGuid);
                if (userContact != null)
                {
                    throw new Exception("Contact already Exists");
                }
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

            // create a new contact.
            MicrosoftDynamicsCRMcontact contact = new MicrosoftDynamicsCRMcontact();
            
            contact.CopyValues(item);

            
            contact.Externaluseridentifier = UserDynamicsExtensions.GetServiceCardID(contactSiteminderGuid);

            // if we have not yet authenticated, then this is the new record for the user.
            if (userSettings.IsNewUserRegistration)
            {
                userSettings.ContactId = contact.Contactid.ToString();

                // we can now authenticate.
                if (userSettings.AuthenticatedUser == null)
                {
                    Models.User user = new Models.User();
                    user.Active = true;
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

            return Json(contact.ToViewModel());
        }
    }
}
