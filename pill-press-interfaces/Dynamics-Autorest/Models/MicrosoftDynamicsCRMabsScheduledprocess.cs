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
    /// Microsoft.Dynamics.CRM.abs_scheduledprocess
    /// </summary>
    public partial class MicrosoftDynamicsCRMabsScheduledprocess
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMabsScheduledprocess class.
        /// </summary>
        public MicrosoftDynamicsCRMabsScheduledprocess()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMabsScheduledprocess class.
        /// </summary>
        /// <param name="absMonday">The process should execute on
        /// Monday</param>
        /// <param name="overriddencreatedon">Date and time that the record was
        /// migrated.</param>
        /// <param name="_owningbusinessunitValue">Unique identifier for the
        /// business unit that owns the record</param>
        /// <param name="absSeptember">The process should execute in the month
        /// of September</param>
        /// <param name="absScheduledprocessid">Unique identifier for entity
        /// instances</param>
        /// <param name="_owninguserValue">Unique identifier for the user that
        /// owns the record.</param>
        /// <param name="absRecurrencepattern">The frequency on which the
        /// process is executed.</param>
        /// <param name="importsequencenumber">Sequence number of the import
        /// that created this record.</param>
        /// <param name="absThursday">The process should execute on
        /// Thursday</param>
        /// <param name="versionnumber">Version Number</param>
        /// <param name="absJanuary">The process should execute in the month of
        /// January</param>
        /// <param name="_modifiedonbehalfbyValue">Unique identifier of the
        /// delegate user who modified the record.</param>
        /// <param name="absSunday">The process should execute on
        /// Sunday</param>
        /// <param name="absWednesday">The process should execute on
        /// Wednesday</param>
        /// <param name="absOctober">The process should execute in the month of
        /// October</param>
        /// <param name="absMay">The process should execute in the month of
        /// May</param>
        /// <param name="absName">The name of the custom entity.</param>
        /// <param name="absMarch">The process should execute in the month of
        /// March</param>
        /// <param name="absTuesday">The process should execute on
        /// Tuesday</param>
        /// <param name="absAmpm">Whether to execute the process before or
        /// after noon.</param>
        /// <param name="absFetchxmlquery">The FetchXML Query to retrieve the
        /// target records for the execution.</param>
        /// <param name="absFebruary">The process should execute in the month
        /// of February</param>
        /// <param name="_modifiedbyValue">Unique identifier of the user who
        /// modified the record.</param>
        /// <param name="modifiedon">Date and time when the record was
        /// modified.</param>
        /// <param name="createdon">Date and time when the record was
        /// created.</param>
        /// <param name="absJuly">The process should execute in the month of
        /// July</param>
        /// <param name="absNovember">The process should execute in the month
        /// of November</param>
        /// <param name="absSaturday">The process should execute on
        /// Saturday</param>
        /// <param name="absHour">The hour of the day at which to execute the
        /// scheduled process.</param>
        /// <param name="absInterval">The process should execute every number
        /// of intervals. (Eg. Every 2 weeks, every 4 months, etc).</param>
        /// <param name="absFriday">The process should execute on
        /// Friday</param>
        /// <param name="absMinute">Specifies the minute of the hour in which
        /// to execute the scheduled process.</param>
        /// <param name="timezoneruleversionnumber">For internal use
        /// only.</param>
        /// <param name="absDayofmonth">Execute the process on the nth day of
        /// the month</param>
        /// <param name="absNextactivation">Date and Time of the Next
        /// Execution</param>
        /// <param name="absDecember">The process should execute in the month
        /// of December</param>
        /// <param name="_absProcessidValue">The target process to
        /// execute.</param>
        /// <param name="statuscode">Reason for the status of the Scheduled
        /// Process</param>
        /// <param name="statecode">Status of the Scheduled Process</param>
        /// <param name="_createdbyValue">Unique identifier of the user who
        /// created the record.</param>
        /// <param name="_createdonbehalfbyValue">Unique identifier of the
        /// delegate user who created the record.</param>
        /// <param name="absSuspendonfailure">A Flag which sets whether or not
        /// the process should suspend subsequent executions when there is a
        /// processing failure.</param>
        /// <param name="absJune">The process should execute in the month of
        /// June</param>
        /// <param name="_owneridValue">Owner Id</param>
        /// <param name="_owningteamValue">Unique identifier for the team that
        /// owns the record.</param>
        /// <param name="utcconversiontimezonecode">Time zone code that was in
        /// use when the record was created.</param>
        /// <param name="absAugust">The process should execute in the month of
        /// August</param>
        /// <param name="absApril">The process should execute in the month of
        /// April</param>
        public MicrosoftDynamicsCRMabsScheduledprocess(bool? absMonday = default(bool?), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), string _owningbusinessunitValue = default(string), bool? absSeptember = default(bool?), string absScheduledprocessid = default(string), string _owninguserValue = default(string), int? absRecurrencepattern = default(int?), int? importsequencenumber = default(int?), bool? absThursday = default(bool?), string versionnumber = default(string), bool? absJanuary = default(bool?), string _modifiedonbehalfbyValue = default(string), bool? absSunday = default(bool?), bool? absWednesday = default(bool?), bool? absOctober = default(bool?), bool? absMay = default(bool?), string absName = default(string), bool? absMarch = default(bool?), bool? absTuesday = default(bool?), bool? absAmpm = default(bool?), string absFetchxmlquery = default(string), bool? absFebruary = default(bool?), string _modifiedbyValue = default(string), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), bool? absJuly = default(bool?), bool? absNovember = default(bool?), bool? absSaturday = default(bool?), int? absHour = default(int?), int? absInterval = default(int?), bool? absFriday = default(bool?), int? absMinute = default(int?), int? timezoneruleversionnumber = default(int?), int? absDayofmonth = default(int?), System.DateTimeOffset? absNextactivation = default(System.DateTimeOffset?), bool? absDecember = default(bool?), string _absProcessidValue = default(string), int? statuscode = default(int?), int? statecode = default(int?), string _createdbyValue = default(string), string _createdonbehalfbyValue = default(string), bool? absSuspendonfailure = default(bool?), bool? absJune = default(bool?), string _owneridValue = default(string), string _owningteamValue = default(string), int? utcconversiontimezonecode = default(int?), bool? absAugust = default(bool?), bool? absApril = default(bool?), MicrosoftDynamicsCRMsystemuser createdbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdonbehalfbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedonbehalfbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser owninguser = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMteam owningteam = default(MicrosoftDynamicsCRMteam), MicrosoftDynamicsCRMprincipal ownerid = default(MicrosoftDynamicsCRMprincipal), MicrosoftDynamicsCRMbusinessunit owningbusinessunit = default(MicrosoftDynamicsCRMbusinessunit), IList<MicrosoftDynamicsCRMactivitypointer> absScheduledprocessActivityPointers = default(IList<MicrosoftDynamicsCRMactivitypointer>), IList<MicrosoftDynamicsCRMappointment> absScheduledprocessAppointments = default(IList<MicrosoftDynamicsCRMappointment>), IList<MicrosoftDynamicsCRMemail> absScheduledprocessEmails = default(IList<MicrosoftDynamicsCRMemail>), IList<MicrosoftDynamicsCRMfax> absScheduledprocessFaxes = default(IList<MicrosoftDynamicsCRMfax>), IList<MicrosoftDynamicsCRMletter> absScheduledprocessLetters = default(IList<MicrosoftDynamicsCRMletter>), IList<MicrosoftDynamicsCRMphonecall> absScheduledprocessPhoneCalls = default(IList<MicrosoftDynamicsCRMphonecall>), IList<MicrosoftDynamicsCRMserviceappointment> absScheduledprocessServiceAppointments = default(IList<MicrosoftDynamicsCRMserviceappointment>), IList<MicrosoftDynamicsCRMtask> absScheduledprocessTasks = default(IList<MicrosoftDynamicsCRMtask>), IList<MicrosoftDynamicsCRMrecurringappointmentmaster> absScheduledprocessRecurringAppointmentMasters = default(IList<MicrosoftDynamicsCRMrecurringappointmentmaster>), IList<MicrosoftDynamicsCRMsocialactivity> absScheduledprocessSocialActivities = default(IList<MicrosoftDynamicsCRMsocialactivity>), IList<MicrosoftDynamicsCRMsyncerror> absScheduledprocessSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), IList<MicrosoftDynamicsCRMasyncoperation> absScheduledprocessAsyncOperations = default(IList<MicrosoftDynamicsCRMasyncoperation>), IList<MicrosoftDynamicsCRMbulkdeletefailure> absScheduledprocessBulkDeleteFailures = default(IList<MicrosoftDynamicsCRMbulkdeletefailure>), IList<MicrosoftDynamicsCRMabsScheduledprocessexecution> absScheduledprocessAbsScheduledprocessexecutions = default(IList<MicrosoftDynamicsCRMabsScheduledprocessexecution>), MicrosoftDynamicsCRMworkflow absProcessId = default(MicrosoftDynamicsCRMworkflow))
        {
            AbsMonday = absMonday;
            Overriddencreatedon = overriddencreatedon;
            this._owningbusinessunitValue = _owningbusinessunitValue;
            AbsSeptember = absSeptember;
            AbsScheduledprocessid = absScheduledprocessid;
            this._owninguserValue = _owninguserValue;
            AbsRecurrencepattern = absRecurrencepattern;
            Importsequencenumber = importsequencenumber;
            AbsThursday = absThursday;
            Versionnumber = versionnumber;
            AbsJanuary = absJanuary;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            AbsSunday = absSunday;
            AbsWednesday = absWednesday;
            AbsOctober = absOctober;
            AbsMay = absMay;
            AbsName = absName;
            AbsMarch = absMarch;
            AbsTuesday = absTuesday;
            AbsAmpm = absAmpm;
            AbsFetchxmlquery = absFetchxmlquery;
            AbsFebruary = absFebruary;
            this._modifiedbyValue = _modifiedbyValue;
            Modifiedon = modifiedon;
            Createdon = createdon;
            AbsJuly = absJuly;
            AbsNovember = absNovember;
            AbsSaturday = absSaturday;
            AbsHour = absHour;
            AbsInterval = absInterval;
            AbsFriday = absFriday;
            AbsMinute = absMinute;
            Timezoneruleversionnumber = timezoneruleversionnumber;
            AbsDayofmonth = absDayofmonth;
            AbsNextactivation = absNextactivation;
            AbsDecember = absDecember;
            this._absProcessidValue = _absProcessidValue;
            Statuscode = statuscode;
            Statecode = statecode;
            this._createdbyValue = _createdbyValue;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            AbsSuspendonfailure = absSuspendonfailure;
            AbsJune = absJune;
            this._owneridValue = _owneridValue;
            this._owningteamValue = _owningteamValue;
            Utcconversiontimezonecode = utcconversiontimezonecode;
            AbsAugust = absAugust;
            AbsApril = absApril;
            Createdbyname = createdbyname;
            Createdonbehalfbyname = createdonbehalfbyname;
            Modifiedbyname = modifiedbyname;
            Modifiedonbehalfbyname = modifiedonbehalfbyname;
            Owninguser = owninguser;
            Owningteam = owningteam;
            Ownerid = ownerid;
            Owningbusinessunit = owningbusinessunit;
            AbsScheduledprocessActivityPointers = absScheduledprocessActivityPointers;
            AbsScheduledprocessAppointments = absScheduledprocessAppointments;
            AbsScheduledprocessEmails = absScheduledprocessEmails;
            AbsScheduledprocessFaxes = absScheduledprocessFaxes;
            AbsScheduledprocessLetters = absScheduledprocessLetters;
            AbsScheduledprocessPhoneCalls = absScheduledprocessPhoneCalls;
            AbsScheduledprocessServiceAppointments = absScheduledprocessServiceAppointments;
            AbsScheduledprocessTasks = absScheduledprocessTasks;
            AbsScheduledprocessRecurringAppointmentMasters = absScheduledprocessRecurringAppointmentMasters;
            AbsScheduledprocessSocialActivities = absScheduledprocessSocialActivities;
            AbsScheduledprocessSyncErrors = absScheduledprocessSyncErrors;
            AbsScheduledprocessAsyncOperations = absScheduledprocessAsyncOperations;
            AbsScheduledprocessBulkDeleteFailures = absScheduledprocessBulkDeleteFailures;
            AbsScheduledprocessAbsScheduledprocessexecutions = absScheduledprocessAbsScheduledprocessexecutions;
            AbsProcessId = absProcessId;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the process should execute on Monday
        /// </summary>
        [JsonProperty(PropertyName = "abs_monday")]
        public bool? AbsMonday { get; set; }

        /// <summary>
        /// Gets or sets date and time that the record was migrated.
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// Gets or sets unique identifier for the business unit that owns the
        /// record
        /// </summary>
        [JsonProperty(PropertyName = "_owningbusinessunit_value")]
        public string _owningbusinessunitValue { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of September
        /// </summary>
        [JsonProperty(PropertyName = "abs_september")]
        public bool? AbsSeptember { get; set; }

        /// <summary>
        /// Gets or sets unique identifier for entity instances
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocessid")]
        public string AbsScheduledprocessid { get; set; }

        /// <summary>
        /// Gets or sets unique identifier for the user that owns the record.
        /// </summary>
        [JsonProperty(PropertyName = "_owninguser_value")]
        public string _owninguserValue { get; set; }

        /// <summary>
        /// Gets or sets the frequency on which the process is executed.
        /// </summary>
        [JsonProperty(PropertyName = "abs_recurrencepattern")]
        public int? AbsRecurrencepattern { get; set; }

        /// <summary>
        /// Gets or sets sequence number of the import that created this
        /// record.
        /// </summary>
        [JsonProperty(PropertyName = "importsequencenumber")]
        public int? Importsequencenumber { get; set; }

        /// <summary>
        /// Gets or sets the process should execute on Thursday
        /// </summary>
        [JsonProperty(PropertyName = "abs_thursday")]
        public bool? AbsThursday { get; set; }

        /// <summary>
        /// Gets or sets version Number
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of January
        /// </summary>
        [JsonProperty(PropertyName = "abs_january")]
        public bool? AbsJanuary { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who modified
        /// the record.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets the process should execute on Sunday
        /// </summary>
        [JsonProperty(PropertyName = "abs_sunday")]
        public bool? AbsSunday { get; set; }

        /// <summary>
        /// Gets or sets the process should execute on Wednesday
        /// </summary>
        [JsonProperty(PropertyName = "abs_wednesday")]
        public bool? AbsWednesday { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of October
        /// </summary>
        [JsonProperty(PropertyName = "abs_october")]
        public bool? AbsOctober { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of May
        /// </summary>
        [JsonProperty(PropertyName = "abs_may")]
        public bool? AbsMay { get; set; }

        /// <summary>
        /// Gets or sets the name of the custom entity.
        /// </summary>
        [JsonProperty(PropertyName = "abs_name")]
        public string AbsName { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of March
        /// </summary>
        [JsonProperty(PropertyName = "abs_march")]
        public bool? AbsMarch { get; set; }

        /// <summary>
        /// Gets or sets the process should execute on Tuesday
        /// </summary>
        [JsonProperty(PropertyName = "abs_tuesday")]
        public bool? AbsTuesday { get; set; }

        /// <summary>
        /// Gets or sets whether to execute the process before or after noon.
        /// </summary>
        [JsonProperty(PropertyName = "abs_ampm")]
        public bool? AbsAmpm { get; set; }

        /// <summary>
        /// Gets or sets the FetchXML Query to retrieve the target records for
        /// the execution.
        /// </summary>
        [JsonProperty(PropertyName = "abs_fetchxmlquery")]
        public string AbsFetchxmlquery { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of February
        /// </summary>
        [JsonProperty(PropertyName = "abs_february")]
        public bool? AbsFebruary { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who modified the record.
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// Gets or sets date and time when the record was modified.
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// Gets or sets date and time when the record was created.
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of July
        /// </summary>
        [JsonProperty(PropertyName = "abs_july")]
        public bool? AbsJuly { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of November
        /// </summary>
        [JsonProperty(PropertyName = "abs_november")]
        public bool? AbsNovember { get; set; }

        /// <summary>
        /// Gets or sets the process should execute on Saturday
        /// </summary>
        [JsonProperty(PropertyName = "abs_saturday")]
        public bool? AbsSaturday { get; set; }

        /// <summary>
        /// Gets or sets the hour of the day at which to execute the scheduled
        /// process.
        /// </summary>
        [JsonProperty(PropertyName = "abs_hour")]
        public int? AbsHour { get; set; }

        /// <summary>
        /// Gets or sets the process should execute every number of intervals.
        /// (Eg. Every 2 weeks, every 4 months, etc).
        /// </summary>
        [JsonProperty(PropertyName = "abs_interval")]
        public int? AbsInterval { get; set; }

        /// <summary>
        /// Gets or sets the process should execute on Friday
        /// </summary>
        [JsonProperty(PropertyName = "abs_friday")]
        public bool? AbsFriday { get; set; }

        /// <summary>
        /// Gets or sets specifies the minute of the hour in which to execute
        /// the scheduled process.
        /// </summary>
        [JsonProperty(PropertyName = "abs_minute")]
        public int? AbsMinute { get; set; }

        /// <summary>
        /// Gets or sets for internal use only.
        /// </summary>
        [JsonProperty(PropertyName = "timezoneruleversionnumber")]
        public int? Timezoneruleversionnumber { get; set; }

        /// <summary>
        /// Gets or sets execute the process on the nth day of the month
        /// </summary>
        [JsonProperty(PropertyName = "abs_dayofmonth")]
        public int? AbsDayofmonth { get; set; }

        /// <summary>
        /// Gets or sets date and Time of the Next Execution
        /// </summary>
        [JsonProperty(PropertyName = "abs_nextactivation")]
        public System.DateTimeOffset? AbsNextactivation { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of December
        /// </summary>
        [JsonProperty(PropertyName = "abs_december")]
        public bool? AbsDecember { get; set; }

        /// <summary>
        /// Gets or sets the target process to execute.
        /// </summary>
        [JsonProperty(PropertyName = "_abs_processid_value")]
        public string _absProcessidValue { get; set; }

        /// <summary>
        /// Gets or sets reason for the status of the Scheduled Process
        /// </summary>
        [JsonProperty(PropertyName = "statuscode")]
        public int? Statuscode { get; set; }

        /// <summary>
        /// Gets or sets status of the Scheduled Process
        /// </summary>
        [JsonProperty(PropertyName = "statecode")]
        public int? Statecode { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the user who created the record.
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the delegate user who created the
        /// record.
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// Gets or sets a Flag which sets whether or not the process should
        /// suspend subsequent executions when there is a processing failure.
        /// </summary>
        [JsonProperty(PropertyName = "abs_suspendonfailure")]
        public bool? AbsSuspendonfailure { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of June
        /// </summary>
        [JsonProperty(PropertyName = "abs_june")]
        public bool? AbsJune { get; set; }

        /// <summary>
        /// Gets or sets owner Id
        /// </summary>
        [JsonProperty(PropertyName = "_ownerid_value")]
        public string _owneridValue { get; set; }

        /// <summary>
        /// Gets or sets unique identifier for the team that owns the record.
        /// </summary>
        [JsonProperty(PropertyName = "_owningteam_value")]
        public string _owningteamValue { get; set; }

        /// <summary>
        /// Gets or sets time zone code that was in use when the record was
        /// created.
        /// </summary>
        [JsonProperty(PropertyName = "utcconversiontimezonecode")]
        public int? Utcconversiontimezonecode { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of August
        /// </summary>
        [JsonProperty(PropertyName = "abs_august")]
        public bool? AbsAugust { get; set; }

        /// <summary>
        /// Gets or sets the process should execute in the month of April
        /// </summary>
        [JsonProperty(PropertyName = "abs_april")]
        public bool? AbsApril { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdbyname")]
        public MicrosoftDynamicsCRMsystemuser Createdbyname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdonbehalfbyname")]
        public MicrosoftDynamicsCRMsystemuser Createdonbehalfbyname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedbyname")]
        public MicrosoftDynamicsCRMsystemuser Modifiedbyname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedonbehalfbyname")]
        public MicrosoftDynamicsCRMsystemuser Modifiedonbehalfbyname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owninguser")]
        public MicrosoftDynamicsCRMsystemuser Owninguser { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owningteam")]
        public MicrosoftDynamicsCRMteam Owningteam { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ownerid")]
        public MicrosoftDynamicsCRMprincipal Ownerid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owningbusinessunit")]
        public MicrosoftDynamicsCRMbusinessunit Owningbusinessunit { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_ActivityPointers")]
        public IList<MicrosoftDynamicsCRMactivitypointer> AbsScheduledprocessActivityPointers { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_Appointments")]
        public IList<MicrosoftDynamicsCRMappointment> AbsScheduledprocessAppointments { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_Emails")]
        public IList<MicrosoftDynamicsCRMemail> AbsScheduledprocessEmails { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_Faxes")]
        public IList<MicrosoftDynamicsCRMfax> AbsScheduledprocessFaxes { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_Letters")]
        public IList<MicrosoftDynamicsCRMletter> AbsScheduledprocessLetters { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_PhoneCalls")]
        public IList<MicrosoftDynamicsCRMphonecall> AbsScheduledprocessPhoneCalls { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_ServiceAppointments")]
        public IList<MicrosoftDynamicsCRMserviceappointment> AbsScheduledprocessServiceAppointments { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_Tasks")]
        public IList<MicrosoftDynamicsCRMtask> AbsScheduledprocessTasks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_RecurringAppointmentMasters")]
        public IList<MicrosoftDynamicsCRMrecurringappointmentmaster> AbsScheduledprocessRecurringAppointmentMasters { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_SocialActivities")]
        public IList<MicrosoftDynamicsCRMsocialactivity> AbsScheduledprocessSocialActivities { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> AbsScheduledprocessSyncErrors { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_AsyncOperations")]
        public IList<MicrosoftDynamicsCRMasyncoperation> AbsScheduledprocessAsyncOperations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_BulkDeleteFailures")]
        public IList<MicrosoftDynamicsCRMbulkdeletefailure> AbsScheduledprocessBulkDeleteFailures { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_scheduledprocess_abs_scheduledprocessexecutions")]
        public IList<MicrosoftDynamicsCRMabsScheduledprocessexecution> AbsScheduledprocessAbsScheduledprocessexecutions { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "abs_ProcessId")]
        public MicrosoftDynamicsCRMworkflow AbsProcessId { get; set; }

    }
}
