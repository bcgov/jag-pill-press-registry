

using Gov.Jag.PillPressRegistry.Interfaces;
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;

namespace DemoTool
{
    /// <summary>
    /// Utility Program to backfill data in the event of a problem with BCeID
    /// This tool can also be used to generate an export of active users
    /// </summary>
    class Program
    {
        // the one parameter is the BCeID guid for an individual.
        static void Main(string[] args)
        {
            bool isClean = false;
            

            if (args.Length > 0)
            {
                string arg = args[0];
                if (!string.IsNullOrEmpty(arg))
                {
                    if (arg.ToLower().Equals("clean"))
                    {
                        isClean = true;
                        Console.Out.WriteLine("Clean users enabled");
                    }
                    else
                    {
                        Console.Out.WriteLine("USAGE - enter the clean parameter to clean users for the given environment.");
                    }
                }
            }

            string dynamicsOdataUri = Environment.GetEnvironmentVariable("DYNAMICS_ODATA_URI");
            string ssgUsername = Environment.GetEnvironmentVariable("SSG_USERNAME");
            string ssgPassword = Environment.GetEnvironmentVariable("SSG_PASSWORD");
            string dynamicsNativeOdataUri = Environment.GetEnvironmentVariable("DYNAMICS_NATIVE_ODATA_URI");


            string aadTenantId = Environment.GetEnvironmentVariable("DYNAMICS_AAD_TENANT_ID");
            string serverAppIdUri = Environment.GetEnvironmentVariable("DYNAMICS_SERVER_APP_ID_URI");
            string clientKey = Environment.GetEnvironmentVariable("DYNAMICS_CLIENT_KEY");
            string clientId = Environment.GetEnvironmentVariable("DYNAMICS_CLIENT_ID");

            ServiceClientCredentials serviceClientCredentials = null;

            if (string.IsNullOrEmpty(ssgUsername) || string.IsNullOrEmpty(ssgPassword))
            {
                var authenticationContext = new AuthenticationContext(
                "https://login.windows.net/" + aadTenantId);
                ClientCredential clientCredential = new ClientCredential(clientId, clientKey);
                var task = authenticationContext.AcquireTokenAsync(serverAppIdUri, clientCredential);
                task.Wait();
                var authenticationResult = task.Result;
                string token = authenticationResult.CreateAuthorizationHeader().Substring("Bearer ".Length);
                serviceClientCredentials = new TokenCredentials(token);
            }
            else
            {
                serviceClientCredentials = new BasicAuthenticationCredentials()
                {
                    UserName = ssgUsername,
                    Password = ssgPassword
                };
            }
            

            var _dynamicsClient = new DynamicsClient(new Uri(dynamicsOdataUri), serviceClientCredentials);

            if (isClean)
            {

                // remove all BusinessContacts.
                var businessContacts = _dynamicsClient.Businesscontacts.Get().Value;

                foreach (var businessContact in businessContacts)
                {
                    try
                    {
                        _dynamicsClient.Businesscontacts.Delete(businessContact.BcgovBusinesscontactid);
                        Console.Out.WriteLine("Deleted BusinessContact " + businessContact.BcgovBusinesscontactid);
                    }
                    catch (OdataerrorException odee)
                    {
                        Console.Out.WriteLine("Error deleting business contact");
                        Console.Out.WriteLine("Request:");
                        Console.Out.WriteLine(odee.Request.Content);
                        Console.Out.WriteLine("Response:");
                        Console.Out.WriteLine(odee.Response.Content);
                    }
                }

                // remove all incidents (waiver applications etc)
                var incidents = _dynamicsClient.Incidents.Get().Value;

                foreach (var incident in incidents)
                {
                    try
                    {
                        _dynamicsClient.Incidents.Delete(incident.Incidentid);
                        Console.Out.WriteLine("Deleted Incident " + incident.Incidentid);
                    }
                    catch (OdataerrorException odee)
                    {
                        Console.Out.WriteLine("Error deleting incident");
                        Console.Out.WriteLine("Request:");
                        Console.Out.WriteLine(odee.Request.Content);
                        Console.Out.WriteLine("Response:");
                        Console.Out.WriteLine(odee.Response.Content);
                    }
                }


                // remove all business profiles.
                var businessProfiles = _dynamicsClient.Accounts.Get().Value;

                foreach (var businessProfile in businessProfiles)
                {
                    try
                    {
                        _dynamicsClient.Accounts.Delete(businessProfile.Accountid);
                        Console.Out.WriteLine("Deleted BusinessProfile " + businessProfile.Accountid);
                    }
                    catch (OdataerrorException odee)
                    {
                        Console.Out.WriteLine("Error deleting business profile");
                        Console.Out.WriteLine("Request:");
                        Console.Out.WriteLine(odee.Request.Content);
                        Console.Out.WriteLine("Response:");
                        Console.Out.WriteLine(odee.Response.Content);
                    }
                }

                // remove contacts

                var contacts = _dynamicsClient.Contacts.Get().Value;

                foreach (var contact in contacts)
                {
                    try
                    {
                        _dynamicsClient.Contacts.Delete(contact.Contactid);
                        Console.Out.WriteLine("Deleted Contact " + contact.Contactid);
                    }
                    catch (OdataerrorException odee)
                    {
                        Console.Out.WriteLine("Error deleting contact");
                        Console.Out.WriteLine("Request:");
                        Console.Out.WriteLine(odee.Request.Content);
                        Console.Out.WriteLine("Response:");
                        Console.Out.WriteLine(odee.Response.Content);
                    }
                }

                // remove customAddresses

                var customAddresses = _dynamicsClient.Customaddresses.Get().Value;

                foreach (var item in customAddresses)
                {
                    try
                    {
                        _dynamicsClient.Customaddresses.Delete(item.BcgovCustomaddressid);
                        Console.Out.WriteLine("Deleted CustomAddress " + item.BcgovCustomaddressid);
                    }
                    catch (OdataerrorException odee)
                    {
                        Console.Out.WriteLine("Error deleting CustomAddress");
                        Console.Out.WriteLine("Request:");
                        Console.Out.WriteLine(odee.Request.Content);
                        Console.Out.WriteLine("Response:");
                        Console.Out.WriteLine(odee.Response.Content);
                    }
                }

                // remove customProducts

                var customProducts = _dynamicsClient.Customproducts.Get().Value;

                foreach (var item in customProducts)
                {
                    try
                    {
                        _dynamicsClient.Customaddresses.Delete(item.BcgovCustomproductid);
                        Console.Out.WriteLine("Deleted customProducts " + item.BcgovCustomproductid);
                    }
                    catch (OdataerrorException odee)
                    {
                        Console.Out.WriteLine("Error deleting customProduct");
                        Console.Out.WriteLine("Request:");
                        Console.Out.WriteLine(odee.Request.Content);
                        Console.Out.WriteLine("Response:");
                        Console.Out.WriteLine(odee.Response.Content);
                    }
                }

                var certificates = _dynamicsClient.Certificates.Get().Value;

                foreach (var item in certificates)
                {
                    try
                    {
                        _dynamicsClient.Certificates.Delete(item.BcgovCertificateid);
                        Console.Out.WriteLine("Deleted certificate " + item.BcgovCertificateid);
                    }
                    catch (OdataerrorException odee)
                    {
                        Console.Out.WriteLine("Error deleting certificate");
                        Console.Out.WriteLine("Request:");
                        Console.Out.WriteLine(odee.Request.Content);
                        Console.Out.WriteLine("Response:");
                        Console.Out.WriteLine(odee.Response.Content);
                    }
                }

                var riskAssessments = _dynamicsClient.Riskassessments.Get().Value;

                foreach (var item in riskAssessments)
                {
                    try
                    {
                        _dynamicsClient.Riskassessments.Delete(item.BcgovRiskassessmentid);
                        Console.Out.WriteLine("Deleted Riskassessment " + item.BcgovRiskassessmentid);
                    }
                    catch (OdataerrorException odee)
                    {
                        Console.Out.WriteLine("Error deleting Riskassessment");
                        Console.Out.WriteLine("Request:");
                        Console.Out.WriteLine(odee.Request.Content);
                        Console.Out.WriteLine("Response:");
                        Console.Out.WriteLine(odee.Response.Content);
                    }
                }

                var equipmentLocations = _dynamicsClient.Equipmentlocations.Get().Value;

                foreach (var item in equipmentLocations)
                {
                    try
                    {
                        _dynamicsClient.Riskassessments.Delete(item.BcgovEquipmentlocationid);
                        Console.Out.WriteLine("Deleted Equipmentlocation " + item.BcgovEquipmentlocationid);
                    }
                    catch (OdataerrorException odee)
                    {
                        Console.Out.WriteLine("Error deleting Equipmentlocation");
                        Console.Out.WriteLine("Request:");
                        Console.Out.WriteLine(odee.Request.Content);
                        Console.Out.WriteLine("Response:");
                        Console.Out.WriteLine(odee.Response.Content);
                    }
                }

                var locations = _dynamicsClient.Locations.Get().Value;

                foreach (var item in locations)
                {
                    try
                    {
                        _dynamicsClient.Riskassessments.Delete(item.BcgovLocationid);
                        Console.Out.WriteLine("Deleted location " + item.BcgovLocationid);
                    }
                    catch (OdataerrorException odee)
                    {
                        Console.Out.WriteLine("Error deleting location");
                        Console.Out.WriteLine("Request:");
                        Console.Out.WriteLine(odee.Request.Content);
                        Console.Out.WriteLine("Response:");
                        Console.Out.WriteLine(odee.Response.Content);
                    }
                }

                var equipment = _dynamicsClient.Equipments.Get().Value;

                foreach (var item in equipment)
                {
                    try
                    {
                        _dynamicsClient.Riskassessments.Delete(item.BcgovEquipmentid);
                        Console.Out.WriteLine("Deleted equipment " + item.BcgovEquipmentid);
                    }
                    catch (OdataerrorException odee)
                    {
                        Console.Out.WriteLine("Error deleting equipment");
                        Console.Out.WriteLine("Request:");
                        Console.Out.WriteLine(odee.Request.Content);
                        Console.Out.WriteLine("Response:");
                        Console.Out.WriteLine(odee.Response.Content);
                    }
                }

            }
        }
    }
}
