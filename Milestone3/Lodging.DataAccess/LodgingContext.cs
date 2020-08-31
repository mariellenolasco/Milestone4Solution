using Lodging.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lodging.DataAccess
{
  public class LodgingContext : DbContext
  {
    public DbSet<LodgingModel> Lodging { get; set; }
    public DbSet<RentalModel> Rentals { get; set; }
    public DbSet<ReviewModel> Reviews { get; set; }

    public LodgingContext(DbContextOptions<LodgingContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<AddressModel>().HasKey(e => e.Id);
      modelBuilder.Entity<LocationModel>().HasKey(e => e.Id);
      modelBuilder.Entity<LodgingModel>().HasKey(e => e.Id);
      modelBuilder.Entity<RentalModel>().HasKey(e => e.Id);
      modelBuilder.Entity<ReviewModel>().HasKey(e => e.Id);

      modelBuilder.Entity<AddressModel>().HasData(
          new AddressModel
          {
            Id = 1,
            Street = "77 Woodsgate Sq",
            City = "Baguio",
            Country = "Philippines",
            StateProvince = "Benguet",
            PostalCode = "2600",
            LocationId = 1
          }
         );
      modelBuilder.Entity<LocationModel>().HasData(
          new LocationModel
          {
            Id = 1,
            Latitude = "16.4023° N",
            Longitude = "120.5960° E",
            Locale = "en",
          }
         );
      modelBuilder.Entity<RentalModel>().HasData(
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
             LodgingId = 2
           },
           new RentalModel
           {
             Id = 3,
             Status = "booked",
             Name = "Waterfront cabin",
             Occupancy = 2,
             Price = 100.00,
             Type = "cabin",
             LodgingId = 3
           },
           new RentalModel
           {
             Id = 4,
             Status = "available",
             Name = "Boat House",
             Occupancy = 8,
             Price = 100.00,
             Type = "home",
             LodgingId = 4
           }
          );
      modelBuilder.Entity<ReviewModel>().HasData(
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
            LodgingId = 2
          },
          new ReviewModel
          {
            Id = 3,
            AccountId = 1,
            Comment = "Nice houses",
            Rating = 10,
            DateCreated = DateTime.Now,
            LodgingId = 3
          },
          new ReviewModel
          {
            Id = 4,
            AccountId = 1,
            Comment = "Water is warm",
            Rating = 10,
            DateCreated = DateTime.Now,
            LodgingId = 4
          }
         );
      modelBuilder.Entity<LodgingModel>().HasData(
          new LodgingModel
          {
            Id = 1,
            Bathrooms = 3,
            Name = "Camp 7"
          },
          new LodgingModel
          {
            Id = 2,
            Bathrooms = 2,
            Name = "Camp 8"
          },
          new LodgingModel
          {
            Id = 3,
            Bathrooms = 0,
            Name = "Camp 9"
          },
          new LodgingModel
          {
            Id = 4,
            Bathrooms = 4,
            Name = "Camp 4"
          }
         );
    }
  }
}