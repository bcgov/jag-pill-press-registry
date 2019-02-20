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
    /// bcgov_certificate
    /// </summary>
    public partial class MicrosoftDynamicsCRMbcgovCertificate
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMbcgovCertificate class.
        /// </summary>
        public MicrosoftDynamicsCRMbcgovCertificate()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMbcgovCertificate class.
        /// </summary>
        public MicrosoftDynamicsCRMbcgovCertificate(int? statecode = default(int?), string _owningteamValue = default(string), string bcgovExpirydatelongdatestring = default(string), string bcgovCertificateholder = default(string), string bcgovIssueddatelongdatestring = default(string), string _bcgovCertificatetypeValue = default(string), string _owningbusinessunitValue = default(string), string _createdonbehalfbyValue = default(string), string _owninguserValue = default(string), int? utcconversiontimezonecode = default(int?), string bcgovBusinessaddresscountry = default(string), int? statuscode = default(int?), int? importsequencenumber = default(int?), string bcgovBusinessaddressstreet = default(string), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), string bcgovBusinessaddresspostalcode = default(string), System.DateTimeOffset? bcgovIssueddate = default(System.DateTimeOffset?), bool? bcgovWaiverexpirynotificationsent = default(bool?), string bcgovBusinessaddresscity = default(string), string _modifiedbyValue = default(string), long? versionnumber = default(long?), string bcgovCertificateid = default(string), string _bcgovApplicationValue = default(string), string bcgovBusinessaddressprovince = default(string), string _owneridValue = default(string), string _createdbyValue = default(string), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), string _bcgovCertificateholderbusinessValue = default(string), string bcgovName = default(string), string _modifiedonbehalfbyValue = default(string), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), bool? bcgovRegisteredsellerexpirynotificationsent = default(bool?), System.DateTimeOffset? bcgovExpirydate = default(System.DateTimeOffset?), string _bcgovEquipmentValue = default(string), int? timezoneruleversionnumber = default(int?), MicrosoftDynamicsCRMsystemuser createdbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdonbehalfbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedonbehalfbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser owninguser = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMteam owningteam = default(MicrosoftDynamicsCRMteam), MicrosoftDynamicsCRMprincipal ownerid = default(MicrosoftDynamicsCRMprincipal), MicrosoftDynamicsCRMbusinessunit owningbusinessunit = default(MicrosoftDynamicsCRMbusinessunit), IList<MicrosoftDynamicsCRMsyncerror> bcgovCertificateSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), IList<MicrosoftDynamicsCRMduplicaterecord> bcgovCertificateDuplicateMatchingRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), IList<MicrosoftDynamicsCRMduplicaterecord> bcgovCertificateDuplicateBaseRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), IList<MicrosoftDynamicsCRMasyncoperation> bcgovCertificateAsyncOperations = default(IList<MicrosoftDynamicsCRMasyncoperation>), IList<MicrosoftDynamicsCRMbulkdeletefailure> bcgovCertificateBulkDeleteFailures = default(IList<MicrosoftDynamicsCRMbulkdeletefailure>), MicrosoftDynamicsCRMaccount bcgovCertificateHolderBusiness = default(MicrosoftDynamicsCRMaccount), MicrosoftDynamicsCRMbcgovApplicationtype bcgovCertificateType = default(MicrosoftDynamicsCRMbcgovApplicationtype), IList<MicrosoftDynamicsCRMbcgovCertificateapprovedproduct> bcgovCertificateBcgovCertificateapprovedproductCertificateId = default(IList<MicrosoftDynamicsCRMbcgovCertificateapprovedproduct>), IList<MicrosoftDynamicsCRMbcgovCertificatetermsandconditions> bcgovCertificateBcgovCertificatetermsandconditionsCertificate = default(IList<MicrosoftDynamicsCRMbcgovCertificatetermsandconditions>), MicrosoftDynamicsCRMbcgovEquipment bcgovEquipment = default(MicrosoftDynamicsCRMbcgovEquipment), MicrosoftDynamicsCRMincident bcgovApplication = default(MicrosoftDynamicsCRMincident))
        {
            Statecode = statecode;
            this._owningteamValue = _owningteamValue;
            BcgovExpirydatelongdatestring = bcgovExpirydatelongdatestring;
            BcgovCertificateholder = bcgovCertificateholder;
            BcgovIssueddatelongdatestring = bcgovIssueddatelongdatestring;
            this._bcgovCertificatetypeValue = _bcgovCertificatetypeValue;
            this._owningbusinessunitValue = _owningbusinessunitValue;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            this._owninguserValue = _owninguserValue;
            Utcconversiontimezonecode = utcconversiontimezonecode;
            BcgovBusinessaddresscountry = bcgovBusinessaddresscountry;
            Statuscode = statuscode;
            Importsequencenumber = importsequencenumber;
            BcgovBusinessaddressstreet = bcgovBusinessaddressstreet;
            Modifiedon = modifiedon;
            BcgovBusinessaddresspostalcode = bcgovBusinessaddresspostalcode;
            BcgovIssueddate = bcgovIssueddate;
            BcgovWaiverexpirynotificationsent = bcgovWaiverexpirynotificationsent;
            BcgovBusinessaddresscity = bcgovBusinessaddresscity;
            this._modifiedbyValue = _modifiedbyValue;
            Versionnumber = versionnumber;
            BcgovCertificateid = bcgovCertificateid;
            this._bcgovApplicationValue = _bcgovApplicationValue;
            BcgovBusinessaddressprovince = bcgovBusinessaddressprovince;
            this._owneridValue = _owneridValue;
            this._createdbyValue = _createdbyValue;
            Overriddencreatedon = overriddencreatedon;
            this._bcgovCertificateholderbusinessValue = _bcgovCertificateholderbusinessValue;
            BcgovName = bcgovName;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            Createdon = createdon;
            BcgovRegisteredsellerexpirynotificationsent = bcgovRegisteredsellerexpirynotificationsent;
            BcgovExpirydate = bcgovExpirydate;
            this._bcgovEquipmentValue = _bcgovEquipmentValue;
            Timezoneruleversionnumber = timezoneruleversionnumber;
            Createdbyname = createdbyname;
            Createdonbehalfbyname = createdonbehalfbyname;
            Modifiedbyname = modifiedbyname;
            Modifiedonbehalfbyname = modifiedonbehalfbyname;
            Owninguser = owninguser;
            Owningteam = owningteam;
            Ownerid = ownerid;
            Owningbusinessunit = owningbusinessunit;
            BcgovCertificateSyncErrors = bcgovCertificateSyncErrors;
            BcgovCertificateDuplicateMatchingRecord = bcgovCertificateDuplicateMatchingRecord;
            BcgovCertificateDuplicateBaseRecord = bcgovCertificateDuplicateBaseRecord;
            BcgovCertificateAsyncOperations = bcgovCertificateAsyncOperations;
            BcgovCertificateBulkDeleteFailures = bcgovCertificateBulkDeleteFailures;
            BcgovCertificateHolderBusiness = bcgovCertificateHolderBusiness;
            BcgovCertificateType = bcgovCertificateType;
            BcgovCertificateBcgovCertificateapprovedproductCertificateId = bcgovCertificateBcgovCertificateapprovedproductCertificateId;
            BcgovCertificateBcgovCertificatetermsandconditionsCertificate = bcgovCertificateBcgovCertificatetermsandconditionsCertificate;
            BcgovEquipment = bcgovEquipment;
            BcgovApplication = bcgovApplication;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statecode")]
        public int? Statecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owningteam_value")]
        public string _owningteamValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_expirydatelongdatestring")]
        public string BcgovExpirydatelongdatestring { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificateholder")]
        public string BcgovCertificateholder { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_issueddatelongdatestring")]
        public string BcgovIssueddatelongdatestring { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_bcgov_certificatetype_value")]
        public string _bcgovCertificatetypeValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owningbusinessunit_value")]
        public string _owningbusinessunitValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owninguser_value")]
        public string _owninguserValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "utcconversiontimezonecode")]
        public int? Utcconversiontimezonecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_businessaddresscountry")]
        public string BcgovBusinessaddresscountry { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statuscode")]
        public int? Statuscode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "importsequencenumber")]
        public int? Importsequencenumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_businessaddressstreet")]
        public string BcgovBusinessaddressstreet { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_businessaddresspostalcode")]
        public string BcgovBusinessaddresspostalcode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_issueddate")]
        public System.DateTimeOffset? BcgovIssueddate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_waiverexpirynotificationsent")]
        public bool? BcgovWaiverexpirynotificationsent { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_businessaddresscity")]
        public string BcgovBusinessaddresscity { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public long? Versionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificateid")]
        public string BcgovCertificateid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_bcgov_application_value")]
        public string _bcgovApplicationValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_businessaddressprovince")]
        public string BcgovBusinessaddressprovince { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_ownerid_value")]
        public string _owneridValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_bcgov_certificateholderbusiness_value")]
        public string _bcgovCertificateholderbusinessValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_name")]
        public string BcgovName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_registeredsellerexpirynotificationsent")]
        public bool? BcgovRegisteredsellerexpirynotificationsent { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_expirydate")]
        public System.DateTimeOffset? BcgovExpirydate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_bcgov_equipment_value")]
        public string _bcgovEquipmentValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "timezoneruleversionnumber")]
        public int? Timezoneruleversionnumber { get; set; }

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
        [JsonProperty(PropertyName = "bcgov_certificate_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> BcgovCertificateSyncErrors { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificate_DuplicateMatchingRecord")]
        public IList<MicrosoftDynamicsCRMduplicaterecord> BcgovCertificateDuplicateMatchingRecord { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificate_DuplicateBaseRecord")]
        public IList<MicrosoftDynamicsCRMduplicaterecord> BcgovCertificateDuplicateBaseRecord { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificate_AsyncOperations")]
        public IList<MicrosoftDynamicsCRMasyncoperation> BcgovCertificateAsyncOperations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificate_BulkDeleteFailures")]
        public IList<MicrosoftDynamicsCRMbulkdeletefailure> BcgovCertificateBulkDeleteFailures { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_CertificateHolderBusiness")]
        public MicrosoftDynamicsCRMaccount BcgovCertificateHolderBusiness { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_CertificateType")]
        public MicrosoftDynamicsCRMbcgovApplicationtype BcgovCertificateType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificate_bcgov_certificateapprovedproduct_CertificateId")]
        public IList<MicrosoftDynamicsCRMbcgovCertificateapprovedproduct> BcgovCertificateBcgovCertificateapprovedproductCertificateId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificate_bcgov_certificatetermsandconditions_Certificate")]
        public IList<MicrosoftDynamicsCRMbcgovCertificatetermsandconditions> BcgovCertificateBcgovCertificatetermsandconditionsCertificate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_Equipment")]
        public MicrosoftDynamicsCRMbcgovEquipment BcgovEquipment { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_Application")]
        public MicrosoftDynamicsCRMincident BcgovApplication { get; set; }

    }
}
