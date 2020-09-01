using Lodging.Models;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}