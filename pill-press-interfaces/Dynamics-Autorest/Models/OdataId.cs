namespace Gov.Jag.PillPressRegistry.Interfaces
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;
     public partial class OdataId
    {
        /// <summary>
        /// Initializes a new instance of the OdataId class.
        /// </summary>
        public OdataId()
        {
            CustomInit();
        }
         /// <summary>
        /// Initializes a new instance of the OdataId class.
        /// </summary>
        public OdataId(string OdataIdProperty)
        {
            this.OdataIdProperty = OdataIdProperty;
            CustomInit();
        }
         /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();
         /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "@odata.id")]
        public string OdataIdProperty { get; set; }
         /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (OdataIdProperty == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "OdataIdProperty");
            }
        }
    }
}
