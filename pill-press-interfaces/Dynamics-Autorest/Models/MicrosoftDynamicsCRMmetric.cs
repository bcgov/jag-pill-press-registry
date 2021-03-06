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
    /// Microsoft.Dynamics.CRM.metric
    /// </summary>
    public partial class MicrosoftDynamicsCRMmetric
    {
        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMmetric class.
        /// </summary>
        public MicrosoftDynamicsCRMmetric()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMmetric class.
        /// </summary>
        /// <param name="_createdonbehalfbyValue">Unique identifier of the
        /// delegate user who created the record.</param>
        /// <param name="timezoneruleversionnumber">For internal use
        /// only.</param>
        /// <param name="statuscode">Reason for the status of the goal
        /// metric.</param>
        /// <param name="isamount">Information that indicates whether the
        /// metric type is Count or Amount.</param>
        /// <param name="utcconversiontimezonecode">Time zone code that was in
        /// use when the record was created.</param>
        /// <param name="importsequencenumber">Sequence number of the import
        /// that created this record.</param>
        /// <param name="_organizationidValue">Unique identifier of the
        /// organization.</param>
        /// <param name="_modifiedbyValue">Unique identifier of the user who
        /// modified the record.</param>
        /// <param name="isstretchtracked">Indicates whether the goal metric
        /// tracks stretch targets.</param>
        /// <param name="amountdatatype">Data type of the amount.</param>
        /// <param name="name">Name of the goal metric.</param>
        /// <param name="versionnumber">Version number of the goal
        /// metric.</param>
        /// <param name="description">Description of the goal metric.</param>
        /// <param name="createdon">Date and time when the record was
        /// created.</param>
        /// <param name="overriddencreatedon">Date and time that the record was
        /// migrated.</param>
        /// <param name="_modifiedonbehalfbyValue">Unique identifier of the
        /// delegate user who modified the record.</param>
        /// <param name="metricid">Unique identifier of the goal
        /// metric.</param>
        /// <param name="statecode">Status of the goal metric.</param>
        /// <param name="modifiedon">Date and time when the record was
        /// modified.</param>
        /// <param name="_createdbyValue">Unique identifier of the user who
        /// created the record.</param>
        public MicrosoftDynamicsCRMmetric(string _createdonbehalfbyValue = default(string), int? timezoneruleversionnumber = default(int?), int? statuscode = default(int?), bool? isamount = default(bool?), int? utcconversiontimezonecode = default(int?), int? importsequencenumber = default(int?), string _organizationidValue = default(string), string _modifiedbyValue = default(string), bool? isstretchtracked = default(bool?), int? amountdatatype = default(int?), string name = default(string), string versionnumber = default(string), string description = default(string), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), string _modifiedonbehalfbyValue = default(string), string metricid = default(string), int? statecode = default(int?), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), string _createdbyValue = default(string), MicrosoftDynamicsCRMsystemuser createdby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMorganization organizationid = default(MicrosoftDynamicsCRMorganization), IList<MicrosoftDynamicsCRMgoal> metricGoal = default(IList<MicrosoftDynamicsCRMgoal>), IList<MicrosoftDynamicsCRMasyncoperation> metricAsyncOperations = default(IList<MicrosoftDynamicsCRMasyncoperation>), IList<MicrosoftDynamicsCRMsyncerror> metricSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>))
        {
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            Timezoneruleversionnumber = timezoneruleversionnumber;
            Statuscode = statuscode;
            Isamount = isamount;
            Utcconversiontimezonecode = utcconversiontimezonecode;
            Importsequencenumber = importsequencenumber;
            this._organizationidValue = _organizationidValue;
            this._modifiedbyValue = _modifiedbyValue;
            Isstretchtracked = isstretchtracked;
            Amountdatatype = amountdatatype;
            Name = name;
            Versionnumber = versionnumber;
            Description = description;
            Createdon = createdon;
            Overriddencreatedon = overriddencreatedon;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            Metricid = metricid;
            Statecode = statecode;
            Modifiedon = modifiedon;
            this._createdbyValue = _createdbyValue;
            Createdby = createdby;
            Createdonbehalfby = createdonbehalfby;
            Modifiedby = modifiedby;
            Modifiedonbehalfby = modifiedonbehalfby;
            Organizationid = organizationid;
            MetricGoal = metricGoal;
            MetricAsyncOperations = metricAsyncOperations;
            MetricSyncErrors = metricSyncErrors;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who created the
        /// record.
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets for internal use only.
        /// </summary>
        [JsonProperty(PropertyName = "timezoneruleversionnumber")]
        public int? Timezoneruleversionnumber { get; set; }

        /// <summary>
        /// Gets or sets reason for the status of the goal metric.
        /// </summary>
        [JsonProperty(PropertyName = "statuscode")]
        public int? Statuscode { get; set; }

        /// <summary>
        /// Gets or sets information that indicates whether the metric type is
        /// Count or Amount.
        /// </summary>
        [JsonProperty(PropertyName = "isamount")]
        public bool? Isamount { get; set; }

        /// <summary>
        /// Gets or sets time zone code that was in use when the record was
        /// created.
        /// </summary>
        [JsonProperty(PropertyName = "utcconversiontimezonecode")]
        public int? Utcconversiontimezonecode { get; set; }

        /// <summary>
        /// Gets or sets sequence number of the import that created this
        /// record.
        /// </summary>
        [JsonProperty(PropertyName = "importsequencenumber")]
        public int? Importsequencenumber { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the organization.
        /// </summary>
        [JsonProperty(PropertyName = "_organizationid_value")]
        public string _organizationidValue { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who modified the record.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// Gets or sets indicates whether the goal metric tracks stretch
        /// targets.
        /// </summary>
        [JsonProperty(PropertyName = "isstretchtracked")]
        public bool? Isstretchtracked { get; set; }

        /// <summary>
        /// Gets or sets data type of the amount.
        /// </summary>
        [JsonProperty(PropertyName = "amountdatatype")]
        public int? Amountdatatype { get; set; }

        /// <summary>
        /// Gets or sets name of the goal metric.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets version number of the goal metric.
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// Gets or sets description of the goal metric.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets date and time when the record was created.
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// Gets or sets date and time that the record was migrated.
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who modified
        /// the record.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the goal metric.
        /// </summary>
        [JsonProperty(PropertyName = "metricid")]
        public string Metricid { get; set; }

        /// <summary>
        /// Gets or sets status of the goal metric.
        /// </summary>
        [JsonProperty(PropertyName = "statecode")]
        public int? Statecode { get; set; }

        /// <summary>
        /// Gets or sets date and time when the record was modified.
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who created the record.
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

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
        [JsonProperty(PropertyName = "modifiedby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "organizationid")]
        public MicrosoftDynamicsCRMorganization Organizationid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "metric_goal")]
        public IList<MicrosoftDynamicsCRMgoal> MetricGoal { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "metric_AsyncOperations")]
        public IList<MicrosoftDynamicsCRMasyncoperation> MetricAsyncOperations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Metric_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> MetricSyncErrors { get; set; }

    }
}
