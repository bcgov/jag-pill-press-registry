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
    /// Extension methods for Complaintequipmentset.
    /// </summary>
    public static partial class ComplaintequipmentsetExtensions
    {
            /// <summary>
            /// Get entities from bcgov_complaint_equipmentset
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='top'>
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
            public static MicrosoftDynamicsCRMbcgovComplaintEquipmentCollection Get(this IComplaintequipmentset operations, int? top = default(int?), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetAsync(top, filter, count, orderby, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get entities from bcgov_complaint_equipmentset
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='top'>
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
            public static async Task<MicrosoftDynamicsCRMbcgovComplaintEquipmentCollection> GetAsync(this IComplaintequipmentset operations, int? top = default(int?), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(top, filter, count, orderby, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Add new entity to bcgov_complaint_equipmentset
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='body'>
            /// New entity
            /// </param>
            /// <param name='prefer'>
            /// Required in order for the service to return a JSON representation of the
            /// object.
            /// </param>
            public static MicrosoftDynamicsCRMbcgovComplaintEquipment Create(this IComplaintequipmentset operations, MicrosoftDynamicsCRMbcgovComplaintEquipment body, string prefer = "return=representation")
            {
                return operations.CreateAsync(body, prefer).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Add new entity to bcgov_complaint_equipmentset
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='body'>
            /// New entity
            /// </param>
            /// <param name='prefer'>
            /// Required in order for the service to return a JSON representation of the
            /// object.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<MicrosoftDynamicsCRMbcgovComplaintEquipment> CreateAsync(this IComplaintequipmentset operations, MicrosoftDynamicsCRMbcgovComplaintEquipment body, string prefer = "return=representation", CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CreateWithHttpMessagesAsync(body, prefer, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Get entity from bcgov_complaint_equipmentset by key
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovComplaintEquipmentid'>
            /// key: bcgov_complaint_equipmentid of bcgov_complaint_equipment
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMbcgovComplaintEquipment GetByKey(this IComplaintequipmentset operations, string bcgovComplaintEquipmentid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetByKeyAsync(bcgovComplaintEquipmentid, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get entity from bcgov_complaint_equipmentset by key
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovComplaintEquipmentid'>
            /// key: bcgov_complaint_equipmentid of bcgov_complaint_equipment
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
            public static async Task<MicrosoftDynamicsCRMbcgovComplaintEquipment> GetByKeyAsync(this IComplaintequipmentset operations, string bcgovComplaintEquipmentid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetByKeyWithHttpMessagesAsync(bcgovComplaintEquipmentid, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Update entity in bcgov_complaint_equipmentset
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovComplaintEquipmentid'>
            /// key: bcgov_complaint_equipmentid of bcgov_complaint_equipment
            /// </param>
            /// <param name='body'>
            /// New property values
            /// </param>
            public static void Update(this IComplaintequipmentset operations, string bcgovComplaintEquipmentid, MicrosoftDynamicsCRMbcgovComplaintEquipment body)
            {
                operations.UpdateAsync(bcgovComplaintEquipmentid, body).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Update entity in bcgov_complaint_equipmentset
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovComplaintEquipmentid'>
            /// key: bcgov_complaint_equipmentid of bcgov_complaint_equipment
            /// </param>
            /// <param name='body'>
            /// New property values
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task UpdateAsync(this IComplaintequipmentset operations, string bcgovComplaintEquipmentid, MicrosoftDynamicsCRMbcgovComplaintEquipment body, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.UpdateWithHttpMessagesAsync(bcgovComplaintEquipmentid, body, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Delete entity from bcgov_complaint_equipmentset
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovComplaintEquipmentid'>
            /// key: bcgov_complaint_equipmentid of bcgov_complaint_equipment
            /// </param>
            /// <param name='ifMatch'>
            /// ETag
            /// </param>
            public static void Delete(this IComplaintequipmentset operations, string bcgovComplaintEquipmentid, string ifMatch = default(string))
            {
                operations.DeleteAsync(bcgovComplaintEquipmentid, ifMatch).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Delete entity from bcgov_complaint_equipmentset
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='bcgovComplaintEquipmentid'>
            /// key: bcgov_complaint_equipmentid of bcgov_complaint_equipment
            /// </param>
            /// <param name='ifMatch'>
            /// ETag
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteAsync(this IComplaintequipmentset operations, string bcgovComplaintEquipmentid, string ifMatch = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeleteWithHttpMessagesAsync(bcgovComplaintEquipmentid, ifMatch, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

    }
}
