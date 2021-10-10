using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WireMock.Server;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

using Microsoft.Extensions.Hosting;

namespace EMobility.WebApi.Test
{
    public class IntegrationTestAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //base.ConfigureWebHost(builder);
            var wiremockServer = WireMockServer.Start();
            var url = wiremockServer.Urls[0];

            builder.ConfigureAppConfiguration(configurationBuilder => {
                configurationBuilder.AddInMemoryCollection(new KeyValuePair<string, string>[] {
                    new KeyValuePair<string, string>("server", url)
                });
            }).ConfigureServices(collection => {
                collection.AddSingleton(wiremockServer);
            });
        }
    }
}
