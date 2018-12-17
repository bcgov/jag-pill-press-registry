
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Authentication;
using Gov.Jag.PillPressRegistry.Public.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class UserDynamicsExtensions
    {

        /// <summary>
        /// Load User from database using their userId and guid
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static async Task<User> LoadUser(this IDynamicsClient _dynamicsClient, string userId, IHeaderDictionary Headers, ILogger _logger, string guid = null)
        {
            User user = null;
            MicrosoftDynamicsCRMcontact contact = null;
            Guid userGuid;

            if (!string.IsNullOrEmpty(guid))
            {
                user = await _dynamicsClient.GetUserByGuid(guid);
            }

            if (user == null)
            {
                _logger.LogInformation(">>>> LoadUser for BCEID.");
                if (Guid.TryParse(userId, out userGuid))
                {
                    user = _dynamicsClient.GetUserBySmUserId(userId);
                    if (user != null)
                    {
                        _logger.LogInformation(">>>> LoadUser for BCEID: user != null");
                        // Update the contact with info from Siteminder
                        var contactVM = new Public.ViewModels.Contact();
                        contactVM.CopyHeaderValues(Headers);
                        _logger.LogInformation(">>>> After reading headers: " + Newtonsoft.Json.JsonConvert.SerializeObject(contactVM));
                        MicrosoftDynamicsCRMcontact patchContact = new MicrosoftDynamicsCRMcontact();
                        patchContact.CopyValues(contactVM);
                        try
                        {
                            _dynamicsClient.Contacts.Update(user.ContactId.ToString(), patchContact);
                        }
                        catch (OdataerrorException odee)
                        {
                            _logger.LogError("Error updating Contact");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                            // fail if we can't create.
                            throw (odee);
                        }
                    }
                }
                else
                { //BC service card login

                    _logger.LogInformation(">>>> LoadUser for BC Services Card.");
                    string externalId = GetServiceCardID(userId);
                    contact = _dynamicsClient.GetContactByExternalId(externalId);

                    if (contact != null)
                    {
                        _logger.LogInformation(">>>> LoadUser for BC Services Card: contact != null");

                        /*
                        user = new User();
                        user.FromContact(contact);

                        // Update the contact and worker with info from Siteminder
                        var contactVM = new Public.ViewModels.Contact();
                        var workerVm = new Public.ViewModels.Worker();
                        contactVM.CopyHeaderValues(Headers);
                        workerVm.CopyHeaderValues(Headers);
                        MicrosoftDynamicsCRMcontact patchContact = new MicrosoftDynamicsCRMcontact();
                        MicrosoftDynamicsCRMadoxioWorker patchWorker = new MicrosoftDynamicsCRMadoxioWorker();
                        patchContact.CopyValues(contactVM);
                        patchWorker.CopyValues(workerVm);
                        try
                        {
                            string filter = $"_adoxio_contactid_value eq {contact.Contactid}";
                            var workers = _dynamicsClient.Workers.Get(filter: filter).Value;
                            foreach (var item in workers)
                            {
                                _dynamicsClient.Workers.Update(item.AdoxioWorkerid, patchWorker);

                            }
                            _dynamicsClient.Contacts.Update(user.ContactId.ToString(), patchContact);
                        }
                        catch (OdataerrorException odee)
                        {
                            _logger.LogError("Error updating Contact");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                            // fail if we can't create.
                            throw (odee);
                        }
                        */
                    }
                }
            }

            if (user == null)
                return null;

            if (guid == null)
                return user;


            if (!user.ContactId.ToString().Equals(guid, StringComparison.OrdinalIgnoreCase))
            {
                // invalid account - guid doesn't match user credential
                return null;
            }

            return user;
        }

        /// <summary>
        /// Convert a service card ID string into a format that is useful (and fits into Dynamics)
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static string GetServiceCardID(string raw)
        {
            string result = "";
            if (!string.IsNullOrEmpty(raw))
            {
                var tokens = raw.Split('|');
                if (tokens.Length > 0)
                {
                    result = tokens[0];
                }

                if (!string.IsNullOrEmpty(result))
                {
                    tokens = result.Split(':');
                    result = tokens[tokens.Length - 1];
                }
            }

            return result;
        }


        /// <summary>
        /// Returns a User based on the guid
        /// </summary>
        /// <param name="context"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static async Task<User> GetUserByGuid(this IDynamicsClient _dynamicsClient, string guid)
        {
            Guid id = new Guid(guid);
            User user = null;
            var contact = _dynamicsClient.GetContactById(id);
            if (contact != null)
            {
                user = new User();
                user.FromContact(contact);
            }

            return user;
        }



        /// <summary>
        /// Returns a User based on the guid
        /// </summary>
        /// <param name="context"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static User GetUserBySmUserId(this IDynamicsClient _dynamicsClient, string guid)
        {
            Guid id = new Guid(guid);
            User user = null;
            var contact = _dynamicsClient.GetContactByExternalId(id.ToString());
            if (contact != null)
            {
                user = new User();
                user.FromContact(contact);
            }

            return user;
        }

        /// <summary>
        /// Verify whether currently logged in user has access to this account id
        /// </summary>
        /// <returns>boolean</returns>
        public static bool CurrentUserHasAccessToAccount(Guid accountId, IHttpContextAccessor _httpContextAccessor, IDynamicsClient _dynamicsClient)
        {
            // get the current user.
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

            if (userSettings.AccountId != null && userSettings.AccountId.Length > 0)
            {
                return userSettings.AccountId == accountId.ToString();
            }

            // if current user doesn't have an account they are probably not logged in
            return false;
        }

        /// <summary>
        /// Verify whether currently logged in user has access to this account id
        /// </summary>
        /// <returns>boolean</returns>
        public static bool CurrentUserHasAccessToApplication(Guid applicationId, IHttpContextAccessor _httpContextAccessor, IDynamicsClient _dynamicsClient)
        {
            var application = _dynamicsClient.GetApplicationById(applicationId);
            
            var accountId = application._customeridValue;

            // get the current user.
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(temp);

            if (userSettings.AccountId != null && userSettings.AccountId.Length > 0)
            {
                return userSettings.AccountId == accountId.ToString();
            }

            // if current user doesn't have an account they are probably not logged in
            return false;
        }
    }
}
