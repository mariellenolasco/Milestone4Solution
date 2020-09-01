using Lodging.DataAccess.Repositories;
using Lodging.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lodging.DataAccess.Tests
{
  public class RepositoryTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<LodgingContext> _options = new DbContextOptionsBuilder<LodgingContext>().UseSqlite(_connection).Options;
    private const int TestId = 500;
    public static readonly IEnumerable<object[]> _records = new List<object[]>()
        {
          new object[]
          {
            new LodgingModel() { Id = TestId },
            new RentalModel() { Id = TestId },
            new ReviewModel() { Id = TestId }
          }
        };

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_Repository_DeleteAsync(LodgingModel lodging, RentalModel rental, ReviewModel review)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          await ctx.Lodging.AddAsync(lodging);
          await ctx.Rentals.AddAsync(rental);
          await ctx.Reviews.AddAsync(review);
          await ctx.SaveChangesAsync();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new Repository<LodgingModel>(ctx);

          await lodgings.DeleteAsync(TestId);
          await ctx.SaveChangesAsync();

          var result = await ctx.Lodging.Select(x => x.Id == TestId).ToListAsync();
          Assert.Empty(result);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var rentals = new Repository<RentalModel>(ctx);

          await rentals.DeleteAsync(TestId);
          await ctx.SaveChangesAsync();

          var result = await ctx.Rentals.Select(x => x.Id == TestId).ToListAsync();
          Assert.Empty(result);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var reviews = new Repository<ReviewModel>(ctx);

          await reviews.DeleteAsync(TestId);
          await ctx.SaveChangesAsync();

          var result = await ctx.Reviews.Select(x => x.Id == TestId).ToListAsync();
          Assert.Empty(result);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_Repository_InsertAsync(LodgingModel lodging, RentalModel rental, ReviewModel review)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new Repository<LodgingModel>(ctx);

          await lodgings.InsertAsync(lodging);
          await ctx.SaveChangesAsync();

          Assert.NotEmpty(await ctx.Lodging.ToListAsync());
        }

        using (var ctx = new LodgingContext(_options))
        {
          var rentals = new Repository<RentalModel>(ctx);

          await rentals.InsertAsync(rental);
          await ctx.SaveChangesAsync();

          Assert.NotEmpty(await ctx.Rentals.ToListAsync());
        }

        using (var ctx = new LodgingContext(_options))
        {
          var reviews = new Repository<ReviewModel>(ctx);

          await reviews.InsertAsync(review);
          await ctx.SaveChangesAsync();

          Assert.NotEmpty(await ctx.Reviews.ToListAsync());
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new Repository<LodgingModel>(ctx);

          var actual = await lodgings.SelectAsync();

          Assert.Empty(actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var rentals = new Repository<RentalModel>(ctx);

          var actual = await rentals.SelectAsync();

          Assert.Empty(actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var reviews = new Repository<ReviewModel>(ctx);

          var actual = await reviews.SelectAsync();

          Assert.Empty(actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new Repository<LodgingModel>(ctx);

          var actual = await lodgings.SelectAsync(TestId);

          Assert.Null(actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var rentals = new Repository<RentalModel>(ctx);

          var actual = await rentals.SelectAsync(TestId);

          Assert.Null(actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var reviews = new Repository<ReviewModel>(ctx);

          var actual = await reviews.SelectAsync(TestId);

          Assert.Null(actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_Repository_Update(LodgingModel lodging, RentalModel rental, ReviewModel review)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          await ctx.Lodging.AddAsync(lodging);
          await ctx.Rentals.AddAsync(rental);
          await ctx.Reviews.AddAsync(review);
          await ctx.SaveChangesAsync();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new Repository<LodgingModel>(ctx);
          var expected = await ctx.Lodging.FirstAsync();

          expected.Name = "name";
          lodgings.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Lodging.FirstAsync();

          Assert.Equal(expected, actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var rentals = new Repository<RentalModel>(ctx);
          var expected = await ctx.Rentals.FirstAsync();

          expected.Name = "name";
          rentals.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Rentals.FirstAsync();

          Assert.Equal(expected, actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var reviews = new Repository<ReviewModel>(ctx);
          var expected = await ctx.Reviews.FirstAsync();

          expected.Comment = "comment";
          reviews.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Reviews.FirstAsync();

          Assert.Equal(expected, actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }
  }
}