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
    public enum AdoxioApplicationStatusCodes
    {
        Active = 1,
        [EnumMember(Value = "In Progress")]
        InProgress = 845280000,
        Intake = 845280001,
        [EnumMember(Value = "Pending for LG/FN/Police Feedback")]
        PendingForLGFNPFeedback = 845280006,
        [EnumMember(Value = "Under Review")]
        UnderReview = 845280003,
        [EnumMember(Value = "Pending for Licence Fee")]
        PendingForLicenceFee = 845280007,        
        Approved = 845280004,
        Denied = 845280005,
        [EnumMember(Value = "Approved in Principle")]
        ApprovedInPrinciple = 845280008,
        Terminated = 845280009
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

        public List<CustomProduct> CustomProducts {get; set;}

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

    }
}
