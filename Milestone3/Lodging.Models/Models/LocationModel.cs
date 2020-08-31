using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lodging.Models
{
    /// <summary>
    /// Model for describing the location of a lodging
    /// </summary>
    public class LocationModel : IValidatableObject
    {
        public int Id { get; set; }

        public AddressModel Address { get; set; }

        public string Latitude { get; set; }

        public string Locale { get; set; }

        public string Longitude { get; set; }

        /// <summary>
        /// This is the object's validate method, returns all validation errors in a list that the end user can go through
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IEnumerable<ValidationResult> validationResults = new List<ValidationResult>();

            var isAddressValid = Address.Validate(new ValidationContext(Address));

            if (!(Regex.IsMatch(Latitude, "\\d+\\.\\d+\\s[N,S]{1}"))) validationResults = validationResults.Append(new ValidationResult("Invalid Latitude"));
            if (!(Regex.IsMatch(Longitude, "\\d+\\.\\d+\\s[E,W]{1}"))) validationResults = validationResults.Append(new ValidationResult("Invalid Longitude"));
            if (!(Regex.IsMatch(Locale, "[a-zA-z]{2}"))) validationResults = validationResults.Append(new ValidationResult("Invalid locale"));
            if (isAddressValid != null) validationResults = validationResults.Concat(isAddressValid);

            return validationResults;
        }
    }
}