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
    /// Microsoft.Dynamics.CRM.entitlementtemplate
    /// </summary>
    public partial class MicrosoftDynamicsCRMentitlementtemplate
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMentitlementtemplate class.
        /// </summary>
        public MicrosoftDynamicsCRMentitlementtemplate()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMentitlementtemplate class.
        /// </summary>
        /// <param name="restrictcasecreation">Tells whether case creation is
        /// restricted based on entitlement terms.</param>
        /// <param name="entitlementtemplateid">Unique identifier for entity
        /// instances</param>
        /// <param name="exchangerate">Exchange rate for the currency
        /// associated with the contact with respect to the base
        /// currency.</param>
        /// <param name="modifiedon">Date and time when the record was
        /// modified.</param>
        /// <param name="_transactioncurrencyidValue">Unique identifier of the
        /// currency associated with the contact.</param>
        /// <param name="importsequencenumber">Unique identifier of the data
        /// import or data migration that created this record.</param>
        /// <param name="_createdbyValue">Unique identifier of the user who
        /// created the record.</param>
        /// <param name="_organizationidValue">Unique identifier of the
        /// organization associated with the entitlement template.</param>
        /// <param name="startdate">Enter the date and time when the
        /// entitlement begins.</param>
        /// <param name="decreaseremainingon">Information about whether to
        /// decrease the remaining terms when the case is created or when it is
        /// resolved</param>
        /// <param name="enddate">Enter the date and time when the entitlement
        /// ends.</param>
        /// <param name="_createdonbehalfbyValue">Unique identifier of the
        /// delegate user who created the record.</param>
        /// <param name="allocationtypecode">Select whether the entitlement
        /// allocation is based on number of cases or number of hours.</param>
        /// <param name="_modifiedbyValue">Unique identifier of the user who
        /// modified the record.</param>
        /// <param name="utcconversiontimezonecode">Time zone code that was in
        /// use when the record was created.</param>
        /// <param name="_slaidValue">Choose the service level agreement (SLA)
        /// associated with the entitlement.</param>
        /// <param name="versionnumber">Version number of the entitlement
        /// template item.</param>
        /// <param name="timezoneruleversionnumber">For internal use
        /// only.</param>
        /// <param name="description">Type additional information to describe
        /// the account, such as an excerpt from the company's website.</param>
        /// <param name="_modifiedonbehalfbyValue">Unique identifier of the
        /// delegate user who modified the record.</param>
        /// <param name="createdon">Date and time when the entitlement was
        /// created.</param>
        /// <param name="overriddencreatedon">Date and time that the record was
        /// migrated.</param>
        /// <param name="totalterms">Type the total number of entitlement
        /// terms.</param>
        /// <param name="kbaccesslevel">Select the access someone will have to
        /// the knowledge base on the portal.</param>
        /// <param name="name">Type a descriptive name for the entitlement
        /// template.</param>
        public MicrosoftDynamicsCRMentitlementtemplate(bool? restrictcasecreation = default(bool?), string entitlementtemplateid = default(string), decimal? exchangerate = default(decimal?), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), string _transactioncurrencyidValue = default(string), int? importsequencenumber = default(int?), string _createdbyValue = default(string), string _organizationidValue = default(string), System.DateTimeOffset? startdate = default(System.DateTimeOffset?), int? decreaseremainingon = default(int?), System.DateTimeOffset? enddate = default(System.DateTimeOffset?), string _createdonbehalfbyValue = default(string), int? allocationtypecode = default(int?), string _modifiedbyValue = default(string), int? utcconversiontimezonecode = default(int?), string _slaidValue = default(string), string versionnumber = default(string), int? timezoneruleversionnumber = default(int?), string description = default(string), string _modifiedonbehalfbyValue = default(string), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), decimal? totalterms = default(decimal?), int? kbaccesslevel = default(int?), string name = default(string), MicrosoftDynamicsCRMsystemuser createdby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedonbehalfby = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMactivitypointer> entitlementtemplateActivityPointers = default(IList<MicrosoftDynamicsCRMactivitypointer>), IList<MicrosoftDynamicsCRMappointment> entitlementtemplateAppointments = default(IList<MicrosoftDynamicsCRMappointment>), IList<MicrosoftDynamicsCRMemail> entitlementtemplateEmails = default(IList<MicrosoftDynamicsCRMemail>), IList<MicrosoftDynamicsCRMfax> entitlementtemplateFaxes = default(IList<MicrosoftDynamicsCRMfax>), IList<MicrosoftDynamicsCRMletter> entitlementtemplateLetters = default(IList<MicrosoftDynamicsCRMletter>), IList<MicrosoftDynamicsCRMphonecall> entitlementtemplatePhoneCalls = default(IList<MicrosoftDynamicsCRMphonecall>), IList<MicrosoftDynamicsCRMserviceappointment> entitlementtemplateServiceAppointments = default(IList<MicrosoftDynamicsCRMserviceappointment>), IList<MicrosoftDynamicsCRMtask> entitlementtemplateTasks = default(IList<MicrosoftDynamicsCRMtask>), IList<MicrosoftDynamicsCRMrecurringappointmentmaster> entitlementtemplateRecurringAppointmentMasters = default(IList<MicrosoftDynamicsCRMrecurringappointmentmaster>), IList<MicrosoftDynamicsCRMduplicaterecord> entitlementtemplateDuplicateMatchingRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), IList<MicrosoftDynamicsCRMduplicaterecord> entitlementtemplateDuplicateBaseRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), IList<MicrosoftDynamicsCRMannotation> entitlementtemplateAnnotations = default(IList<MicrosoftDynamicsCRMannotation>), IList<MicrosoftDynamicsCRMasyncoperation> entitlementtemplateAsyncOperations = default(IList<MicrosoftDynamicsCRMasyncoperation>), IList<MicrosoftDynamicsCRMbulkdeletefailure> entitlementtemplateBulkDeleteFailures = default(IList<MicrosoftDynamicsCRMbulkdeletefailure>), IList<MicrosoftDynamicsCRMentitlement> entitlementtemplateidRelationShip = default(IList<MicrosoftDynamicsCRMentitlement>), IList<MicrosoftDynamicsCRMsyncerror> entitlementTemplateSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), IList<MicrosoftDynamicsCRMproduct> productEntitlementtemplateAssociation = default(IList<MicrosoftDynamicsCRMproduct>), MicrosoftDynamicsCRMtransactioncurrency transactioncurrencyid = default(MicrosoftDynamicsCRMtransactioncurrency), MicrosoftDynamicsCRMorganization organizationid = default(MicrosoftDynamicsCRMorganization), MicrosoftDynamicsCRMsla slaid = default(MicrosoftDynamicsCRMsla), IList<MicrosoftDynamicsCRMsocialactivity> entitlementtemplateSocialActivities = default(IList<MicrosoftDynamicsCRMsocialactivity>), IList<MicrosoftDynamicsCRMentitlementtemplatechannel> entitlementtemplateEntitlementchannelEntitlementtemplateid = default(IList<MicrosoftDynamicsCRMentitlementtemplatechannel>), IList<MicrosoftDynamicsCRMabsScheduledprocessexecution> entitlementtemplateAbsScheduledprocessexecutions = default(IList<MicrosoftDynamicsCRMabsScheduledprocessexecution>))
        {
            Restrictcasecreation = restrictcasecreation;
            Entitlementtemplateid = entitlementtemplateid;
            Exchangerate = exchangerate;
            Modifiedon = modifiedon;
            this._transactioncurrencyidValue = _transactioncurrencyidValue;
            Importsequencenumber = importsequencenumber;
            this._createdbyValue = _createdbyValue;
            this._organizationidValue = _organizationidValue;
            Startdate = startdate;
            Decreaseremainingon = decreaseremainingon;
            Enddate = enddate;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            Allocationtypecode = allocationtypecode;
            this._modifiedbyValue = _modifiedbyValue;
            Utcconversiontimezonecode = utcconversiontimezonecode;
            this._slaidValue = _slaidValue;
            Versionnumber = versionnumber;
            Timezoneruleversionnumber = timezoneruleversionnumber;
            Description = description;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            Createdon = createdon;
            Overriddencreatedon = overriddencreatedon;
            Totalterms = totalterms;
            Kbaccesslevel = kbaccesslevel;
            Name = name;
            Createdby = createdby;
            Createdonbehalfby = createdonbehalfby;
            Modifiedby = modifiedby;
            Modifiedonbehalfby = modifiedonbehalfby;
            EntitlementtemplateActivityPointers = entitlementtemplateActivityPointers;
            EntitlementtemplateAppointments = entitlementtemplateAppointments;
            EntitlementtemplateEmails = entitlementtemplateEmails;
            EntitlementtemplateFaxes = entitlementtemplateFaxes;
            EntitlementtemplateLetters = entitlementtemplateLetters;
            EntitlementtemplatePhoneCalls = entitlementtemplatePhoneCalls;
            EntitlementtemplateServiceAppointments = entitlementtemplateServiceAppointments;
            EntitlementtemplateTasks = entitlementtemplateTasks;
            EntitlementtemplateRecurringAppointmentMasters = entitlementtemplateRecurringAppointmentMasters;
            EntitlementtemplateDuplicateMatchingRecord = entitlementtemplateDuplicateMatchingRecord;
            EntitlementtemplateDuplicateBaseRecord = entitlementtemplateDuplicateBaseRecord;
            EntitlementtemplateAnnotations = entitlementtemplateAnnotations;
            EntitlementtemplateAsyncOperations = entitlementtemplateAsyncOperations;
            EntitlementtemplateBulkDeleteFailures = entitlementtemplateBulkDeleteFailures;
            EntitlementtemplateidRelationShip = entitlementtemplateidRelationShip;
            EntitlementTemplateSyncErrors = entitlementTemplateSyncErrors;
            ProductEntitlementtemplateAssociation = productEntitlementtemplateAssociation;
            Transactioncurrencyid = transactioncurrencyid;
            Organizationid = organizationid;
            Slaid = slaid;
            EntitlementtemplateSocialActivities = entitlementtemplateSocialActivities;
            EntitlementtemplateEntitlementchannelEntitlementtemplateid = entitlementtemplateEntitlementchannelEntitlementtemplateid;
            EntitlementtemplateAbsScheduledprocessexecutions = entitlementtemplateAbsScheduledprocessexecutions;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets tells whether case creation is restricted based on
        /// entitlement terms.
        /// </summary>
        [JsonProperty(PropertyName = "restrictcasecreation")]
        public bool? Restrictcasecreation { get; set; }

        /// <summary>
        /// Gets or sets unique identifier for entity instances
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplateid")]
        public string Entitlementtemplateid { get; set; }

        /// <summary>
        /// Gets or sets exchange rate for the currency associated with the
        /// contact with respect to the base currency.
        /// </summary>
        [JsonProperty(PropertyName = "exchangerate")]
        public decimal? Exchangerate { get; set; }

        /// <summary>
        /// Gets or sets date and time when the record was modified.
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the currency associated with the
        /// contact.
        /// </summary>
        [JsonProperty(PropertyName = "_transactioncurrencyid_value")]
        public string _transactioncurrencyidValue { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the data import or data migration
        /// that created this record.
        /// </summary>
        [JsonProperty(PropertyName = "importsequencenumber")]
        public int? Importsequencenumber { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who created the record.
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the organization associated with
        /// the entitlement template.
        /// </summary>
        [JsonProperty(PropertyName = "_organizationid_value")]
        public string _organizationidValue { get; set; }

        /// <summary>
        /// Gets or sets enter the date and time when the entitlement begins.
        /// </summary>
        [JsonProperty(PropertyName = "startdate")]
        public System.DateTimeOffset? Startdate { get; set; }

        /// <summary>
        /// Gets or sets information about whether to decrease the remaining
        /// terms when the case is created or when it is resolved
        /// </summary>
        [JsonProperty(PropertyName = "decreaseremainingon")]
        public int? Decreaseremainingon { get; set; }

        /// <summary>
        /// Gets or sets enter the date and time when the entitlement ends.
        /// </summary>
        [JsonProperty(PropertyName = "enddate")]
        public System.DateTimeOffset? Enddate { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who created the
        /// record.
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets select whether the entitlement allocation is based on
        /// number of cases or number of hours.
        /// </summary>
        [JsonProperty(PropertyName = "allocationtypecode")]
        public int? Allocationtypecode { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who modified the record.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// Gets or sets time zone code that was in use when the record was
        /// created.
        /// </summary>
        [JsonProperty(PropertyName = "utcconversiontimezonecode")]
        public int? Utcconversiontimezonecode { get; set; }

        /// <summary>
        /// Gets or sets choose the service level agreement (SLA) associated
        /// with the entitlement.
        /// </summary>
        [JsonProperty(PropertyName = "_slaid_value")]
        public string _slaidValue { get; set; }

        /// <summary>
        /// Gets or sets version number of the entitlement template item.
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// Gets or sets for internal use only.
        /// </summary>
        [JsonProperty(PropertyName = "timezoneruleversionnumber")]
        public int? Timezoneruleversionnumber { get; set; }

        /// <summary>
        /// Gets or sets type additional information to describe the account,
        /// such as an excerpt from the company's website.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who modified
        /// the record.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets date and time when the entitlement was created.
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// Gets or sets date and time that the record was migrated.
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// Gets or sets type the total number of entitlement terms.
        /// </summary>
        [JsonProperty(PropertyName = "totalterms")]
        public decimal? Totalterms { get; set; }

        /// <summary>
        /// Gets or sets select the access someone will have to the knowledge
        /// base on the portal.
        /// </summary>
        [JsonProperty(PropertyName = "kbaccesslevel")]
        public int? Kbaccesslevel { get; set; }

        /// <summary>
        /// Gets or sets type a descriptive name for the entitlement template.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

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
        [JsonProperty(PropertyName = "entitlementtemplate_ActivityPointers")]
        public IList<MicrosoftDynamicsCRMactivitypointer> EntitlementtemplateActivityPointers { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_Appointments")]
        public IList<MicrosoftDynamicsCRMappointment> EntitlementtemplateAppointments { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_Emails")]
        public IList<MicrosoftDynamicsCRMemail> EntitlementtemplateEmails { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_Faxes")]
        public IList<MicrosoftDynamicsCRMfax> EntitlementtemplateFaxes { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_Letters")]
        public IList<MicrosoftDynamicsCRMletter> EntitlementtemplateLetters { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_PhoneCalls")]
        public IList<MicrosoftDynamicsCRMphonecall> EntitlementtemplatePhoneCalls { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_ServiceAppointments")]
        public IList<MicrosoftDynamicsCRMserviceappointment> EntitlementtemplateServiceAppointments { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_Tasks")]
        public IList<MicrosoftDynamicsCRMtask> EntitlementtemplateTasks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_RecurringAppointmentMasters")]
        public IList<MicrosoftDynamicsCRMrecurringappointmentmaster> EntitlementtemplateRecurringAppointmentMasters { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_DuplicateMatchingRecord")]
        public IList<MicrosoftDynamicsCRMduplicaterecord> EntitlementtemplateDuplicateMatchingRecord { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_DuplicateBaseRecord")]
        public IList<MicrosoftDynamicsCRMduplicaterecord> EntitlementtemplateDuplicateBaseRecord { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_Annotations")]
        public IList<MicrosoftDynamicsCRMannotation> EntitlementtemplateAnnotations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_AsyncOperations")]
        public IList<MicrosoftDynamicsCRMasyncoperation> EntitlementtemplateAsyncOperations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_BulkDeleteFailures")]
        public IList<MicrosoftDynamicsCRMbulkdeletefailure> EntitlementtemplateBulkDeleteFailures { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplateid_RelationShip")]
        public IList<MicrosoftDynamicsCRMentitlement> EntitlementtemplateidRelationShip { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "EntitlementTemplate_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> EntitlementTemplateSyncErrors { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "product_entitlementtemplate_association")]
        public IList<MicrosoftDynamicsCRMproduct> ProductEntitlementtemplateAssociation { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "transactioncurrencyid")]
        public MicrosoftDynamicsCRMtransactioncurrency Transactioncurrencyid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "organizationid")]
        public MicrosoftDynamicsCRMorganization Organizationid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "slaid")]
        public MicrosoftDynamicsCRMsla Slaid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_SocialActivities")]
        public IList<MicrosoftDynamicsCRMsocialactivity> EntitlementtemplateSocialActivities { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_entitlementchannel_entitlementtemplateid")]
        public IList<MicrosoftDynamicsCRMentitlementtemplatechannel> EntitlementtemplateEntitlementchannelEntitlementtemplateid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entitlementtemplate_abs_scheduledprocessexecutions")]
        public IList<MicrosoftDynamicsCRMabsScheduledprocessexecution> EntitlementtemplateAbsScheduledprocessexecutions { get; set; }

    }
}
