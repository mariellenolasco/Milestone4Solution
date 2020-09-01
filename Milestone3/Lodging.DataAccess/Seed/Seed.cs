using System;
using System.Collections.Generic;
using Lodging.Models;

namespace Lodging.DataAccess.Seed
{
  public static class Seed
  {
    public static void SeedDatabase(LodgingContext context)
    {
      var lodging1 = new LodgingModel()
      {
        Id = 1,
        Name = "Campsite 1",
        Bathrooms = 3,
        Location = new LocationModel()
        {
          Id = 1,
          Address = new AddressModel()
          {
            Id = 1,
            Street = "77 Woodsgate Sq",
            City = "Baguio",
            Country = "Philippines",
            StateProvince = "Benguet",
            PostalCode = "2600",
            LocationId = 1
          },
          Latitude = "16.4023° N",
          Longitude = "120.5960° E",
          Locale = "en"
        },
        Rentals = new List<RentalModel>()
        {
            new RentalModel
            {
                Id = 1,
                Status = "available",
                Name = "Baguio House",
                Occupancy = 6,
                Price = 100.00,
                Type = "home",
                LodgingId = 1
            },
            new RentalModel
            {
                Id = 2,
                Status = "available",
                Name = "Baguio Cabin",
                Occupancy = 4,
                Price = 100.00,
                Type = "cabin",
                LodgingId = 1
            },
            new RentalModel
            {
                Id = 3,
                Status = "booked",
                Name = "Waterfront cabin",
                Occupancy = 2,
                Price = 100.00,
                Type = "cabin",
                LodgingId = 1
            },
            new RentalModel
            {
                Id = 4,
                Status = "available",
                Name = "Boat House",
                Occupancy = 8,
                Price = 100.00,
                Type = "home",
                LodgingId = 1
            }
        },
        Reviews = new List<ReviewModel>()
        {
            new ReviewModel
          {
            Id = 1,
            AccountId = 1,
            Comment = "I love it here",
            Rating = 10,
            DateCreated = DateTime.Now,
            LodgingId = 1
          },
          new ReviewModel
          {
            Id = 2,
            AccountId = 1,
            Comment = "The flora and fauna is beautiful",
            Rating = 10,
            DateCreated = DateTime.Now,
            LodgingId = 1
          },
          new ReviewModel
          {
            Id = 3,
            AccountId = 1,
            Comment = "Nice houses",
            Rating = 10,
            DateCreated = DateTime.Now,
            LodgingId = 1
          },
          new ReviewModel
          {
            Id = 4,
            AccountId = 1,
            Comment = "Water is warm",
            Rating = 10,
            DateCreated = DateTime.Now,
            LodgingId = 1
          }
        }
      };
      context.Add(lodging1);
      context.SaveChanges();
    }
  }
}