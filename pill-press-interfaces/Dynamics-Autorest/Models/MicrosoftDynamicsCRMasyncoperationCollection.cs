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
    /// Collection of asyncoperation
    /// </summary>
    /// <remarks>
    /// Microsoft.Dynamics.CRM.asyncoperationCollection
    /// </remarks>
    public partial class MicrosoftDynamicsCRMasyncoperationCollection
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMasyncoperationCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMasyncoperationCollection()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMasyncoperationCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMasyncoperationCollection(IList<MicrosoftDynamicsCRMasyncoperation> value = default(IList<MicrosoftDynamicsCRMasyncoperation>))
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
        public IList<MicrosoftDynamicsCRMasyncoperation> Value { get; set; }

    }
}
