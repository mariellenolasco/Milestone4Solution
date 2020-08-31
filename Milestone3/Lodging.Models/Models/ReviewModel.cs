using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lodging.Models
{
    /// <summary>
    /// Review model that represents the details of a review
    /// </summary>
    public class ReviewModel : IValidatableObject
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreated { get; set; }

        public int Rating { get; set; }
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

            var isCommentValid = validator.ValidateString(Comment);

            if (DateCreated.CompareTo(DateTime.Now) > 0) validationResults = validationResults.Append(new ValidationResult("Invalid Date. Cannot have a comment created in the future"));
            if (isCommentValid != null) validationResults = validationResults.Append(new ValidationResult(isCommentValid));
            if (Rating < 0) validationResults = validationResults.Append(new ValidationResult("Rating should be nonnegative"));

            return validationResults;
        }
    }
}