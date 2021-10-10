using EMobility.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using FluentAssertions;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System;

namespace EMobility.Data.IntegrationTest
{
    public class EMobilityDbContextIntTest
    {
        [Fact]
        public async void ChargingPoints_GetAll_IsEmpty()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<EMobilityDbContext>();
            optionsBuilder.UseInMemoryDatabase(System.Reflection.MethodBase.GetCurrentMethod().Name);
            EMobilityDbContext testContext =new(optionsBuilder.Options);

            // Act
            var chargingPoints = await testContext.ChargingPoints.ToListAsync();

            // Assert
            Assert.NotNull(chargingPoints);
            Assert.Empty(chargingPoints);
           
        }

        [Fact]
        public async void ChargingPoints_GetAll_HasOneItem()
        {
            // Arrange
            EMobilityDbContext testContext = CreateUniqueContext();
            var testChargingPoint = new ChargingPoint(0, "Name", "http://localhost:5001/", "CP ID");
            testContext.ChargingPoints.Add(testChargingPoint);
            testContext.SaveChanges();

            // Act
            var chargingPoints = await testContext.ChargingPoints.ToListAsync();
            var resultCp = chargingPoints.FirstOrDefault();

            // Assert
            Assert.NotNull(chargingPoints);
            Assert.Single(chargingPoints);
            Assert.NotNull(resultCp);

            // fluent asserts
            resultCp.ChargingPointId.Should().Contain(testChargingPoint.ChargingPointId);
            resultCp.Name.Should().Contain(testChargingPoint.Name);
            resultCp.RestUrl.Should().Be(testChargingPoint.RestUrl);
            resultCp.Id.Should().Be(testChargingPoint.Id);
            chargingPoints.Should().NotBeEmpty().And.HaveCount(1);

            // https://fluentassertions.com/
        }

        public async void ChargingSessions_GetAll_IsEmpty()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<EMobilityDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            EMobilityDbContext testContext = new(optionsBuilder.Options);

            // Act
            var chargingSessions = await testContext.ChargingSessions.ToListAsync();

            // Assert
            Assert.NotNull(chargingSessions);
            Assert.Empty(chargingSessions);
        }

        private EMobilityDbContext CreateUniqueContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EMobilityDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            EMobilityDbContext testContext = new(optionsBuilder.Options);
            return testContext;
        }
    }
}
