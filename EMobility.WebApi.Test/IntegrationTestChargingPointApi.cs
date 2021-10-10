using AutoFixture;
using EMobility.Data;
using EMobility.WebApplication;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;
using System.Net.Http;

namespace EMobility.WebApi.Test
{
    public class IntegrationTestChargingPointApi : IClassFixture<IntegrationTestAppFactory<Startup>>
    {
        //private readonly Fixture fixture;
        private readonly WireMockServer WiremockServer;

        public IntegrationTestChargingPointApi(IntegrationTestAppFactory<Startup> factory)
        {
            Factory = factory;
            WiremockServer = factory.Services.GetRequiredService<WireMockServer>();
        }

        public IntegrationTestAppFactory<Startup> Factory { get; }

        [Fact]
        public async Task ChargingPoint_GetAll_Returns5DefaultChargingPoints()
        {
            // Arrange ---------------------------------------------------------------------------
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var client = Factory.CreateClient();

            // Act -------------------------------------------------------------------------------
            var response = await client.GetAsync("/api/ChargingPoint");
            var content = await response.Content.ReadAsStringAsync();
            List<ChargingPoint> cps = JsonSerializer.Deserialize<List<ChargingPoint>>(content, options);

            // Assert ---------------------------------------------------------------------------
            Assert.NotNull(response);
            Assert.NotNull(content);
            Assert.NotNull(cps);
            Assert.Equal("TG Stellplatz 4", cps.Where(cp => cp.Id == -4).Select(cp => cp.Name).FirstOrDefault());
            Assert.Equal("TG Stellplatz 5", cps.Where(cp => cp.Id == -5).Select(cp => cp.Name).FirstOrDefault());
            Assert.Equal("Stellplatz 1", cps.Where(cp => cp.Id == 1).Select(cp => cp.Name).FirstOrDefault());
            Assert.Equal("Stellplatz 2", cps.Where(cp => cp.Id == 2).Select(cp => cp.Name).FirstOrDefault());
            Assert.Equal("Stellplatz 3", cps.Where(cp => cp.Id == 3).Select(cp => cp.Name).FirstOrDefault());
            Assert.Equal("Stellplatz 4", cps.Where(cp => cp.Id == 4).Select(cp => cp.Name).FirstOrDefault());
        }


        [Fact]
        public async Task ChargingPoint_GetById_ReturnsCpWithId_2()
        {
            // Arrange ---------------------------------------------------------------------------
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var client = Factory.CreateClient();
            int id = 2;

            // Act -------------------------------------------------------------------------------
            var response = await client.GetAsync($"/api/ChargingPoint/{id}");
            var content = await response.Content.ReadAsStringAsync();
            ChargingPoint cp = JsonSerializer.Deserialize<ChargingPoint>(content, options);

            // Assert ---------------------------------------------------------------------------
            Assert.NotNull(response);
            Assert.NotNull(content);
            Assert.NotNull(cp);
            Assert.Equal("Stellplatz 2", cp.Name);
        }

        [Theory]
        [InlineData(-4, "TG Stellplatz 4")]
        [InlineData(-5, "TG Stellplatz 5")]
        [InlineData(1, "Stellplatz 1")]
        [InlineData(2, "Stellplatz 2")]
        [InlineData(3, "Stellplatz 3")]
        [InlineData(4, "Stellplatz 4")]
        public async Task ChargingPoint_GetById_Theory(int id, string name)
        {
            // Arrange ---------------------------------------------------------------------------
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var client = Factory.CreateClient();

            // Act -------------------------------------------------------------------------------
            var response = await client.GetAsync($"/api/ChargingPoint/{id}");
            var content = await response.Content.ReadAsStringAsync();
            ChargingPoint cp = JsonSerializer.Deserialize<ChargingPoint>(content, options);

            // Assert ---------------------------------------------------------------------------
            Assert.Equal(name, cp.Name);
            Assert.Equal(id, cp.Id);
        }


        [Fact]
        public async Task ChargingPoint_GetAll_Returns()
        {
            //var cp = fixture.Build<ChargingPoint>()
            //    .With(cp => cp.ChargingPointId, "CPID 1")
            //    .With(cp => cp.Id, 1)
            //    .With(cp => cp.Name, "CPID 1")
            //    .With(cp => cp.RestUrl, "CPID 1")
            //    .Create();

            WiremockServer.Given(Request.Create().WithPath("/api").UsingGet())
                .RespondWith(Response.Create().WithBodyAsJson(""));

            // Arrange ---------------------------------------------------------------------------
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var client = Factory.CreateClient();

            // Act -------------------------------------------------------------------------------
//            HttpContent body = new postco
            //var response = await client.PostAsync("/api/ChargingPoint", Htt);
            //var content = await response.Content.ReadAsStringAsync();
            //List<ChargingPoint> cps = JsonSerializer.Deserialize<List<ChargingPoint>>(content, options);

            //// Assert ---------------------------------------------------------------------------
            //Assert.NotNull(response);
            //Assert.NotNull(content);
            //Assert.NotNull(cps);
        }
    }
}
