using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EMobility.Data.IntegrationTest
{
    public class ChargingPointTest : IClassFixture<DatabaseFixtureInMemory>
    {
        private readonly DatabaseFixture fixture;

        public ChargingPointTest(DatabaseFixtureInMemory fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        public async Task DataTestSimple()
        {
            // --- arrange ---
            var cp = CreateChargingPoint("CP 4");

            // --- act ---
            var result = fixture.Context.ChargingPoints.Add(cp);
            await fixture.Context.SaveChangesAsync();

            // --- assert ---
            Assert.NotNull(result);
        }

        private static ChargingPoint CreateChargingPoint(string name)
        {
            var cp = new ChargingPoint();
            cp.Id = 0;
            cp.Name = name;
            cp.ChargingPointId = $"{name}-{cp.Id}";
            cp.RestUrl = "https://localhost:5001/";
            return cp;
        }
    }
}
