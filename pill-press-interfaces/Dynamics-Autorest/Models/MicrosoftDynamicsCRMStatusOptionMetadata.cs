// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Gov.Jag.PillPressRegistry.Interfaces.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Microsoft.Dynamics.CRM.StatusOptionMetadata
    /// </summary>
    public partial class MicrosoftDynamicsCRMStatusOptionMetadata
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMStatusOptionMetadata class.
        /// </summary>
        public MicrosoftDynamicsCRMStatusOptionMetadata()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMStatusOptionMetadata class.
        /// </summary>
        public MicrosoftDynamicsCRMStatusOptionMetadata(int? state = default(int?), string transitionData = default(string))
        {
            State = state;
            TransitionData = transitionData;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "State")]
        public int? State { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "TransitionData")]
        public string TransitionData { get; set; }

    }
}
