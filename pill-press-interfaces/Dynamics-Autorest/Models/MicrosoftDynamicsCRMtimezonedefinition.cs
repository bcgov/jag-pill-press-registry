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
    /// Microsoft.Dynamics.CRM.timezonedefinition
    /// </summary>
    public partial class MicrosoftDynamicsCRMtimezonedefinition
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMtimezonedefinition class.
        /// </summary>
        public MicrosoftDynamicsCRMtimezonedefinition()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMtimezonedefinition class.
        /// </summary>
        /// <param name="userinterfacename">Display name for the time zone in
        /// the Microsoft Windows registry.</param>
        /// <param name="timezonedefinitionid">Unique identifier of the time
        /// zone record.</param>
        /// <param name="_modifiedonbehalfbyValue">Unique identifier of the
        /// delegate user who last modified the timezonedefinition.</param>
        /// <param name="_organizationidValue">Unique identifier of the
        /// organization associated with the time zone definition.</param>
        /// <param name="bias">Base time bias of the time zone.</param>
        /// <param name="_createdonbehalfbyValue">Unique identifier of the
        /// delegate user who created the timezonedefinition.</param>
        /// <param name="createdon">Date and time when the time zone record was
        /// created.</param>
        /// <param name="timezonecode">Time zone identification code.</param>
        /// <param name="_modifiedbyValue">Unique identifier of the user who
        /// last modified the time zone record.</param>
        /// <param name="_createdbyValue">Unique identifier of the user who
        /// created the time zone record.</param>
        /// <param name="standardname">Time zone name for the standard
        /// time.</param>
        /// <param name="retiredorder">Order an entry for a time zone
        /// definition is retired. 0 for the latest entry.</param>
        /// <param name="modifiedon">Date and time when the time zone record
        /// was modified.</param>
        /// <param name="daylightname">Time zone name for the daylight
        /// time.</param>
        public MicrosoftDynamicsCRMtimezonedefinition(string userinterfacename = default(string), string timezonedefinitionid = default(string), string versionnumber = default(string), string _modifiedonbehalfbyValue = default(string), string _organizationidValue = default(string), int? bias = default(int?), string _createdonbehalfbyValue = default(string), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), int? timezonecode = default(int?), string _modifiedbyValue = default(string), string _createdbyValue = default(string), string standardname = default(string), int? retiredorder = default(int?), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), string daylightname = default(string), MicrosoftDynamicsCRMsystemuser modifiedby = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMtimezonelocalizedname> lkTimezonelocalizednameTimezonedefinitionid = default(IList<MicrosoftDynamicsCRMtimezonelocalizedname>), MicrosoftDynamicsCRMsystemuser createdonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedonbehalfby = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMtimezonerule> lkTimezoneruleTimezonedefinitionid = default(IList<MicrosoftDynamicsCRMtimezonerule>))
        {
            Userinterfacename = userinterfacename;
            Timezonedefinitionid = timezonedefinitionid;
            Versionnumber = versionnumber;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            this._organizationidValue = _organizationidValue;
            Bias = bias;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            Createdon = createdon;
            Timezonecode = timezonecode;
            this._modifiedbyValue = _modifiedbyValue;
            this._createdbyValue = _createdbyValue;
            Standardname = standardname;
            Retiredorder = retiredorder;
            Modifiedon = modifiedon;
            Daylightname = daylightname;
            Modifiedby = modifiedby;
            LkTimezonelocalizednameTimezonedefinitionid = lkTimezonelocalizednameTimezonedefinitionid;
            Createdonbehalfby = createdonbehalfby;
            Createdby = createdby;
            Modifiedonbehalfby = modifiedonbehalfby;
            LkTimezoneruleTimezonedefinitionid = lkTimezoneruleTimezonedefinitionid;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets display name for the time zone in the Microsoft
        /// Windows registry.
        /// </summary>
        [JsonProperty(PropertyName = "userinterfacename")]
        public string Userinterfacename { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the time zone record.
        /// </summary>
        [JsonProperty(PropertyName = "timezonedefinitionid")]
        public string Timezonedefinitionid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who last
        /// modified the timezonedefinition.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the organization associated with
        /// the time zone definition.
        /// </summary>
        [JsonProperty(PropertyName = "_organizationid_value")]
        public string _organizationidValue { get; set; }

        /// <summary>
        /// Gets or sets base time bias of the time zone.
        /// </summary>
        [JsonProperty(PropertyName = "bias")]
        public int? Bias { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who created the
        /// timezonedefinition.
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets date and time when the time zone record was created.
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// Gets or sets time zone identification code.
        /// </summary>
        [JsonProperty(PropertyName = "timezonecode")]
        public int? Timezonecode { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who last modified the
        /// time zone record.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who created the time
        /// zone record.
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// Gets or sets time zone name for the standard time.
        /// </summary>
        [JsonProperty(PropertyName = "standardname")]
        public string Standardname { get; set; }

        /// <summary>
        /// Gets or sets order an entry for a time zone definition is retired.
        /// 0 for the latest entry.
        /// </summary>
        [JsonProperty(PropertyName = "retiredorder")]
        public int? Retiredorder { get; set; }

        /// <summary>
        /// Gets or sets date and time when the time zone record was modified.
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// Gets or sets time zone name for the daylight time.
        /// </summary>
        [JsonProperty(PropertyName = "daylightname")]
        public string Daylightname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "lk_timezonelocalizedname_timezonedefinitionid")]
        public IList<MicrosoftDynamicsCRMtimezonelocalizedname> LkTimezonelocalizednameTimezonedefinitionid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser Createdonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdby")]
        public MicrosoftDynamicsCRMsystemuser Createdby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "lk_timezonerule_timezonedefinitionid")]
        public IList<MicrosoftDynamicsCRMtimezonerule> LkTimezoneruleTimezonedefinitionid { get; set; }

    }
}
