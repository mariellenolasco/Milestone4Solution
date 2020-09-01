using Lodging.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lodging.DataAccess.Seed;
using System;

namespace Lodging.WebAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<LodgingContext>();
        context.Database.EnsureCreated();
        try
        {
          Seed.SeedDatabase(context);
        }
        catch (Exception ex)
        {
          Console.WriteLine("An error occurred seeding the database with test messages. Error: {0}", ex.Message);
        }
      }
      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}