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
    /// Microsoft.Dynamics.CRM.savedqueryvisualization
    /// </summary>
    public partial class MicrosoftDynamicsCRMsavedqueryvisualization
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMsavedqueryvisualization class.
        /// </summary>
        public MicrosoftDynamicsCRMsavedqueryvisualization()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMsavedqueryvisualization class.
        /// </summary>
        /// <param name="createdon">Date and time when the system chart was
        /// created.</param>
        /// <param name="primaryentitytypecode">Type of entity with which the
        /// system chart is attached.</param>
        /// <param name="description">Description of the system chart.</param>
        /// <param name="_webresourceidValue">Unique identifier of the Web
        /// resource that will be displayed in the system chart.</param>
        /// <param name="versionnumber">Version number of the system
        /// chart.</param>
        /// <param name="presentationdescription">XML string used to define the
        /// presentation properties of the system chart.</param>
        /// <param name="savedqueryvisualizationidunique">For internal use
        /// only.</param>
        /// <param name="solutionid">Unique identifier of the associated
        /// solution.</param>
        /// <param name="isdefault">Indicates whether the system chart is the
        /// default chart for the entity.</param>
        /// <param name="introducedversion">Version in which the form is
        /// introduced.</param>
        /// <param name="_modifiedonbehalfbyValue">Unique identifier of the
        /// delegate user who last modified the system chart.</param>
        /// <param name="componentstate">For internal use only.</param>
        /// <param name="iscustomizable">Information that specifies whether
        /// this component can be customized.</param>
        /// <param name="type">Specifies where the chart will be used, 0 for
        /// data centric as well as interaction centric and 1 for just
        /// interaction centric</param>
        /// <param name="charttype">Indicates the library used to render the
        /// visualization.</param>
        /// <param name="_organizationidValue">Unique identifier of the
        /// organization associated with the system chart.</param>
        /// <param name="name">Name of the system chart.</param>
        /// <param name="datadescription">XML string used to define the
        /// underlying data for the system chart.</param>
        /// <param name="ismanaged">Indicates whether the solution component is
        /// part of a managed solution.</param>
        /// <param name="_modifiedbyValue">Unique identifier of the user who
        /// last modified the system chart.</param>
        /// <param name="modifiedon">Date and time when the system chart was
        /// last modified.</param>
        /// <param name="_createdbyValue">Unique identifier of the user who
        /// created the system chart.</param>
        /// <param name="canbedeleted">Tells whether the saved query
        /// visualization can be deleted.</param>
        /// <param name="savedqueryvisualizationid">Unique identifier of the
        /// system chart.</param>
        /// <param name="_createdonbehalfbyValue">Unique identifier of the
        /// delegate user who created the system chart.</param>
        /// <param name="overwritetime">For internal use only.</param>
        public MicrosoftDynamicsCRMsavedqueryvisualization(System.DateTimeOffset? createdon = default(System.DateTimeOffset?), string primaryentitytypecode = default(string), string description = default(string), string _webresourceidValue = default(string), string versionnumber = default(string), string presentationdescription = default(string), string savedqueryvisualizationidunique = default(string), string solutionid = default(string), bool? isdefault = default(bool?), string introducedversion = default(string), string _modifiedonbehalfbyValue = default(string), int? componentstate = default(int?), string iscustomizable = default(string), int? type = default(int?), int? charttype = default(int?), string _organizationidValue = default(string), string name = default(string), string datadescription = default(string), bool? ismanaged = default(bool?), string _modifiedbyValue = default(string), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), string _createdbyValue = default(string), string canbedeleted = default(string), string savedqueryvisualizationid = default(string), string _createdonbehalfbyValue = default(string), System.DateTimeOffset? overwritetime = default(System.DateTimeOffset?), IList<MicrosoftDynamicsCRMsyncerror> savedQueryVisualizationSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), MicrosoftDynamicsCRMsystemuser createdonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMorganization organizationid = default(MicrosoftDynamicsCRMorganization), MicrosoftDynamicsCRMsystemuser createdby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMwebresource webresourceid = default(MicrosoftDynamicsCRMwebresource), MicrosoftDynamicsCRMsystemuser modifiedby = default(MicrosoftDynamicsCRMsystemuser))
        {
            Createdon = createdon;
            Primaryentitytypecode = primaryentitytypecode;
            Description = description;
            this._webresourceidValue = _webresourceidValue;
            Versionnumber = versionnumber;
            Presentationdescription = presentationdescription;
            Savedqueryvisualizationidunique = savedqueryvisualizationidunique;
            Solutionid = solutionid;
            Isdefault = isdefault;
            Introducedversion = introducedversion;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            Componentstate = componentstate;
            Iscustomizable = iscustomizable;
            Type = type;
            Charttype = charttype;
            this._organizationidValue = _organizationidValue;
            Name = name;
            Datadescription = datadescription;
            Ismanaged = ismanaged;
            this._modifiedbyValue = _modifiedbyValue;
            Modifiedon = modifiedon;
            this._createdbyValue = _createdbyValue;
            Canbedeleted = canbedeleted;
            Savedqueryvisualizationid = savedqueryvisualizationid;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            Overwritetime = overwritetime;
            SavedQueryVisualizationSyncErrors = savedQueryVisualizationSyncErrors;
            Createdonbehalfby = createdonbehalfby;
            Organizationid = organizationid;
            Createdby = createdby;
            Modifiedonbehalfby = modifiedonbehalfby;
            Webresourceid = webresourceid;
            Modifiedby = modifiedby;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets date and time when the system chart was created.
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// Gets or sets type of entity with which the system chart is
        /// attached.
        /// </summary>
        [JsonProperty(PropertyName = "primaryentitytypecode")]
        public string Primaryentitytypecode { get; set; }

        /// <summary>
        /// Gets or sets description of the system chart.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the Web resource that will be
        /// displayed in the system chart.
        /// </summary>
        [JsonProperty(PropertyName = "_webresourceid_value")]
        public string _webresourceidValue { get; set; }

        /// <summary>
        /// Gets or sets version number of the system chart.
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// Gets or sets XML string used to define the presentation properties
        /// of the system chart.
        /// </summary>
        [JsonProperty(PropertyName = "presentationdescription")]
        public string Presentationdescription { get; set; }

        /// <summary>
        /// Gets or sets for internal use only.
        /// </summary>
        [JsonProperty(PropertyName = "savedqueryvisualizationidunique")]
        public string Savedqueryvisualizationidunique { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the associated solution.
        /// </summary>
        [JsonProperty(PropertyName = "solutionid")]
        public string Solutionid { get; set; }

        /// <summary>
        /// Gets or sets indicates whether the system chart is the default
        /// chart for the entity.
        /// </summary>
        [JsonProperty(PropertyName = "isdefault")]
        public bool? Isdefault { get; set; }

        /// <summary>
        /// Gets or sets version in which the form is introduced.
        /// </summary>
        [JsonProperty(PropertyName = "introducedversion")]
        public string Introducedversion { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who last
        /// modified the system chart.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets for internal use only.
        /// </summary>
        [JsonProperty(PropertyName = "componentstate")]
        public int? Componentstate { get; set; }

        /// <summary>
        /// Gets or sets information that specifies whether this component can
        /// be customized.
        /// </summary>
        [JsonProperty(PropertyName = "iscustomizable")]
        public string Iscustomizable { get; set; }

        /// <summary>
        /// Gets or sets specifies where the chart will be used, 0 for data
        /// centric as well as interaction centric and 1 for just interaction
        /// centric
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public int? Type { get; set; }

        /// <summary>
        /// Gets or sets indicates the library used to render the
        /// visualization.
        /// </summary>
        [JsonProperty(PropertyName = "charttype")]
        public int? Charttype { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the organization associated with
        /// the system chart.
        /// </summary>
        [JsonProperty(PropertyName = "_organizationid_value")]
        public string _organizationidValue { get; set; }

        /// <summary>
        /// Gets or sets name of the system chart.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets XML string used to define the underlying data for the
        /// system chart.
        /// </summary>
        [JsonProperty(PropertyName = "datadescription")]
        public string Datadescription { get; set; }

        /// <summary>
        /// Gets or sets indicates whether the solution component is part of a
        /// managed solution.
        /// </summary>
        [JsonProperty(PropertyName = "ismanaged")]
        public bool? Ismanaged { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who last modified the
        /// system chart.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// Gets or sets date and time when the system chart was last modified.
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who created the system
        /// chart.
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// Gets or sets tells whether the saved query visualization can be
        /// deleted.
        /// </summary>
        [JsonProperty(PropertyName = "canbedeleted")]
        public string Canbedeleted { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the system chart.
        /// </summary>
        [JsonProperty(PropertyName = "savedqueryvisualizationid")]
        public string Savedqueryvisualizationid { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who created the
        /// system chart.
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets for internal use only.
        /// </summary>
        [JsonProperty(PropertyName = "overwritetime")]
        public System.DateTimeOffset? Overwritetime { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "SavedQueryVisualization_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> SavedQueryVisualizationSyncErrors { get; set; }

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
        [JsonProperty(PropertyName = "createdby")]
        public MicrosoftDynamicsCRMsystemuser Createdby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "webresourceid")]
        public MicrosoftDynamicsCRMwebresource Webresourceid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedby { get; set; }

    }
}
