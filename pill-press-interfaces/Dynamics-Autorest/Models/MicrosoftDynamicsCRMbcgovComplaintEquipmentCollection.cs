// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Collection of bcgov_complaint_equipment
    /// </summary>
    /// <remarks>
    /// Microsoft.Dynamics.CRM.bcgov_complaint_equipmentCollection
    /// </remarks>
    public partial class MicrosoftDynamicsCRMbcgovComplaintEquipmentCollection
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMbcgovComplaintEquipmentCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMbcgovComplaintEquipmentCollection()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMbcgovComplaintEquipmentCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMbcgovComplaintEquipmentCollection(IList<MicrosoftDynamicsCRMbcgovComplaintEquipment> value = default(IList<MicrosoftDynamicsCRMbcgovComplaintEquipment>))
        {
            Value = value;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public IList<MicrosoftDynamicsCRMbcgovComplaintEquipment> Value { get; set; }

    }
}