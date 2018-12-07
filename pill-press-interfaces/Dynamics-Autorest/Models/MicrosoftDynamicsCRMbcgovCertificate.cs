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
        public MicrosoftDynamicsCRMbcgovCertificate(int? utcconversiontimezonecode = default(int?), int? timezoneruleversionnumber = default(int?), string _modifiedbyValue = default(string), string _bcgovApplicationValue = default(string), string _owningbusinessunitValue = default(string), string bcgovCertificateholder = default(string), long? versionnumber = default(long?), string _owneridValue = default(string), string bcgovBusinessaddresscountry = default(string), string _bcgovCertificatetypeValue = default(string), string bcgovApprovedintendeduse = default(string), string bcgovBusinessaddressprovince = default(string), System.DateTimeOffset? bcgovIssueddate = default(System.DateTimeOffset?), string _owninguserValue = default(string), int? importsequencenumber = default(int?), string bcgovName = default(string), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), string bcgovExpirydatelongdatestring = default(string), string bcgovBusinessaddresscity = default(string), string bcgovBusinessaddresspostalcode = default(string), string bcgovCertificateid = default(string), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), string _modifiedonbehalfbyValue = default(string), string _createdonbehalfbyValue = default(string), string _createdbyValue = default(string), string bcgovIssueddatelongdatestring = default(string), int? statecode = default(int?), int? statuscode = default(int?), System.DateTimeOffset? bcgovExpirydate = default(System.DateTimeOffset?), string _owningteamValue = default(string), string bcgovBusinessaddressstreet = default(string), int? bcgovApprovedproductcategory = default(int?), int? bcgovApprovedproductsubcategory = default(int?), MicrosoftDynamicsCRMsystemuser createdbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdonbehalfbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedonbehalfbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser owninguser = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMteam owningteam = default(MicrosoftDynamicsCRMteam), MicrosoftDynamicsCRMprincipal ownerid = default(MicrosoftDynamicsCRMprincipal), MicrosoftDynamicsCRMbusinessunit owningbusinessunit = default(MicrosoftDynamicsCRMbusinessunit), IList<MicrosoftDynamicsCRMsyncerror> bcgovCertificateSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), IList<MicrosoftDynamicsCRMduplicaterecord> bcgovCertificateDuplicateMatchingRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), IList<MicrosoftDynamicsCRMduplicaterecord> bcgovCertificateDuplicateBaseRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), IList<MicrosoftDynamicsCRMasyncoperation> bcgovCertificateAsyncOperations = default(IList<MicrosoftDynamicsCRMasyncoperation>), IList<MicrosoftDynamicsCRMbulkdeletefailure> bcgovCertificateBulkDeleteFailures = default(IList<MicrosoftDynamicsCRMbulkdeletefailure>), MicrosoftDynamicsCRMbcgovApplicationtype bcgovCertificateType = default(MicrosoftDynamicsCRMbcgovApplicationtype), IList<MicrosoftDynamicsCRMbcgovApplicationterms> bcgovCertificateBcgovApplicationtermsCertificate = default(IList<MicrosoftDynamicsCRMbcgovApplicationterms>), MicrosoftDynamicsCRMincident bcgovApplication = default(MicrosoftDynamicsCRMincident), IList<MicrosoftDynamicsCRMbcgovCertificatetermsandconditions> bcgovCertificateBcgovCertificatetermsandconditionsCertificate = default(IList<MicrosoftDynamicsCRMbcgovCertificatetermsandconditions>))
        {
            Utcconversiontimezonecode = utcconversiontimezonecode;
            Timezoneruleversionnumber = timezoneruleversionnumber;
            this._modifiedbyValue = _modifiedbyValue;
            this._bcgovApplicationValue = _bcgovApplicationValue;
            this._owningbusinessunitValue = _owningbusinessunitValue;
            BcgovCertificateholder = bcgovCertificateholder;
            Versionnumber = versionnumber;
            this._owneridValue = _owneridValue;
            BcgovBusinessaddresscountry = bcgovBusinessaddresscountry;
            this._bcgovCertificatetypeValue = _bcgovCertificatetypeValue;
            BcgovApprovedintendeduse = bcgovApprovedintendeduse;
            BcgovBusinessaddressprovince = bcgovBusinessaddressprovince;
            BcgovIssueddate = bcgovIssueddate;
            this._owninguserValue = _owninguserValue;
            Importsequencenumber = importsequencenumber;
            BcgovName = bcgovName;
            Overriddencreatedon = overriddencreatedon;
            Createdon = createdon;
            BcgovExpirydatelongdatestring = bcgovExpirydatelongdatestring;
            BcgovBusinessaddresscity = bcgovBusinessaddresscity;
            BcgovBusinessaddresspostalcode = bcgovBusinessaddresspostalcode;
            BcgovCertificateid = bcgovCertificateid;
            Modifiedon = modifiedon;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            this._createdbyValue = _createdbyValue;
            BcgovIssueddatelongdatestring = bcgovIssueddatelongdatestring;
            Statecode = statecode;
            Statuscode = statuscode;
            BcgovExpirydate = bcgovExpirydate;
            this._owningteamValue = _owningteamValue;
            BcgovBusinessaddressstreet = bcgovBusinessaddressstreet;
            BcgovApprovedproductcategory = bcgovApprovedproductcategory;
            BcgovApprovedproductsubcategory = bcgovApprovedproductsubcategory;
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
            BcgovCertificateType = bcgovCertificateType;
            BcgovCertificateBcgovApplicationtermsCertificate = bcgovCertificateBcgovApplicationtermsCertificate;
            BcgovApplication = bcgovApplication;
            BcgovCertificateBcgovCertificatetermsandconditionsCertificate = bcgovCertificateBcgovCertificatetermsandconditionsCertificate;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "utcconversiontimezonecode")]
        public int? Utcconversiontimezonecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "timezoneruleversionnumber")]
        public int? Timezoneruleversionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_bcgov_application_value")]
        public string _bcgovApplicationValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owningbusinessunit_value")]
        public string _owningbusinessunitValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificateholder")]
        public string BcgovCertificateholder { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public long? Versionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_ownerid_value")]
        public string _owneridValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_businessaddresscountry")]
        public string BcgovBusinessaddresscountry { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_bcgov_certificatetype_value")]
        public string _bcgovCertificatetypeValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_approvedintendeduse")]
        public string BcgovApprovedintendeduse { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_businessaddressprovince")]
        public string BcgovBusinessaddressprovince { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_issueddate")]
        public System.DateTimeOffset? BcgovIssueddate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owninguser_value")]
        public string _owninguserValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "importsequencenumber")]
        public int? Importsequencenumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_name")]
        public string BcgovName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_expirydatelongdatestring")]
        public string BcgovExpirydatelongdatestring { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_businessaddresscity")]
        public string BcgovBusinessaddresscity { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_businessaddresspostalcode")]
        public string BcgovBusinessaddresspostalcode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificateid")]
        public string BcgovCertificateid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_issueddatelongdatestring")]
        public string BcgovIssueddatelongdatestring { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statecode")]
        public int? Statecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statuscode")]
        public int? Statuscode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_expirydate")]
        public System.DateTimeOffset? BcgovExpirydate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owningteam_value")]
        public string _owningteamValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_businessaddressstreet")]
        public string BcgovBusinessaddressstreet { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_approvedproductcategory")]
        public int? BcgovApprovedproductcategory { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_approvedproductsubcategory")]
        public int? BcgovApprovedproductsubcategory { get; set; }

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
        [JsonProperty(PropertyName = "bcgov_CertificateType")]
        public MicrosoftDynamicsCRMbcgovApplicationtype BcgovCertificateType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificate_bcgov_applicationterms_Certificate")]
        public IList<MicrosoftDynamicsCRMbcgovApplicationterms> BcgovCertificateBcgovApplicationtermsCertificate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_Application")]
        public MicrosoftDynamicsCRMincident BcgovApplication { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bcgov_certificate_bcgov_certificatetermsandconditions_Certificate")]
        public IList<MicrosoftDynamicsCRMbcgovCertificatetermsandconditions> BcgovCertificateBcgovCertificatetermsandconditionsCertificate { get; set; }

    }
}
