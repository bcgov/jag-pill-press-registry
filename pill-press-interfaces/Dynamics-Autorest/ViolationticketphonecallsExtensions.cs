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
    /// Extension methods for Violationticketphonecalls.
    /// </summary>
    public static partial class ViolationticketphonecallsExtensions
    {
            /// <summary>
            /// Get bcgov_violationticket_PhoneCalls from bcgov_violationtickets
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovViolationticketid'>
            /// key: bcgov_violationticketid of bcgov_violationticket
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
            public static MicrosoftDynamicsCRMphonecallCollection Get(this IViolationticketphonecalls operations, string bcgovViolationticketid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetAsync(bcgovViolationticketid, top, skip, search, filter, count, orderby, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_violationticket_PhoneCalls from bcgov_violationtickets
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovViolationticketid'>
            /// key: bcgov_violationticketid of bcgov_violationticket
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
            public static async Task<MicrosoftDynamicsCRMphonecallCollection> GetAsync(this IViolationticketphonecalls operations, string bcgovViolationticketid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(bcgovViolationticketid, top, skip, search, filter, count, orderby, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Get bcgov_violationticket_PhoneCalls from bcgov_violationtickets
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovViolationticketid'>
            /// key: bcgov_violationticketid of bcgov_violationticket
            /// </param>
            /// <param name='activityid'>
            /// key: activityid of phonecall
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMphonecall PhoneCallsByKey(this IViolationticketphonecalls operations, string bcgovViolationticketid, string activityid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.PhoneCallsByKeyAsync(bcgovViolationticketid, activityid, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_violationticket_PhoneCalls from bcgov_violationtickets
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovViolationticketid'>
            /// key: bcgov_violationticketid of bcgov_violationticket
            /// </param>
            /// <param name='activityid'>
            /// key: activityid of phonecall
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
            public static async Task<MicrosoftDynamicsCRMphonecall> PhoneCallsByKeyAsync(this IViolationticketphonecalls operations, string bcgovViolationticketid, string activityid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PhoneCallsByKeyWithHttpMessagesAsync(bcgovViolationticketid, activityid, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
