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
    /// Extension methods for Certificateaccountcurrentwaivercertificate.
    /// </summary>
    public static partial class CertificateaccountcurrentwaivercertificateExtensions
    {
            /// <summary>
            /// Get bcgov_certificate_account_CurrentWaiverCertificate from
            /// bcgov_certificates
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovCertificateid'>
            /// key: bcgov_certificateid of bcgov_certificate
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
            public static MicrosoftDynamicsCRMaccountCollection Get(this ICertificateaccountcurrentwaivercertificate operations, string bcgovCertificateid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetAsync(bcgovCertificateid, top, skip, search, filter, count, orderby, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_certificate_account_CurrentWaiverCertificate from
            /// bcgov_certificates
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovCertificateid'>
            /// key: bcgov_certificateid of bcgov_certificate
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
            public static async Task<MicrosoftDynamicsCRMaccountCollection> GetAsync(this ICertificateaccountcurrentwaivercertificate operations, string bcgovCertificateid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(bcgovCertificateid, top, skip, search, filter, count, orderby, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Get bcgov_certificate_account_CurrentWaiverCertificate from
            /// bcgov_certificates
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovCertificateid'>
            /// key: bcgov_certificateid of bcgov_certificate
            /// </param>
            /// <param name='accountid'>
            /// key: accountid of account
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMaccount CurrentWaiverCertificateByKey(this ICertificateaccountcurrentwaivercertificate operations, string bcgovCertificateid, string accountid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.CurrentWaiverCertificateByKeyAsync(bcgovCertificateid, accountid, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_certificate_account_CurrentWaiverCertificate from
            /// bcgov_certificates
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovCertificateid'>
            /// key: bcgov_certificateid of bcgov_certificate
            /// </param>
            /// <param name='accountid'>
            /// key: accountid of account
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
            public static async Task<MicrosoftDynamicsCRMaccount> CurrentWaiverCertificateByKeyAsync(this ICertificateaccountcurrentwaivercertificate operations, string bcgovCertificateid, string accountid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CurrentWaiverCertificateByKeyWithHttpMessagesAsync(bcgovCertificateid, accountid, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
