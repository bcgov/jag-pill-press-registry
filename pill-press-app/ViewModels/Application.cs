using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Public.ViewModels
{
    public enum ApplicationStatusCodes
    {
        Draft = 931490005,        
        Pending = 931490000,
        [EnumMember(Value = "Under Review")]
        UnderReview = 931490001,
        Incomplete = 931490002,
        [EnumMember(Value = "With Risk Assessment")]
        WithRiskAssessment = 931490007,
        [EnumMember(Value = "With Deputy Registrar")]
        WithDeputyRegistrar = 931490008,
        [EnumMember(Value = "With C&E Investigations")]
        WithCEInvestigations = 931490009,
        Approved = 931490010,
        Hearing = 931490011,
        Denied = 931490012,
        Withdrawn = 931490013,
        Cancelled = 931490014,
        Expired = 931490015
    }

    public enum UserApplicationStatusCodes
    {
        Draft = 931490005,
        Pending = 931490000
    }



    public enum AdoxioFinalDecisionCodes
    {
        Approved = 845280000,
        Denied = 845280001
    }

    public class Application
    {     

        public string id { get; set; } //adoxio_applicationid

        /// <summary>
        /// The related business
        /// </summary>

        public ViewModels.Account applicant { get; set; }

        // #### EQUIPMENT INFORMATION ####

        /// <summary>
        /// Do you currently own, use, or possess Controlled Equipment?
        /// </summary>

        public bool? currentlyownusepossessequipment { get; set; }

        /// <summary>
        ///  do you intend on purchasing Controlled Equipment in the future?
        /// </summary>
        public bool? intendtopurchaseequipment { get; set; }

        /// <summary>
        /// Do you own or intend to own Controlled Equipment for the sole use of your business?
        /// </summary>
        public bool? ownintendtoownequipmentforbusinessuse { get; set; }

        /// <summary>
        /// Do you borrow, rent, or lease Controlled Equipment from someone else?
        /// </summary>
        public bool? borrowrentleaseequipment { get; set; }

        /// <summary>
        /// Do you sell Controlled Equipment to others?
        /// </summary>
        public bool? sellequipment { get; set; }

        // ### PURPOSE OF CONTROLLED EQUIPMENT ###

        /// <summary>
        /// Do you own, use, or possess (or intend to own) Controlled Equipment for the purposes of producing your own products?
        /// </summary>
        public bool? producingownproduct { get; set; }

        /// <summary>
        /// Do you own, use, or possess (or intend to own) Controlled Equipment for the purposes of providing manufacturing services to others?
        /// </summary>
        public bool? providingmanufacturingtoothers { get; set; }

        /// <summary>
        ///  provide detailed information on the types of products you produce for others and their intended uses.
        /// </summary>

        public List<CustomProduct> CustomProducts { get; set; }

        // ### BUSINESS DETAILS ###

        /// <summary>
        /// Please explain the main focus of your business and why that requires Controlled Equipment.
        /// </summary>
        public string mainbusinessfocus { get; set; }

        /// <summary>
        /// Please describe the manufacturing process you use to produce the above-noted products. Please include specific information on how you utilize the Controlled Equipment throughout the manufacturing process.
        /// </summary>
        public string manufacturingprocessdescription { get; set; }

        // ### DECLARATIONS AND CONSENT ###
        /// <summary>
        /// Declaration that all information provided is correct, including the information on the Client Profile - Business Information page (which is incorporated into this application)
        /// </summary>
        public bool? declarationofcorrectinformation { get; set; }

        /// <summary>
        /// Consent that by submitting the application the applicant understands their information is being collected for FOIPPA purposes and may be released as per FOIPPA.
        /// </summary>
        public bool? foippaconsent { get; set; }



        public bool? foodanddrugact { get; set; }
        public string legislativeauthorityother { get; set; }
        public bool? kindsofproductsdrugs { get; set; }
        public bool? kindsofproductsnaturalhealthproducts { get; set; }
        public string kindsofproductsother { get; set; }
        public bool? drugestablishmentlicence { get; set; }
        public bool? sitelicence { get; set; }
        public string otherlicence { get; set; }
        public string delbusinessname { get; set; }
        public string drugestablishmentlicencenumber { get; set; }
        public DateTimeOffset? drugestablishmentlicenceexpirydate { get; set; }
        public string sitelicencebusinessname { get; set; }
        public string sitelicencenumber { get; set; }
        public DateTimeOffset? sitelicenceexpirydate { get; set; }
        public string otherlicencebusinessname { get; set; }
        public string otherlicencenumber { get; set; }
        public DateTimeOffset? otherlicenceexpirydate { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApplicationStatusCodes statuscode { get; set; }
        public string applicationtype { get; set; }

    }
}
