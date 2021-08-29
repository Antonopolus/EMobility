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

namespace EMobilityService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        ChargingPointManager? Manager;
        HttpClient? client;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _logger.LogInformation("Worker created");
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            Manager = new ChargingPointManager(client);
            _logger.LogInformation("Worker started");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client?.Dispose();
            _logger.LogInformation("Worker stoped");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if(Manager != null)
                    await Manager.CheckVehicleConnectionStates(stoppingToken);

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
