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
    /// Microsoft.Dynamics.CRM.customcontrol
    /// </summary>
    public partial class MicrosoftDynamicsCRMcustomcontrol
    {
        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMcustomcontrol
        /// class.
        /// </summary>
        public MicrosoftDynamicsCRMcustomcontrol()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMcustomcontrol
        /// class.
        /// </summary>
        /// <param name="createdon">Date and time when the record was
        /// created.</param>
        /// <param name="introducedversion">Version in which the form is
        /// introduced.</param>
        /// <param name="overwritetime">For internal use only.</param>
        /// <param name="_modifiedbyValue">Unique identifier of the user who
        /// modified the record.</param>
        /// <param name="customcontrolidunique">For internal use only.</param>
        /// <param name="_createdonbehalfbyValue">Unique identifier of the
        /// delegate user who created the record.</param>
        /// <param name="compatibledatatypes">Compatible Data Types For Custom
        /// Controls</param>
        /// <param name="_modifiedonbehalfbyValue">Unique identifier of the
        /// delegate user who modified the record.</param>
        /// <param name="customcontrolid">Unique identifier of the Custom
        /// Control for the Microsoft Dynamics 365.</param>
        /// <param name="version">For internal use only.</param>
        /// <param name="solutionid">Unique identifier of the associated
        /// solution.</param>
        /// <param name="componentstate">For internal use only.</param>
        /// <param name="versionnumber">Version number of the Custom
        /// Control.</param>
        /// <param name="_createdbyValue">Unique identifier of the user who
        /// created the record.</param>
        /// <param name="modifiedon">Date and time when the record was
        /// modified.</param>
        /// <param name="manifest">Manifest of the CustomControl.</param>
        /// <param name="_organizationidValue">Unique identifier of the
        /// organization associated with the custom control.</param>
        /// <param name="name">Name of the custom control.</param>
        public MicrosoftDynamicsCRMcustomcontrol(System.DateTimeOffset? createdon = default(System.DateTimeOffset?), string introducedversion = default(string), System.DateTimeOffset? overwritetime = default(System.DateTimeOffset?), bool? ismanaged = default(bool?), string _modifiedbyValue = default(string), string customcontrolidunique = default(string), string _createdonbehalfbyValue = default(string), string compatibledatatypes = default(string), string _modifiedonbehalfbyValue = default(string), string customcontrolid = default(string), string version = default(string), string solutionid = default(string), int? componentstate = default(int?), string versionnumber = default(string), string _createdbyValue = default(string), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), string manifest = default(string), string _organizationidValue = default(string), string name = default(string), MicrosoftDynamicsCRMsystemuser modifiedonbehalfby = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMcustomcontrolresource> customcontrolResourceId = default(IList<MicrosoftDynamicsCRMcustomcontrolresource>), MicrosoftDynamicsCRMsystemuser createdby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMorganization organizationid = default(MicrosoftDynamicsCRMorganization), MicrosoftDynamicsCRMsystemuser modifiedby = default(MicrosoftDynamicsCRMsystemuser))
        {
            Createdon = createdon;
            Introducedversion = introducedversion;
            Overwritetime = overwritetime;
            Ismanaged = ismanaged;
            this._modifiedbyValue = _modifiedbyValue;
            Customcontrolidunique = customcontrolidunique;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            Compatibledatatypes = compatibledatatypes;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            Customcontrolid = customcontrolid;
            Version = version;
            Solutionid = solutionid;
            Componentstate = componentstate;
            Versionnumber = versionnumber;
            this._createdbyValue = _createdbyValue;
            Modifiedon = modifiedon;
            Manifest = manifest;
            this._organizationidValue = _organizationidValue;
            Name = name;
            Modifiedonbehalfby = modifiedonbehalfby;
            CustomcontrolResourceId = customcontrolResourceId;
            Createdby = createdby;
            Createdonbehalfby = createdonbehalfby;
            Organizationid = organizationid;
            Modifiedby = modifiedby;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets date and time when the record was created.
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// Gets or sets version in which the form is introduced.
        /// </summary>
        [JsonProperty(PropertyName = "introducedversion")]
        public string Introducedversion { get; set; }

        /// <summary>
        /// Gets or sets for internal use only.
        /// </summary>
        [JsonProperty(PropertyName = "overwritetime")]
        public System.DateTimeOffset? Overwritetime { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ismanaged")]
        public bool? Ismanaged { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who modified the record.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// Gets or sets for internal use only.
        /// </summary>
        [JsonProperty(PropertyName = "customcontrolidunique")]
        public string Customcontrolidunique { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who created the
        /// record.
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets compatible Data Types For Custom Controls
        /// </summary>
        [JsonProperty(PropertyName = "compatibledatatypes")]
        public string Compatibledatatypes { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who modified
        /// the record.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the Custom Control for the
        /// Microsoft Dynamics 365.
        /// </summary>
        [JsonProperty(PropertyName = "customcontrolid")]
        public string Customcontrolid { get; set; }

        /// <summary>
        /// Gets or sets for internal use only.
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the associated solution.
        /// </summary>
        [JsonProperty(PropertyName = "solutionid")]
        public string Solutionid { get; set; }

        /// <summary>
        /// Gets or sets for internal use only.
        /// </summary>
        [JsonProperty(PropertyName = "componentstate")]
        public int? Componentstate { get; set; }

        /// <summary>
        /// Gets or sets version number of the Custom Control.
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who created the record.
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// Gets or sets date and time when the record was modified.
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// Gets or sets manifest of the CustomControl.
        /// </summary>
        [JsonProperty(PropertyName = "manifest")]
        public string Manifest { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the organization associated with
        /// the custom control.
        /// </summary>
        [JsonProperty(PropertyName = "_organizationid_value")]
        public string _organizationidValue { get; set; }

        /// <summary>
        /// Gets or sets name of the custom control.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "customcontrol_resource_id")]
        public IList<MicrosoftDynamicsCRMcustomcontrolresource> CustomcontrolResourceId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdby")]
        public MicrosoftDynamicsCRMsystemuser Createdby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser Createdonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "organizationid")]
        public MicrosoftDynamicsCRMorganization Organizationid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedby { get; set; }

    }
}
