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
    /// Extension methods for Regardingobjectidbcgovequipmentrecurringappointmentmaster.
    /// </summary>
    public static partial class RegardingobjectidbcgovequipmentrecurringappointmentmasterExtensions
    {
            /// <summary>
            /// Get regardingobjectid_bcgov_equipment_recurringappointmentmaster from
            /// recurringappointmentmasters
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='activityid'>
            /// key: activityid of recurringappointmentmaster
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMbcgovEquipment Get(this IRegardingobjectidbcgovequipmentrecurringappointmentmaster operations, string activityid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetAsync(activityid, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get regardingobjectid_bcgov_equipment_recurringappointmentmaster from
            /// recurringappointmentmasters
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='activityid'>
            /// key: activityid of recurringappointmentmaster
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
            public static async Task<MicrosoftDynamicsCRMbcgovEquipment> GetAsync(this IRegardingobjectidbcgovequipmentrecurringappointmentmaster operations, string activityid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(activityid, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
