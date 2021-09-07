using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EMobility;
using Microsoft.Extensions.DependencyInjection;

namespace EMobilityService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        IChargingPointManager? Manager;
        IChargingPointManager? DemoManager;
        HttpClient? client;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _logger.LogInformation("Worker created ************");
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            Manager = new ChargingPointManager(client);
            DemoManager = new DemoChargingPointManager(client);
            _logger.LogInformation("Worker started ************");

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client?.Dispose();
            _logger.LogInformation("Worker stoped ************");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<EMobilityDbContext>();

                var newCp = new ChargingPoint(0, "Name", @"http//heise.de/", "cpId");
                dbContext.Add(newCp);
                await dbContext.SaveChangesAsync();

                Log.Information("new cp added: {cp}" , newCp.ToString());
                //if (Manager != null)
                //    await Manager.CheckVehicleConnectionStatesAsync(stoppingToken);

                //if (DemoManager != null)
                //    await DemoManager.CheckVehicleConnectionStatesAsync(stoppingToken);


                //dbContext.Tests.Add(new Test() { Date = DateTime.Now });
                //dbContext.SaveChanges();

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
