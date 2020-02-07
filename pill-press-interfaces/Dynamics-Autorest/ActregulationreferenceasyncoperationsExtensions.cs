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
    /// Extension methods for Actregulationreferenceasyncoperations.
    /// </summary>
    public static partial class ActregulationreferenceasyncoperationsExtensions
    {
            /// <summary>
            /// Get bcgov_actregulationreference_AsyncOperations from
            /// bcgov_actregulationreferences
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovActregulationreferenceid'>
            /// key: bcgov_actregulationreferenceid of bcgov_actregulationreference
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
            public static MicrosoftDynamicsCRMasyncoperationCollection Get(this IActregulationreferenceasyncoperations operations, string bcgovActregulationreferenceid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetAsync(bcgovActregulationreferenceid, top, skip, search, filter, count, orderby, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_actregulationreference_AsyncOperations from
            /// bcgov_actregulationreferences
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovActregulationreferenceid'>
            /// key: bcgov_actregulationreferenceid of bcgov_actregulationreference
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
            public static async Task<MicrosoftDynamicsCRMasyncoperationCollection> GetAsync(this IActregulationreferenceasyncoperations operations, string bcgovActregulationreferenceid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(bcgovActregulationreferenceid, top, skip, search, filter, count, orderby, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Get bcgov_actregulationreference_AsyncOperations from
            /// bcgov_actregulationreferences
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovActregulationreferenceid'>
            /// key: bcgov_actregulationreferenceid of bcgov_actregulationreference
            /// </param>
            /// <param name='asyncoperationid'>
            /// key: asyncoperationid of asyncoperation
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMasyncoperation AsyncOperationsByKey(this IActregulationreferenceasyncoperations operations, string bcgovActregulationreferenceid, string asyncoperationid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.AsyncOperationsByKeyAsync(bcgovActregulationreferenceid, asyncoperationid, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_actregulationreference_AsyncOperations from
            /// bcgov_actregulationreferences
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovActregulationreferenceid'>
            /// key: bcgov_actregulationreferenceid of bcgov_actregulationreference
            /// </param>
            /// <param name='asyncoperationid'>
            /// key: asyncoperationid of asyncoperation
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
            public static async Task<MicrosoftDynamicsCRMasyncoperation> AsyncOperationsByKeyAsync(this IActregulationreferenceasyncoperations operations, string bcgovActregulationreferenceid, string asyncoperationid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.AsyncOperationsByKeyWithHttpMessagesAsync(bcgovActregulationreferenceid, asyncoperationid, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}