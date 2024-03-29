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
    /// Extension methods for Riskassessmentincidentassociatedriskassessmentrecord.
    /// </summary>
    public static partial class RiskassessmentincidentassociatedriskassessmentrecordExtensions
    {
            /// <summary>
            /// Get bcgov_riskassessment_incident_AssociatedRiskAssessmentRecord from
            /// bcgov_riskassessments
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovRiskassessmentid'>
            /// key: bcgov_riskassessmentid of bcgov_riskassessment
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
            public static MicrosoftDynamicsCRMincidentCollection Get(this IRiskassessmentincidentassociatedriskassessmentrecord operations, string bcgovRiskassessmentid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetAsync(bcgovRiskassessmentid, top, skip, search, filter, count, orderby, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_riskassessment_incident_AssociatedRiskAssessmentRecord from
            /// bcgov_riskassessments
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovRiskassessmentid'>
            /// key: bcgov_riskassessmentid of bcgov_riskassessment
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
            public static async Task<MicrosoftDynamicsCRMincidentCollection> GetAsync(this IRiskassessmentincidentassociatedriskassessmentrecord operations, string bcgovRiskassessmentid, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(bcgovRiskassessmentid, top, skip, search, filter, count, orderby, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Get bcgov_riskassessment_incident_AssociatedRiskAssessmentRecord from
            /// bcgov_riskassessments
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovRiskassessmentid'>
            /// key: bcgov_riskassessmentid of bcgov_riskassessment
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
            public static MicrosoftDynamicsCRMincident AssociatedRiskAssessmentRecordByKey(this IRiskassessmentincidentassociatedriskassessmentrecord operations, string bcgovRiskassessmentid, string incidentid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.AssociatedRiskAssessmentRecordByKeyAsync(bcgovRiskassessmentid, incidentid, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get bcgov_riskassessment_incident_AssociatedRiskAssessmentRecord from
            /// bcgov_riskassessments
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovRiskassessmentid'>
            /// key: bcgov_riskassessmentid of bcgov_riskassessment
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
            public static async Task<MicrosoftDynamicsCRMincident> AssociatedRiskAssessmentRecordByKeyAsync(this IRiskassessmentincidentassociatedriskassessmentrecord operations, string bcgovRiskassessmentid, string incidentid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.AssociatedRiskAssessmentRecordByKeyWithHttpMessagesAsync(bcgovRiskassessmentid, incidentid, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
