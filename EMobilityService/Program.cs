using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EMobilityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Service Starting Up");
                CreateHostBuilder(args, configuration)
                    .Build().Run();
            }
            catch (Exception ex)
            {
                // be aware of the diff from .NET5 to .NET6
                // https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/hosting-exception-handling
                // in .NET5 this catch will never happen
                Log.Fatal(ex, "There was a problem running the service!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<EMobilityDbContext>();
                    var test = configuration["ConnectionStrings:DefaultConnection"];
                    var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EMobility;Trusted_Connection=True";
                    optionsBuilder.UseSqlServer(connectionString);
                    services.AddScoped<EMobilityDbContext>(s => new EMobilityDbContext(optionsBuilder.Options));

                    services.AddHostedService<Worker>();
                });
    }
}
