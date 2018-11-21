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

            MicrosoftDynamicsCRMincident Application = _dynamicsClient.GetApplicationById(ApplicationId);
            if (Application == null)
            {
                return new NotFoundResult();
            }
            MicrosoftDynamicsCRMincident patchApplication = new MicrosoftDynamicsCRMincident();
            patchApplication.CopyValues(item);
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

            Application = _dynamicsClient.GetApplicationById(ApplicationId);
            return Json(Application.ToViewModel());
        }

        /// <summary>
        /// Create a Application
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateApplication([FromBody] ViewModels.Application item)
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
            
            // create a new Application.
            MicrosoftDynamicsCRMincident application = new MicrosoftDynamicsCRMincident();
            application.CopyValues(item);

            // set the author based on the current user.
            application.SubmitterODataBind = _dynamicsClient.GetEntityURI("contacts", userSettings.ContactId);

            // Also setup the customer.
            var account = await _dynamicsClient.GetAccountById(Guid.Parse(userSettings.AccountId));
            //application.CustomeridAccount = account;
            application.CustomerIdAccountODataBind = _dynamicsClient.GetEntityURI("accounts", userSettings.AccountId);

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
            Guid applicationId = new Guid(id);

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
