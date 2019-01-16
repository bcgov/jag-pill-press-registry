
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Gov.Jag.PillPressRegistry.Public.Utils;
using Gov.Jag.PillPressRegistry.Public.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    public static class BusinessContactExtensions
    {

        /// <summary>
        /// Get a Account by their Guid
        /// </summary>
        /// <param name="system"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void CreateBusinessContactLink(this IDynamicsClient system, ILogger _logger, string contactId, string accountId, string jobtitle, int? contactType, string jobTitle)
        {
            bool valid = true;
            string errorMessage = "";
            if (string.IsNullOrEmpty(contactId))
            {
                errorMessage += "ContactId is null. ";
                valid = false;
            }
            if (string.IsNullOrEmpty(accountId))
            {
                errorMessage += "AccountId is null. ";
                valid = false;
            }
            
            if (contactType == null)
            {
                errorMessage += "ContactType is null. ";
                valid = false;
            }

            if (valid)
            {
                MicrosoftDynamicsCRMbcgovBusinesscontact result = system.GetBusinessContactLink(_logger, contactId, accountId);

                if (result == null)
                {
                    // create it.
                    try
                    {
                        result = new MicrosoftDynamicsCRMbcgovBusinesscontact()
                        {                            
                            BcgovContacttype = contactType,
                            ContactODataBind = system.GetEntityURI("contacts", contactId),
                            AccountODataBind = system.GetEntityURI("accounts", accountId),
                            BcgovJobtitle = jobTitle
                        };                        

                        if (!string.IsNullOrEmpty(jobtitle))
                        {
                            result.BcgovJobtitle = jobtitle;
                        }
                        result = system.Businesscontacts.Create(result);
                    }
                    catch (OdataerrorException odee)
                    {
                        if (_logger != null)
                        {
                            _logger.LogError(LoggingEvents.Error, "Error while creating a business contact.");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                        }                        
                    }

                    // now link the BusinessContact to the Account.
                                      
                    try
                    {                        
                        OdataId oDataId = new OdataId()
                        {
                            OdataIdProperty = system.GetEntityURI("bcgov_businesscontacts", result.BcgovBusinesscontactid)
                        };
                        system.Accounts.AddReference(accountId, "bcgov_account_bcgov_businesscontact_BusinessProfile", oDataId);
                    }
                    catch (OdataerrorException odee)
                    {
                        if (_logger != null)
                        {
                            _logger.LogError(LoggingEvents.Error, "Error while adding reference from account to business contact");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                        }
                    }

                    // now link the contact

                    try
                    {
                        OdataId oDataId = new OdataId()
                        {
                            OdataIdProperty = system.GetEntityURI("bcgov_businesscontacts", result.BcgovBusinesscontactid)
                        };
                        system.Contacts.AddReference(contactId, "bcgov_contact_bcgov_businesscontact_Contact", oDataId);
                    }
                    catch (OdataerrorException odee)
                    {
                        if (_logger != null)
                        {
                            _logger.LogError(LoggingEvents.Error, "Error while adding reference from contact to business contact.");
                            _logger.LogError("Request:");
                            _logger.LogError(odee.Request.Content);
                            _logger.LogError("Response:");
                            _logger.LogError(odee.Response.Content);
                        }
                    }
                }
            }
            else
            {
                if (_logger != null)
                {
                    _logger.LogError(LoggingEvents.Error, "Invalid parameters passed.");
                    _logger.LogError(errorMessage);                    
                }
            }            
        }

        public static MicrosoftDynamicsCRMbcgovBusinesscontact GetBusinessContactLink(this IDynamicsClient system, ILogger _logger, string contactId, string accountId)
        {
            MicrosoftDynamicsCRMbcgovBusinesscontact result = null;            
            try
            {
                var businessContact = system.Businesscontacts.Get(filter: $"_bcgov_contact_value eq {contactId} and _bcgov_businessprofile_value eq {accountId}");
                result = businessContact.Value.FirstOrDefault();
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError(LoggingEvents.Error, "Error while getting a business contact.");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
            }

            return result;
        }

        public static MicrosoftDynamicsCRMbcgovBusinesscontact GetBusinessContactById(this IDynamicsClient system, string id)
        {
            MicrosoftDynamicsCRMbcgovBusinesscontact result = null;
            List<string> expand = new List<string>()
                {
                    "bcgov_Contact","bcgov_BusinessProfile"
                };
            try
            {
                result = system.Businesscontacts.GetByKey(bcgovBusinesscontactid:id,expand: expand);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }


        /// <summary>
        /// Delete any business contact links if they exist.
        /// </summary>
        /// <param name="system"></param>
        /// <param name="_logger"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static bool DeleteNonBceidBusinessContactLinkForAccount(this IDynamicsClient system, ILogger _logger, string accountId)
        {
            bool result = true;

            try
            {
                var businessContacts = system.Businesscontacts.Get(filter: $"_bcgov_businessprofile_value eq {accountId}");
                if (businessContacts.Value != null)
                {
                    foreach (MicrosoftDynamicsCRMbcgovBusinesscontact businessContact in businessContacts.Value)
                    {
                        if (businessContact.BcgovContacttype != (int?)ContactTypeCodes.BCeID)
                        {
                            try
                            {
                                system.Businesscontacts.Delete(businessContact.BcgovBusinesscontactid);
                            }
                            catch (OdataerrorException odee)
                            {
                                _logger.LogError(LoggingEvents.Error, "Error while deleting a business contact.");
                                _logger.LogError("Request:");
                                _logger.LogError(odee.Request.Content);
                                _logger.LogError("Response:");
                                _logger.LogError(odee.Response.Content);
                                result = false;
                            }
                        }
                    }
                }
                
            }
            catch (OdataerrorException odee)
            {
                _logger.LogError(LoggingEvents.Error, "Error while getting business contacts.");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Response.Content);
                result = false;
            }

            return result;
        }

    }
}
