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
    /// Collection of bcgov_inspectionprocessflow
    /// </summary>
    /// <remarks>
    /// Microsoft.Dynamics.CRM.bcgov_inspectionprocessflowCollection
    /// </remarks>
    public partial class MicrosoftDynamicsCRMbcgovInspectionprocessflowCollection
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMbcgovInspectionprocessflowCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMbcgovInspectionprocessflowCollection()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMbcgovInspectionprocessflowCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMbcgovInspectionprocessflowCollection(IList<MicrosoftDynamicsCRMbcgovInspectionprocessflow> value = default(IList<MicrosoftDynamicsCRMbcgovInspectionprocessflow>))
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
        public IList<MicrosoftDynamicsCRMbcgovInspectionprocessflow> Value { get; set; }

    }
}