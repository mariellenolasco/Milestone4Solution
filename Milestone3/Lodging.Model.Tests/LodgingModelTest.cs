using Lodging.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Lodging.Models.Tests
{
    public class LodgingModelTest
    {
        public static readonly LocationModel _location = new LocationModel()
        {
            Id = 0,
            Address = new AddressModel()
            {
                Id = 0,
                City = "Baguio City",
                Country = "Philippines",
                PostalCode = "26000",
                StateProvince = "Benguet",
                Street = "77 Woodsgate Sq"
            },
            Latitude = "16.4023 N",
            Locale = "en",
            Longitude = "120.5960 E"
        };
        public static readonly IEnumerable<Object[]> _lodgings = new List<Object[]>
        {
          new object[]
          {
            new LodgingModel()
            {
              Id = 0,
              Location = _location,
              Name = "Camp 7",
              Bathrooms = 5,
              Rentals = new List<RentalModel>(),
              Reviews = new List<ReviewModel>()
            }
          }
        };
        public static readonly IEnumerable<Object[]> _invalidLodgings = new List<Object[]>
        {
        //invalid bathrooms
          new object[]
          {
            new LodgingModel()
            {
              Id = 0,
              Location = _location,
              Name = "Camp 7",
              Bathrooms = -5,
              Rentals = new List<RentalModel>(),
              Reviews = new List<ReviewModel>()
            }
          },
          //invalid Name
          new object[]
          {
            new LodgingModel()
            {
              Id = 0,
              Location = _location,
              Name = "",
              Bathrooms = 5,
              Rentals = new List<RentalModel>(),
              Reviews = new List<ReviewModel>()
            }
          }
        };

        [Theory]
        [MemberData(nameof(_lodgings))]
        public void Test_Create_LodgingModel(LodgingModel lodging)
        {
            var validationContext = new ValidationContext(lodging);
            var actual = Validator.TryValidateObject(lodging, validationContext, null, true);

            Assert.True(actual);
        }

        [Theory]
        [MemberData(nameof(_lodgings))]
        public void Test_Validate_LodgingModel(LodgingModel lodging)
        {
            var validationContext = new ValidationContext(lodging);

            Assert.Empty(lodging.Validate(validationContext));
        }

        [Theory]
        [MemberData(nameof(_invalidLodgings))]
        public void Test_Validate_InvalidLodgingModel(LodgingModel lodging)
        {
            var validationContext = new ValidationContext(lodging);

            Assert.NotEmpty(lodging.Validate(validationContext));
        }
    }
}