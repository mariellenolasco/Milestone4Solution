using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Lodging.Models.Tests
{
    public class RentalModelTest
    {
        public static readonly IEnumerable<Object[]> _rentals = new List<Object[]>
        {
          new object[]
          {
            new RentalModel()
            {
              Id = 0,
              Name = "name",
              Occupancy = 4,
              Type = "camptype",
              Status = "available",
              Price = 20.00
            }
          }
        };

        public static readonly IEnumerable<Object[]> _invalidRentals = new List<Object[]>
        {
        //invalid occupancy
          new object[]
          {
            new RentalModel()
            {
              Id = 0,
              Name = "name",
              Occupancy = -4,
              Type = "camptype",
              Status = "available",
              Price = 20.00
            }
          },
          //invalid name
          new object[]
          {
            new RentalModel()
            {
              Id = 0,
              Name = "",
              Occupancy = 4,
              Type = "camptype",
              Status = "available",
              Price = 20.00
            }
          },
          //invalid type
          new object[]
          {
            new RentalModel()
            {
              Id = 0,
              Name = "name",
              Occupancy = 4,
              Type = "",
              Status = "available",
              Price = 20.00
            }
          },
          //invalid status
          new object[]
          {
            new RentalModel()
            {
              Id = 0,
              Name = "name",
              Occupancy = 4,
              Type = "camptype",
              Status = "not available",
              Price = 20.00
            }
          },
          //invalid price
          new object[]
          {
            new RentalModel()
            {
              Id = 0,
              Name = "name",
              Occupancy = 4,
              Type = "camptype",
              Status = "available",
              Price = -20.00
            }
          }
        };

        [Theory]
        [MemberData(nameof(_rentals))]
        public void Test_Create_RentalModel(RentalModel rental)
        {
            var validationContext = new ValidationContext(rental);
            var actual = Validator.TryValidateObject(rental, validationContext, null, true);

            Assert.True(actual);
        }

        [Theory]
        [MemberData(nameof(_rentals))]
        public void Test_Validate_RentalModel(RentalModel rental)
        {
            var validationContext = new ValidationContext(rental);

            Assert.Empty(rental.Validate(validationContext));
        }

        [Theory]
        [MemberData(nameof(_invalidRentals))]
        public void Test_Validate_InvalidRentalModel(RentalModel rental)
        {
            var validationContext = new ValidationContext(rental);

            Assert.NotEmpty(rental.Validate(validationContext));
        }
    }
}