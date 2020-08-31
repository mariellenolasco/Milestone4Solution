using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lodging.Models
{
    /// <summary>
    /// Model for the Lodging and all its details
    /// </summary>
    public class LodgingModel : IValidatableObject
    {
        public int Id { get; set; }

        public LocationModel Location { get; set; }

        public string Name { get; set; }

        public int Bathrooms { get; set; }
        public IEnumerable<RentalModel> Rentals { get; set; }

        public IEnumerable<ReviewModel> Reviews { get; set; }

        /// <summary>
        /// This is the object's validate method, returns all validation errors in a list that the end user can go through
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IEnumerable<ValidationResult> validationResults = new List<ValidationResult>();
            Validation validator = new Validation();

            var isValidName = validator.ValidateString(Name);
            var isValidBathroomCount = validator.ValidateDigit(Bathrooms);

            if (isValidName != null) validationResults = validationResults.Append(new ValidationResult(isValidName));
            if (isValidBathroomCount != null) validationResults = validationResults.Append(new ValidationResult(isValidBathroomCount));
            validationResults.Concat(Location.Validate(new ValidationContext(Location)));
            return validationResults;
        }
    }
}