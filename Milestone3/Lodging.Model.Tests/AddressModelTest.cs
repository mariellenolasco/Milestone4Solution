using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Lodging.Models.Tests
{
    public class AddressModelTest
    {
        public static readonly IEnumerable<Object[]> _addresses = new List<Object[]>
        {
          new object[]
          {
            new AddressModel()
            {
              Id = 0,
              City = "city",
              Country = "country",
              PostalCode = "33613",
              StateProvince = "stateprovince",
              Street = "street"
            }
          }
        };

        public static readonly IEnumerable<Object[]> _invalidAddresses = new List<Object[]>
        {
          //invalid city
          new object[]
          {
            new AddressModel()
            {
              Id = 0,
              City = "",
              Country = "testCountry",
              PostalCode = "12345",
              StateProvince = "province",
              Street = "1234 test"
            }
          },
          //invalid country
          new object[]
          {
            new AddressModel()
            {
              Id = 0,
              City = "testCity",
              Country = "",
              PostalCode = "12345",
              StateProvince = "province",
              Street = "1234 test"
            }
          },
          //invalid postal code
          new object[]
          {
            new AddressModel()
            {
              Id = 0,
              City = "testCity",
              Country = "testCountry",
              PostalCode = "",
              StateProvince = "test state",
              Street = "123 test"
            }
          },
          //invalid state/province
          new object[]
          {
            new AddressModel()
            {
              Id = 0,
              City = "testCity",
              Country = "testCountry",
              PostalCode = "12345",
              StateProvince = "",
              Street = "1234 test"
            }
          },
          //invalid street
          new object[]
          {
            new AddressModel()
            {
              Id = 0,
              City = "testcity",
              Country = "testCountry",
              PostalCode = "12345",
              StateProvince = "province",
              Street = ""
            }
          }
        };

        [Theory]
        [MemberData(nameof(_addresses))]
        public void Test_Create_AddressModel(AddressModel address)
        {
            var validationContext = new ValidationContext(address);
            var actual = Validator.TryValidateObject(address, validationContext, null, true);

            Assert.True(actual);
        }

        [Theory]
        [MemberData(nameof(_addresses))]
        public void Test_Validate_AddressModel(AddressModel address)
        {
            var validationContext = new ValidationContext(address);
            Assert.Empty(address.Validate(validationContext));
        }

        [Theory]
        [MemberData(nameof(_invalidAddresses))]
        public void Test_Validate_InvalidAddressModel(AddressModel address)
        {
            var validationContext = new ValidationContext(address);
            Assert.NotEmpty(address.Validate(validationContext));
        }

    }
}