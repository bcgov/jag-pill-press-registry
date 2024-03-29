// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for Customaddressincidentcivicaddressofpurchaser.
    /// </summary>
    public static partial class CustomaddressincidentcivicaddressofpurchaserExtensions
    {
            /// <summary>
            /// Get bcgov_customaddress_incident_CivicAddressofPurchaser from
            /// bcgov_customaddresses
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovCustomaddressid'>
            /// key: bcgov_customaddressid of bcgov_customaddress
            /// </param>
            /// <param name='top'>
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='search'>
            /// </param>
            /// <param name='filter'>
            /// </param>
            /// <param name='count'>
            /// </param>
            /// <param name='orderby'>
            /// Order items by property values
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMincidentCollection Get(this ICustomaddressincidentcivicaddressofpurchaser operations, string bcgovCustomaddressid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetAsync(bcgovCustomaddressid, top, skip, search, filter, count, orderby, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_customaddress_incident_CivicAddressofPurchaser from
            /// bcgov_customaddresses
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovCustomaddressid'>
            /// key: bcgov_customaddressid of bcgov_customaddress
            /// </param>
            /// <param name='top'>
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='search'>
            /// </param>
            /// <param name='filter'>
            /// </param>
            /// <param name='count'>
            /// </param>
            /// <param name='orderby'>
            /// Order items by property values
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<MicrosoftDynamicsCRMincidentCollection> GetAsync(this ICustomaddressincidentcivicaddressofpurchaser operations, string bcgovCustomaddressid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(bcgovCustomaddressid, top, skip, search, filter, count, orderby, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Get bcgov_customaddress_incident_CivicAddressofPurchaser from
            /// bcgov_customaddresses
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovCustomaddressid'>
            /// key: bcgov_customaddressid of bcgov_customaddress
            /// </param>
            /// <param name='incidentid'>
            /// key: incidentid of incident
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMincident CivicAddressofPurchaserByKey(this ICustomaddressincidentcivicaddressofpurchaser operations, string bcgovCustomaddressid, string incidentid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.CivicAddressofPurchaserByKeyAsync(bcgovCustomaddressid, incidentid, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_customaddress_incident_CivicAddressofPurchaser from
            /// bcgov_customaddresses
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovCustomaddressid'>
            /// key: bcgov_customaddressid of bcgov_customaddress
            /// </param>
            /// <param name='incidentid'>
            /// key: incidentid of incident
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<MicrosoftDynamicsCRMincident> CivicAddressofPurchaserByKeyAsync(this ICustomaddressincidentcivicaddressofpurchaser operations, string bcgovCustomaddressid, string incidentid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CivicAddressofPurchaserByKeyWithHttpMessagesAsync(bcgovCustomaddressid, incidentid, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
