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
    /// Collection of activityparty
    /// </summary>
    /// <remarks>
    /// Microsoft.Dynamics.CRM.activitypartyCollection
    /// </remarks>
    public partial class MicrosoftDynamicsCRMactivitypartyCollection
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMactivitypartyCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMactivitypartyCollection()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMactivitypartyCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMactivitypartyCollection(IList<MicrosoftDynamicsCRMactivityparty> value = default(IList<MicrosoftDynamicsCRMactivityparty>))
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
        public IList<MicrosoftDynamicsCRMactivityparty> Value { get; set; }

    }
}
