using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


            //Log.Logger = new LoggerConfiguration()
            //     .MinimumLevel.Debug()
            //     .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            //     .Enrich.FromLogContext()
            //     .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            //     .WriteTo.Console()
            //     .CreateLogger();

            try
            {
                Log.Information("Service Starting Up");
                CreateHostBuilder(args)
                 //   .UseSerilog()           // here we use it
                    .Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "There was a problem running the service!");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
