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
    /// Microsoft.Dynamics.CRM.bcgov_inspectionchecklist_actregu
    /// </summary>
    public partial class MicrosoftDynamicsCRMbcgovInspectionchecklistActregu
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMbcgovInspectionchecklistActregu class.
        /// </summary>
        public MicrosoftDynamicsCRMbcgovInspectionchecklistActregu()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMbcgovInspectionchecklistActregu class.
        /// </summary>
        public MicrosoftDynamicsCRMbcgovInspectionchecklistActregu(string bcgovInspectionchecklistActreguid = default(string), string versionnumber = default(string), string bcgovInspectionchecklistid = default(string), string bcgovActregulationreferenceid = default(string))
        {
            BcgovInspectionchecklistActreguid = bcgovInspectionchecklistActreguid;
            Versionnumber = versionnumber;
            BcgovInspectionchecklistid = bcgovInspectionchecklistid;
            BcgovActregulationreferenceid = bcgovActregulationreferenceid;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_inspectionchecklist_actreguid")]
        public string BcgovInspectionchecklistActreguid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_inspectionchecklistid")]
        public string BcgovInspectionchecklistid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_actregulationreferenceid")]
        public string BcgovActregulationreferenceid { get; set; }

    }
}
