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
    /// Extension methods for Locationcontact.
    /// </summary>
    public static partial class LocationcontactExtensions
    {
            /// <summary>
            /// Get bcgov_LocationContact from bcgov_inspections
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovInspectionid'>
            /// key: bcgov_inspectionid of bcgov_inspection
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMcontact Get(this ILocationcontact operations, string bcgovInspectionid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetAsync(bcgovInspectionid, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_LocationContact from bcgov_inspections
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovInspectionid'>
            /// key: bcgov_inspectionid of bcgov_inspection
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
            public static async Task<MicrosoftDynamicsCRMcontact> GetAsync(this ILocationcontact operations, string bcgovInspectionid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(bcgovInspectionid, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Get bcgov_LocationContact from bcgov_locations
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovLocationid'>
            /// key: bcgov_locationid of bcgov_location
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMcontact Get1(this ILocationcontact operations, string bcgovLocationid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.Get1Async(bcgovLocationid, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_LocationContact from bcgov_locations
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovLocationid'>
            /// key: bcgov_locationid of bcgov_location
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
            public static async Task<MicrosoftDynamicsCRMcontact> Get1Async(this ILocationcontact operations, string bcgovLocationid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.Get1WithHttpMessagesAsync(bcgovLocationid, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
