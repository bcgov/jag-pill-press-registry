
using Gov.Jag.PillPressRegistry.Interfaces;
using Gov.Jag.PillPressRegistry.Interfaces.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.Serialization;

namespace Gov.Jag.PillPressRegistry.Public.Utils
{
    /// <summary>
    /// Helper methods for working with shared logic within controllers
    /// </summary>
    public static class ControllerUtility
    {
        public static int? GetAttributeMaxLength(DynamicsClient client, IMemoryCache cache, ILogger logger, string entityName, string attributeName)
        {
            try
            {
                int? maxLength = null;
                if (!cache.TryGetValue("", out maxLength))
                {

                    maxLength = DynamicsAutorest.Extensions.ClientExtensions.GetEntityAttribute<MicrosoftDynamicsCRMMemoAttributeMetadata>(
                        (DynamicsClient)client, "Microsoft.Dynamics.CRM.MemoAttributeMetadata", entityName, attributeName)?.MaxLength;

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    cache.Set(attributeName, maxLength, cacheEntryOptions);
                }
                return maxLength;
            }
            catch (OdataerrorException odee)
            {
                logger.LogError("Error retrieving Entity Attribute");
                logger.LogError("Request:");
                logger.LogError(odee.Request.Content);
                logger.LogError("Response:");
                logger.LogError(odee.Response.Content);
            }
            catch (SerializationException se)
            {
                logger.LogError("Error retrieving Entity Attribute");
                logger.LogError("Message:");
                logger.LogError(se.Message);
            }
            return null;
        }
    }
}
