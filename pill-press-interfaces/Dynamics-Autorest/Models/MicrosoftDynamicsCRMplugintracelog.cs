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
    /// Microsoft.Dynamics.CRM.plugintracelog
    /// </summary>
    public partial class MicrosoftDynamicsCRMplugintracelog
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMplugintracelog class.
        /// </summary>
        public MicrosoftDynamicsCRMplugintracelog()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMplugintracelog class.
        /// </summary>
        /// <param name="performanceconstructorduration">Time, in milliseconds,
        /// to construct.</param>
        /// <param name="operationtype">Type of custom code.</param>
        /// <param name="primaryentity">Entity, if any, that the plug-in is
        /// executed against.</param>
        /// <param name="secureconfiguration">Secured configuration for the
        /// plug-in trace log.</param>
        /// <param name="configuration">Unsecured configuration for the plug-in
        /// trace log.</param>
        /// <param name="exceptiondetails">Details of the exception.</param>
        /// <param name="plugintracelogid">Unique identifier for an entity
        /// instance.</param>
        /// <param name="pluginstepid">ID of the plug-in registration
        /// step.</param>
        /// <param name="mode">Type of execution.</param>
        /// <param name="messageblock">Trace text from the plug-in.</param>
        /// <param name="_createdbyValue">Unique identifier of the delegate
        /// user who created the record.</param>
        /// <param name="typename">Class name of the plug-in.</param>
        /// <param name="createdon">Date and time when the record was
        /// created.</param>
        /// <param name="_createdonbehalfbyValue">Unique identifier of the
        /// delegate user who created the record.</param>
        /// <param name="profile">Plug-in profile formatted as serialized
        /// text.</param>
        /// <param name="organizationid">Unique identifier for the
        /// organization.</param>
        /// <param name="performanceconstructorstarttime">Date and time when
        /// constructed.</param>
        /// <param name="performanceexecutionstarttime">Time, in milliseconds,
        /// to execute the request.</param>
        /// <param name="depth">Depth of execution of the plug-in or custom
        /// workflow activity.</param>
        /// <param name="issystemcreated">Where the event originated. Set to
        /// true if it's a system trace; otherwise, false.</param>
        /// <param name="requestid">Unique identifier of the message
        /// request.</param>
        /// <param name="correlationid">Unique identifier for tracking plug-in
        /// or custom workflow activity execution.</param>
        /// <param name="persistencekey">Asynchronous workflow persistence
        /// key.</param>
        /// <param name="performanceexecutionduration">Time, in milliseconds,
        /// to execute the request.</param>
        /// <param name="messagename">Name of the message that triggered this
        /// plug-in.</param>
        public MicrosoftDynamicsCRMplugintracelog(int? performanceconstructorduration = default(int?), int? operationtype = default(int?), string primaryentity = default(string), string secureconfiguration = default(string), string configuration = default(string), string exceptiondetails = default(string), string plugintracelogid = default(string), string pluginstepid = default(string), int? mode = default(int?), string messageblock = default(string), string _createdbyValue = default(string), string typename = default(string), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), string _createdonbehalfbyValue = default(string), string profile = default(string), string organizationid = default(string), System.DateTimeOffset? performanceconstructorstarttime = default(System.DateTimeOffset?), System.DateTimeOffset? performanceexecutionstarttime = default(System.DateTimeOffset?), int? depth = default(int?), bool? issystemcreated = default(bool?), string requestid = default(string), string correlationid = default(string), string persistencekey = default(string), int? performanceexecutionduration = default(int?), string messagename = default(string), MicrosoftDynamicsCRMsystemuser createdonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdby = default(MicrosoftDynamicsCRMsystemuser))
        {
            Performanceconstructorduration = performanceconstructorduration;
            Operationtype = operationtype;
            Primaryentity = primaryentity;
            Secureconfiguration = secureconfiguration;
            Configuration = configuration;
            Exceptiondetails = exceptiondetails;
            Plugintracelogid = plugintracelogid;
            Pluginstepid = pluginstepid;
            Mode = mode;
            Messageblock = messageblock;
            this._createdbyValue = _createdbyValue;
            Typename = typename;
            Createdon = createdon;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            Profile = profile;
            Organizationid = organizationid;
            Performanceconstructorstarttime = performanceconstructorstarttime;
            Performanceexecutionstarttime = performanceexecutionstarttime;
            Depth = depth;
            Issystemcreated = issystemcreated;
            Requestid = requestid;
            Correlationid = correlationid;
            Persistencekey = persistencekey;
            Performanceexecutionduration = performanceexecutionduration;
            Messagename = messagename;
            Createdonbehalfby = createdonbehalfby;
            Createdby = createdby;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets time, in milliseconds, to construct.
        /// </summary>
        [JsonProperty(PropertyName = "performanceconstructorduration")]
        public int? Performanceconstructorduration { get; set; }

        /// <summary>
        /// Gets or sets type of custom code.
        /// </summary>
        [JsonProperty(PropertyName = "operationtype")]
        public int? Operationtype { get; set; }

        /// <summary>
        /// Gets or sets entity, if any, that the plug-in is executed against.
        /// </summary>
        [JsonProperty(PropertyName = "primaryentity")]
        public string Primaryentity { get; set; }

        /// <summary>
        /// Gets or sets secured configuration for the plug-in trace log.
        /// </summary>
        [JsonProperty(PropertyName = "secureconfiguration")]
        public string Secureconfiguration { get; set; }

        /// <summary>
        /// Gets or sets unsecured configuration for the plug-in trace log.
        /// </summary>
        [JsonProperty(PropertyName = "configuration")]
        public string Configuration { get; set; }

        /// <summary>
        /// Gets or sets details of the exception.
        /// </summary>
        [JsonProperty(PropertyName = "exceptiondetails")]
        public string Exceptiondetails { get; set; }

        /// <summary>
        /// Gets or sets unique identifier for an entity instance.
        /// </summary>
        [JsonProperty(PropertyName = "plugintracelogid")]
        public string Plugintracelogid { get; set; }

        /// <summary>
        /// Gets or sets ID of the plug-in registration step.
        /// </summary>
        [JsonProperty(PropertyName = "pluginstepid")]
        public string Pluginstepid { get; set; }

        /// <summary>
        /// Gets or sets type of execution.
        /// </summary>
        [JsonProperty(PropertyName = "mode")]
        public int? Mode { get; set; }

        /// <summary>
        /// Gets or sets trace text from the plug-in.
        /// </summary>
        [JsonProperty(PropertyName = "messageblock")]
        public string Messageblock { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who created the
        /// record.
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// Gets or sets class name of the plug-in.
        /// </summary>
        [JsonProperty(PropertyName = "typename")]
        public string Typename { get; set; }

        /// <summary>
        /// Gets or sets date and time when the record was created.
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who created the
        /// record.
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets plug-in profile formatted as serialized text.
        /// </summary>
        [JsonProperty(PropertyName = "profile")]
        public string Profile { get; set; }

        /// <summary>
        /// Gets or sets unique identifier for the organization.
        /// </summary>
        [JsonProperty(PropertyName = "organizationid")]
        public string Organizationid { get; set; }

        /// <summary>
        /// Gets or sets date and time when constructed.
        /// </summary>
        [JsonProperty(PropertyName = "performanceconstructorstarttime")]
        public System.DateTimeOffset? Performanceconstructorstarttime { get; set; }

        /// <summary>
        /// Gets or sets time, in milliseconds, to execute the request.
        /// </summary>
        [JsonProperty(PropertyName = "performanceexecutionstarttime")]
        public System.DateTimeOffset? Performanceexecutionstarttime { get; set; }

        /// <summary>
        /// Gets or sets depth of execution of the plug-in or custom workflow
        /// activity.
        /// </summary>
        [JsonProperty(PropertyName = "depth")]
        public int? Depth { get; set; }

        /// <summary>
        /// Gets or sets where the event originated. Set to true if it's a
        /// system trace; otherwise, false.
        /// </summary>
        [JsonProperty(PropertyName = "issystemcreated")]
        public bool? Issystemcreated { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the message request.
        /// </summary>
        [JsonProperty(PropertyName = "requestid")]
        public string Requestid { get; set; }

        /// <summary>
        /// Gets or sets unique identifier for tracking plug-in or custom
        /// workflow activity execution.
        /// </summary>
        [JsonProperty(PropertyName = "correlationid")]
        public string Correlationid { get; set; }

        /// <summary>
        /// Gets or sets asynchronous workflow persistence key.
        /// </summary>
        [JsonProperty(PropertyName = "persistencekey")]
        public string Persistencekey { get; set; }

        /// <summary>
        /// Gets or sets time, in milliseconds, to execute the request.
        /// </summary>
        [JsonProperty(PropertyName = "performanceexecutionduration")]
        public int? Performanceexecutionduration { get; set; }

        /// <summary>
        /// Gets or sets name of the message that triggered this plug-in.
        /// </summary>
        [JsonProperty(PropertyName = "messagename")]
        public string Messagename { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser Createdonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdby")]
        public MicrosoftDynamicsCRMsystemuser Createdby { get; set; }

    }
}
