using Lodging.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Lodging.Models.Tests
{
    public class LocationModelTest
    {
        public static readonly IEnumerable<Object[]> _locations = new List<Object[]>
        {
          new object[]
          {
            new LocationModel()
            {
              Id = 0,
              Address = new AddressModel()
                {
                  Id = 0,
                  City = "city",
                  Country = "country",
                  PostalCode = "33613",
                  StateProvince = "stateprovince",
                  Street = "street"
                },
              Latitude = "16.4023 N",
              Locale = "en",
              Longitude = "120.5960 E"
            }
          }
        };
        public static readonly IEnumerable<Object[]> _invalidLocations = new List<Object[]>
        {
        //invalid latitude
          new object[]
          {
            new LocationModel()
            {
              Id = 0,
              Address = new AddressModel()
                {
                  Id = 0,
                  City = "city",
                  Country = "country",
                  PostalCode = "33613",
                  StateProvince = "stateprovince",
                  Street = "street"
                },
              Latitude = "",
              Locale = "en",
              Longitude = "120.5960 E"
            }
          },
          //invalid latitude
          new object[]
          {
            new LocationModel()
            {
              Id = 0,
              Address = new AddressModel()
                {
                  Id = 0,
                  City = "city",
                  Country = "country",
                  PostalCode = "33613",
                  StateProvince = "stateprovince",
                  Street = "street"
                },
              Latitude = "120.5960 E",
              Locale = "en",
              Longitude = "120.5960 E"
            }
          },
          //invalid longitude
          new object[]
          {
            new LocationModel()
            {
              Id = 0,
              Address = new AddressModel()
                {
                  Id = 0,
                  City = "city",
                  Country = "country",
                  PostalCode = "33613",
                  StateProvince = "stateprovince",
                  Street = "street"
                },
              Latitude = "16.4023 N",
              Locale = "en",
              Longitude = ""
            }
          },
           //invalid locale
          new object[]
          {
            new LocationModel()
            {
              Id = 0,
              Address = new AddressModel()
                {
                  Id = 0,
                  City = "city",
                  Country = "country",
                  PostalCode = "33613",
                  StateProvince = "stateprovince",
                  Street = "street"
                },
              Latitude = "16.4023 N",
              Locale = "",
              Longitude = "120.5960 E"
            }
          }
        };

        [Theory]
        [MemberData(nameof(_locations))]
        public void Test_Create_LocationModel(LocationModel location)
        {
            var validationContext = new ValidationContext(location);
            var actual = Validator.TryValidateObject(location, validationContext, null, true);

            Assert.True(actual);
        }

        [Theory]
        [MemberData(nameof(_locations))]
        public void Test_Validate_LocationModel(LocationModel location)
        {
            var validationContext = new ValidationContext(location);

            Assert.Empty(location.Validate(validationContext));
        }

        [Theory]
        [MemberData(nameof(_invalidLocations))]
        public void Test_Validate_InvalidLocationModel(LocationModel location)
        {
            var validationContext = new ValidationContext(location);

            Assert.NotEmpty(location.Validate(validationContext));
        }
    }
}