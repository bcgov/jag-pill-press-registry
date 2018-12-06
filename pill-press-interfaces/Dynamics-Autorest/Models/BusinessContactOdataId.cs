namespace Gov.Jag.PillPressRegistry.Interfaces
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;
     public partial class BusinessContactOdataId
    {
        /// <summary>
        /// Initializes a new instance of the OdataId class.
        /// </summary>
        public BusinessContactOdataId()
        {
            CustomInit();
        }
         /// <summary>
        /// Initializes a new instance of the OdataId class.
        /// </summary>
        public BusinessContactOdataId(string OdataIdProperty)
        {
            this.OdataIdProperty = OdataIdProperty;

            CustomInit();
        }
         /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();
        
       // [JsonProperty(PropertyName = "participationtypemask")]
       // public int ParticipationTypeMask { get; set; }

        /// <summary>
        /// </summary>        
        [JsonProperty(PropertyName = "bcgov_businesscontact@odata.id")]
        public string OdataIdProperty { get; set; }
         /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        /// 
        public virtual void Validate()
        {
            if (OdataIdProperty == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "OdataIdProperty");
            }
        }
    }
}
