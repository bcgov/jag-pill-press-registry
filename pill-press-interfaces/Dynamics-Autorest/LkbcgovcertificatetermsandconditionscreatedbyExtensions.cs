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
    /// Extension methods for Lkbcgovcertificatetermsandconditionscreatedby.
    /// </summary>
    public static partial class LkbcgovcertificatetermsandconditionscreatedbyExtensions
    {
            /// <summary>
            /// Get lk_bcgov_certificatetermsandconditions_createdby from systemusers
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='ownerid'>
            /// key: ownerid of systemuser
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
            public static MicrosoftDynamicsCRMbcgovCertificatetermsandconditionsCollection Get(this ILkbcgovcertificatetermsandconditionscreatedby operations, string ownerid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetAsync(ownerid, top, skip, search, filter, count, orderby, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get lk_bcgov_certificatetermsandconditions_createdby from systemusers
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='ownerid'>
            /// key: ownerid of systemuser
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
            public static async Task<MicrosoftDynamicsCRMbcgovCertificatetermsandconditionsCollection> GetAsync(this ILkbcgovcertificatetermsandconditionscreatedby operations, string ownerid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(ownerid, top, skip, search, filter, count, orderby, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Get lk_bcgov_certificatetermsandconditions_createdby from systemusers
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='ownerid'>
            /// key: ownerid of systemuser
            /// </param>
            /// <param name='bcgovCertificatetermsandconditionsid'>
            /// key: bcgov_certificatetermsandconditionsid of
            /// bcgov_certificatetermsandconditions
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMbcgovCertificatetermsandconditions CreatedbyByKey(this ILkbcgovcertificatetermsandconditionscreatedby operations, string ownerid, string bcgovCertificatetermsandconditionsid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.CreatedbyByKeyAsync(ownerid, bcgovCertificatetermsandconditionsid, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get lk_bcgov_certificatetermsandconditions_createdby from systemusers
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='ownerid'>
            /// key: ownerid of systemuser
            /// </param>
            /// <param name='bcgovCertificatetermsandconditionsid'>
            /// key: bcgov_certificatetermsandconditionsid of
            /// bcgov_certificatetermsandconditions
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
            public static async Task<MicrosoftDynamicsCRMbcgovCertificatetermsandconditions> CreatedbyByKeyAsync(this ILkbcgovcertificatetermsandconditionscreatedby operations, string ownerid, string bcgovCertificatetermsandconditionsid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CreatedbyByKeyWithHttpMessagesAsync(ownerid, bcgovCertificatetermsandconditionsid, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
