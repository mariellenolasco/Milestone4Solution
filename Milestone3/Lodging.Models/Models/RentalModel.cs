using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lodging.Models
{
    /// <summary>
    /// Model for the rentals found in a lodging and its rental units
    /// </summary>
    public class RentalModel : IValidatableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Total number of occupants
        /// </summary>
        public int Occupancy { get; set; }

        /// <summary>
        /// type of site
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// booking status, one of:
        ///    - available(neither booked nor currently in use)
        ///    - booked(booked by someone else, but not in use)
        ///    - occupied(currently in use)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Nightly cost
        /// </summary>
        public double Price { get; set; }

        public int? LodgingId { get; set; }

        public LodgingModel Lodging { get; set; }
        /// <summary>
        /// This is the object's validate method, returns all validation errors in a list that the end user can go through
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IEnumerable<ValidationResult> validationResults = new List<ValidationResult>();
            Validation validator = new Validation();

            var isNameValid = validator.ValidateString(Name);
            var isPriceValid = validator.ValidateDigit(Price);
            var isOccupancyValid = validator.ValidateDigit(Occupancy);
            var isTypeValid = validator.ValidateString(Type);

            if (isNameValid != null) validationResults = validationResults.Append(new ValidationResult(isNameValid));
            if (isPriceValid != null) validationResults = validationResults.Append(new ValidationResult(isPriceValid));
            if (isOccupancyValid != null) validationResults = validationResults.Append(new ValidationResult(isOccupancyValid));
            if (isTypeValid != null) validationResults = validationResults.Append(new ValidationResult(isTypeValid));
            if (!Regex.IsMatch(Status.ToLower(), "\\A(occupied||booked||available)\\z")) validationResults = validationResults.Append(new ValidationResult("Invalid status"));


            return validationResults;
        }
    }
}