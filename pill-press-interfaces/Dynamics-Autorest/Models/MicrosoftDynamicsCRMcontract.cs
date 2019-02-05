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
    /// contract
    /// </summary>
    public partial class MicrosoftDynamicsCRMcontract
    {
        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMcontract
        /// class.
        /// </summary>
        public MicrosoftDynamicsCRMcontract()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMcontract
        /// class.
        /// </summary>
        public MicrosoftDynamicsCRMcontract(string _createdonbehalfbyValue = default(string), string _owningteamValue = default(string), string _billingaccountidValue = default(string), string contracttemplateabbreviation = default(string), object netprice = default(object), object exchangerate = default(object), string entityimageUrl = default(string), long? entityimageTimestamp = default(long?), object totalpriceBase = default(object), string _billtoaddressValue = default(string), System.DateTimeOffset? billingstarton = default(System.DateTimeOffset?), string _serviceaddressValue = default(string), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), int? statuscode = default(int?), object totalprice = default(object), long? versionnumber = default(long?), string _billingcontactidValue = default(string), System.DateTimeOffset? billingendon = default(System.DateTimeOffset?), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), string _contracttemplateidValue = default(string), object entityimage = default(object), int? contractservicelevelcode = default(int?), object totaldiscount = default(object), int? timezoneruleversionnumber = default(int?), int? statecode = default(int?), string effectivitycalendar = default(string), string contractlanguage = default(string), string entityimageid = default(string), string _billingcustomeridValue = default(string), string _owningbusinessunitValue = default(string), int? importsequencenumber = default(int?), string contractnumber = default(string), string title = default(string), int? allotmenttypecode = default(int?), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), string _modifiedbyValue = default(string), int? utcconversiontimezonecode = default(int?), string contractid = default(string), string _customeridValue = default(string), object netpriceBase = default(object), string _accountidValue = default(string), System.DateTimeOffset? activeon = default(System.DateTimeOffset?), int? billingfrequencycode = default(int?), string _createdbyValue = default(string), string _modifiedonbehalfbyValue = default(string), string _originatingcontractValue = default(string), string _contactidValue = default(string), bool? usediscountaspercentage = default(bool?), string _owninguserValue = default(string), string _transactioncurrencyidValue = default(string), System.DateTimeOffset? expireson = default(System.DateTimeOffset?), System.DateTimeOffset? cancelon = default(System.DateTimeOffset?), int? duration = default(int?), object totaldiscountBase = default(object), string _owneridValue = default(string), IList<MicrosoftDynamicsCRMcontact> servicecontractcontactsAssociation = default(IList<MicrosoftDynamicsCRMcontact>), MicrosoftDynamicsCRMaccount billingcustomeridAccount = default(MicrosoftDynamicsCRMaccount), MicrosoftDynamicsCRMsystemuser owninguser = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMactivitypointer> contractActivityPointers = default(IList<MicrosoftDynamicsCRMactivitypointer>), MicrosoftDynamicsCRMcustomeraddress serviceaddress = default(MicrosoftDynamicsCRMcustomeraddress), IList<MicrosoftDynamicsCRMrecurringappointmentmaster> contractRecurringAppointmentMasters = default(IList<MicrosoftDynamicsCRMrecurringappointmentmaster>), MicrosoftDynamicsCRMteam owningteam = default(MicrosoftDynamicsCRMteam), IList<MicrosoftDynamicsCRMactivityparty> contractActivityParties = default(IList<MicrosoftDynamicsCRMactivityparty>), IList<MicrosoftDynamicsCRMconnection> contractConnections1 = default(IList<MicrosoftDynamicsCRMconnection>), IList<MicrosoftDynamicsCRMconnection> contractConnections2 = default(IList<MicrosoftDynamicsCRMconnection>), MicrosoftDynamicsCRMtransactioncurrency transactioncurrencyid = default(MicrosoftDynamicsCRMtransactioncurrency), IList<MicrosoftDynamicsCRMasyncoperation> contractAsyncOperations = default(IList<MicrosoftDynamicsCRMasyncoperation>), IList<MicrosoftDynamicsCRMappointment> contractAppointments = default(IList<MicrosoftDynamicsCRMappointment>), MicrosoftDynamicsCRMcontact customeridContact = default(MicrosoftDynamicsCRMcontact), MicrosoftDynamicsCRMcontact billingcustomeridContact = default(MicrosoftDynamicsCRMcontact), IList<MicrosoftDynamicsCRMduplicaterecord> contractDuplicateBaseRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), MicrosoftDynamicsCRMbusinessunit owningbusinessunit = default(MicrosoftDynamicsCRMbusinessunit), IList<MicrosoftDynamicsCRMannotation> contractAnnotation = default(IList<MicrosoftDynamicsCRMannotation>), IList<MicrosoftDynamicsCRMserviceappointment> contractServiceAppointments = default(IList<MicrosoftDynamicsCRMserviceappointment>), MicrosoftDynamicsCRMsystemuser modifiedonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedby = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMcontractdetail> contractLineItems = default(IList<MicrosoftDynamicsCRMcontractdetail>), MicrosoftDynamicsCRMcontract originatingcontract = default(MicrosoftDynamicsCRMcontract), IList<MicrosoftDynamicsCRMcontract> contractOriginatingContract = default(IList<MicrosoftDynamicsCRMcontract>), IList<MicrosoftDynamicsCRMsyncerror> contractSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), IList<MicrosoftDynamicsCRMphonecall> contractPhonecalls = default(IList<MicrosoftDynamicsCRMphonecall>), MicrosoftDynamicsCRMsystemuser createdonbehalfby = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMletter> contractLetters = default(IList<MicrosoftDynamicsCRMletter>), MicrosoftDynamicsCRMcustomeraddress billtoaddress = default(MicrosoftDynamicsCRMcustomeraddress), IList<MicrosoftDynamicsCRMsocialactivity> contractSocialActivities = default(IList<MicrosoftDynamicsCRMsocialactivity>), IList<MicrosoftDynamicsCRMfax> contractFaxes = default(IList<MicrosoftDynamicsCRMfax>), IList<MicrosoftDynamicsCRMtask> contractTasks = default(IList<MicrosoftDynamicsCRMtask>), MicrosoftDynamicsCRMsystemuser createdby = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMbulkdeletefailure> contractBulkDeleteFailures = default(IList<MicrosoftDynamicsCRMbulkdeletefailure>), IList<MicrosoftDynamicsCRMduplicaterecord> contractDuplicateMatchingRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), IList<MicrosoftDynamicsCRMemail> contractEmails = default(IList<MicrosoftDynamicsCRMemail>), MicrosoftDynamicsCRMaccount customeridAccount = default(MicrosoftDynamicsCRMaccount), IList<MicrosoftDynamicsCRMincident> contractCases = default(IList<MicrosoftDynamicsCRMincident>), MicrosoftDynamicsCRMcontracttemplate contracttemplateid = default(MicrosoftDynamicsCRMcontracttemplate), MicrosoftDynamicsCRMprincipal ownerid = default(MicrosoftDynamicsCRMprincipal), IList<MicrosoftDynamicsCRMabsScheduledprocessexecution> contractAbsScheduledprocessexecutions = default(IList<MicrosoftDynamicsCRMabsScheduledprocessexecution>))
        {
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            this._owningteamValue = _owningteamValue;
            this._billingaccountidValue = _billingaccountidValue;
            Contracttemplateabbreviation = contracttemplateabbreviation;
            Netprice = netprice;
            Exchangerate = exchangerate;
            EntityimageUrl = entityimageUrl;
            EntityimageTimestamp = entityimageTimestamp;
            TotalpriceBase = totalpriceBase;
            this._billtoaddressValue = _billtoaddressValue;
            Billingstarton = billingstarton;
            this._serviceaddressValue = _serviceaddressValue;
            Modifiedon = modifiedon;
            Statuscode = statuscode;
            Totalprice = totalprice;
            Versionnumber = versionnumber;
            this._billingcontactidValue = _billingcontactidValue;
            Billingendon = billingendon;
            Createdon = createdon;
            this._contracttemplateidValue = _contracttemplateidValue;
            Entityimage = entityimage;
            Contractservicelevelcode = contractservicelevelcode;
            Totaldiscount = totaldiscount;
            Timezoneruleversionnumber = timezoneruleversionnumber;
            Statecode = statecode;
            Effectivitycalendar = effectivitycalendar;
            Contractlanguage = contractlanguage;
            Entityimageid = entityimageid;
            this._billingcustomeridValue = _billingcustomeridValue;
            this._owningbusinessunitValue = _owningbusinessunitValue;
            Importsequencenumber = importsequencenumber;
            Contractnumber = contractnumber;
            Title = title;
            Allotmenttypecode = allotmenttypecode;
            Overriddencreatedon = overriddencreatedon;
            this._modifiedbyValue = _modifiedbyValue;
            Utcconversiontimezonecode = utcconversiontimezonecode;
            Contractid = contractid;
            this._customeridValue = _customeridValue;
            NetpriceBase = netpriceBase;
            this._accountidValue = _accountidValue;
            Activeon = activeon;
            Billingfrequencycode = billingfrequencycode;
            this._createdbyValue = _createdbyValue;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            this._originatingcontractValue = _originatingcontractValue;
            this._contactidValue = _contactidValue;
            Usediscountaspercentage = usediscountaspercentage;
            this._owninguserValue = _owninguserValue;
            this._transactioncurrencyidValue = _transactioncurrencyidValue;
            Expireson = expireson;
            Cancelon = cancelon;
            Duration = duration;
            TotaldiscountBase = totaldiscountBase;
            this._owneridValue = _owneridValue;
            ServicecontractcontactsAssociation = servicecontractcontactsAssociation;
            BillingcustomeridAccount = billingcustomeridAccount;
            Owninguser = owninguser;
            ContractActivityPointers = contractActivityPointers;
            Serviceaddress = serviceaddress;
            ContractRecurringAppointmentMasters = contractRecurringAppointmentMasters;
            Owningteam = owningteam;
            ContractActivityParties = contractActivityParties;
            ContractConnections1 = contractConnections1;
            ContractConnections2 = contractConnections2;
            Transactioncurrencyid = transactioncurrencyid;
            ContractAsyncOperations = contractAsyncOperations;
            ContractAppointments = contractAppointments;
            CustomeridContact = customeridContact;
            BillingcustomeridContact = billingcustomeridContact;
            ContractDuplicateBaseRecord = contractDuplicateBaseRecord;
            Owningbusinessunit = owningbusinessunit;
            ContractAnnotation = contractAnnotation;
            ContractServiceAppointments = contractServiceAppointments;
            Modifiedonbehalfby = modifiedonbehalfby;
            Modifiedby = modifiedby;
            ContractLineItems = contractLineItems;
            Originatingcontract = originatingcontract;
            ContractOriginatingContract = contractOriginatingContract;
            ContractSyncErrors = contractSyncErrors;
            ContractPhonecalls = contractPhonecalls;
            Createdonbehalfby = createdonbehalfby;
            ContractLetters = contractLetters;
            Billtoaddress = billtoaddress;
            ContractSocialActivities = contractSocialActivities;
            ContractFaxes = contractFaxes;
            ContractTasks = contractTasks;
            Createdby = createdby;
            ContractBulkDeleteFailures = contractBulkDeleteFailures;
            ContractDuplicateMatchingRecord = contractDuplicateMatchingRecord;
            ContractEmails = contractEmails;
            CustomeridAccount = customeridAccount;
            ContractCases = contractCases;
            Contracttemplateid = contracttemplateid;
            Ownerid = ownerid;
            ContractAbsScheduledprocessexecutions = contractAbsScheduledprocessexecutions;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owningteam_value")]
        public string _owningteamValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_billingaccountid_value")]
        public string _billingaccountidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contracttemplateabbreviation")]
        public string Contracttemplateabbreviation { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "netprice")]
        public object Netprice { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "exchangerate")]
        public object Exchangerate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entityimage_url")]
        public string EntityimageUrl { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entityimage_timestamp")]
        public long? EntityimageTimestamp { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "totalprice_base")]
        public object TotalpriceBase { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_billtoaddress_value")]
        public string _billtoaddressValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "billingstarton")]
        public System.DateTimeOffset? Billingstarton { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_serviceaddress_value")]
        public string _serviceaddressValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statuscode")]
        public int? Statuscode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "totalprice")]
        public object Totalprice { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public long? Versionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_billingcontactid_value")]
        public string _billingcontactidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "billingendon")]
        public System.DateTimeOffset? Billingendon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_contracttemplateid_value")]
        public string _contracttemplateidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entityimage")]
        public object Entityimage { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contractservicelevelcode")]
        public int? Contractservicelevelcode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "totaldiscount")]
        public object Totaldiscount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "timezoneruleversionnumber")]
        public int? Timezoneruleversionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statecode")]
        public int? Statecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "effectivitycalendar")]
        public string Effectivitycalendar { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contractlanguage")]
        public string Contractlanguage { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "entityimageid")]
        public string Entityimageid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_billingcustomerid_value")]
        public string _billingcustomeridValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owningbusinessunit_value")]
        public string _owningbusinessunitValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "importsequencenumber")]
        public int? Importsequencenumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contractnumber")]
        public string Contractnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "allotmenttypecode")]
        public int? Allotmenttypecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "utcconversiontimezonecode")]
        public int? Utcconversiontimezonecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contractid")]
        public string Contractid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_customerid_value")]
        public string _customeridValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "netprice_base")]
        public object NetpriceBase { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_accountid_value")]
        public string _accountidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "activeon")]
        public System.DateTimeOffset? Activeon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "billingfrequencycode")]
        public int? Billingfrequencycode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_originatingcontract_value")]
        public string _originatingcontractValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_contactid_value")]
        public string _contactidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "usediscountaspercentage")]
        public bool? Usediscountaspercentage { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owninguser_value")]
        public string _owninguserValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_transactioncurrencyid_value")]
        public string _transactioncurrencyidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "expireson")]
        public System.DateTimeOffset? Expireson { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "cancelon")]
        public System.DateTimeOffset? Cancelon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "duration")]
        public int? Duration { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "totaldiscount_base")]
        public object TotaldiscountBase { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_ownerid_value")]
        public string _owneridValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "servicecontractcontacts_association")]
        public IList<MicrosoftDynamicsCRMcontact> ServicecontractcontactsAssociation { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "billingcustomerid_account")]
        public MicrosoftDynamicsCRMaccount BillingcustomeridAccount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owninguser")]
        public MicrosoftDynamicsCRMsystemuser Owninguser { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_ActivityPointers")]
        public IList<MicrosoftDynamicsCRMactivitypointer> ContractActivityPointers { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "serviceaddress")]
        public MicrosoftDynamicsCRMcustomeraddress Serviceaddress { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_RecurringAppointmentMasters")]
        public IList<MicrosoftDynamicsCRMrecurringappointmentmaster> ContractRecurringAppointmentMasters { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owningteam")]
        public MicrosoftDynamicsCRMteam Owningteam { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contract_activity_parties")]
        public IList<MicrosoftDynamicsCRMactivityparty> ContractActivityParties { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contract_connections1")]
        public IList<MicrosoftDynamicsCRMconnection> ContractConnections1 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contract_connections2")]
        public IList<MicrosoftDynamicsCRMconnection> ContractConnections2 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "transactioncurrencyid")]
        public MicrosoftDynamicsCRMtransactioncurrency Transactioncurrencyid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_AsyncOperations")]
        public IList<MicrosoftDynamicsCRMasyncoperation> ContractAsyncOperations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_Appointments")]
        public IList<MicrosoftDynamicsCRMappointment> ContractAppointments { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "customerid_contact")]
        public MicrosoftDynamicsCRMcontact CustomeridContact { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "billingcustomerid_contact")]
        public MicrosoftDynamicsCRMcontact BillingcustomeridContact { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_DuplicateBaseRecord")]
        public IList<MicrosoftDynamicsCRMduplicaterecord> ContractDuplicateBaseRecord { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owningbusinessunit")]
        public MicrosoftDynamicsCRMbusinessunit Owningbusinessunit { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_Annotation")]
        public IList<MicrosoftDynamicsCRMannotation> ContractAnnotation { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_ServiceAppointments")]
        public IList<MicrosoftDynamicsCRMserviceappointment> ContractServiceAppointments { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contract_line_items")]
        public IList<MicrosoftDynamicsCRMcontractdetail> ContractLineItems { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "originatingcontract")]
        public MicrosoftDynamicsCRMcontract Originatingcontract { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contract_originating_contract")]
        public IList<MicrosoftDynamicsCRMcontract> ContractOriginatingContract { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> ContractSyncErrors { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_Phonecalls")]
        public IList<MicrosoftDynamicsCRMphonecall> ContractPhonecalls { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser Createdonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_Letters")]
        public IList<MicrosoftDynamicsCRMletter> ContractLetters { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "billtoaddress")]
        public MicrosoftDynamicsCRMcustomeraddress Billtoaddress { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_SocialActivities")]
        public IList<MicrosoftDynamicsCRMsocialactivity> ContractSocialActivities { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_Faxes")]
        public IList<MicrosoftDynamicsCRMfax> ContractFaxes { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_Tasks")]
        public IList<MicrosoftDynamicsCRMtask> ContractTasks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdby")]
        public MicrosoftDynamicsCRMsystemuser Createdby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_BulkDeleteFailures")]
        public IList<MicrosoftDynamicsCRMbulkdeletefailure> ContractBulkDeleteFailures { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_DuplicateMatchingRecord")]
        public IList<MicrosoftDynamicsCRMduplicaterecord> ContractDuplicateMatchingRecord { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Contract_Emails")]
        public IList<MicrosoftDynamicsCRMemail> ContractEmails { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "customerid_account")]
        public MicrosoftDynamicsCRMaccount CustomeridAccount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contract_cases")]
        public IList<MicrosoftDynamicsCRMincident> ContractCases { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contracttemplateid")]
        public MicrosoftDynamicsCRMcontracttemplate Contracttemplateid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ownerid")]
        public MicrosoftDynamicsCRMprincipal Ownerid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contract_abs_scheduledprocessexecutions")]
        public IList<MicrosoftDynamicsCRMabsScheduledprocessexecution> ContractAbsScheduledprocessexecutions { get; set; }

    }
}
