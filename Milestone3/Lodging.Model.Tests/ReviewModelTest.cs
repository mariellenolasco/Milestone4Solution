using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Lodging.Models.Tests
{
    public class ReviewModelTest
    {
        public static readonly IEnumerable<Object[]> _reviews = new List<Object[]>
        {
          new object[]
          {
            new ReviewModel()
            {
              Id = 0,
              AccountId = 0,
              Comment = "comment",
              DateCreated = DateTime.Now,
              Rating = 0
            }
          }
        };
        public static readonly IEnumerable<Object[]> _invalidReviews = new List<Object[]>
        {
          new object[]
          {
            //invalid comment
            new ReviewModel()
            {
              Id = 0,
              AccountId = 0,
              Comment = "",
              DateCreated = DateTime.Now,
              Rating = 1
            }
          },
          new object[]
          {
            //invalid date
            new ReviewModel()
            {
              Id = 0,
              AccountId = 0,
              Comment = "I love it here",
              DateCreated = DateTime.Now.AddDays(3.0),
              Rating = 1
            }
          },
          new object[]
          {
            //invalid rating
            new ReviewModel()
            {
              Id = 0,
              AccountId = 0,
              Comment = "I love it here",
              DateCreated = DateTime.Now,
              Rating = -1
            }
          }
        };
        [Theory]
        [MemberData(nameof(_reviews))]
        public void Test_Create_ReviewModel(ReviewModel review)
        {
            var validationContext = new ValidationContext(review);
            var actual = Validator.TryValidateObject(review, validationContext, null, true);

            Assert.True(actual);
        }

        [Theory]
        [MemberData(nameof(_reviews))]
        public void Test_Validate_ReviewModel(ReviewModel review)
        {
            var validationContext = new ValidationContext(review);

            Assert.Empty(review.Validate(validationContext));
        }

        [Theory]
        [MemberData(nameof(_invalidReviews))]
        public void Test_Validate_InvalidReviewModel(ReviewModel review)
        {
            var validationContext = new ValidationContext(review);

            Assert.NotEmpty(review.Validate(validationContext));
        }
    }
}