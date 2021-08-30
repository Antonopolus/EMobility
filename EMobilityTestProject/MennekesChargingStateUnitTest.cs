using EMobility;
using EMobility.Mennekes;
using System;
using System.Threading;
using Xunit;

namespace EMobilityTestProject
{
    public class MennekesChargingStateUnitTest
    {
        [Fact]
        public void Test()
        {
            MennekesVehicleConnection mennekesVehicleConnection = new();
            Assert.Equal(VehicleConnectionState.UNKNOWN, mennekesVehicleConnection.State);
        }

        [Fact]
        public void TestNoVehicleConnected()
        {
            MennekesVehicleConnection MennekesVehicleConnection = new();
            MennekesVehicleConnection.CheckState("no_vehicle_connected");
            Assert.Equal(VehicleConnectionState.FREE, MennekesVehicleConnection.State);
        }

        [Fact]
        public void TestError()
        {
            MennekesVehicleConnection MennekesVehicleConnection = new();
            MennekesVehicleConnection.CheckState("vehicle_connector_error");
            Assert.Equal(VehicleConnectionState.ERROR, MennekesVehicleConnection.State);
        }

        [Theory]
        [InlineData("vehicle_connected_schuko")]
        [InlineData("vehicle_connected_type2")]
        public void TestTheoryConnected(string state)
        {
            MennekesVehicleConnection MennekesVehicleConnection = new();
            MennekesVehicleConnection.CheckState(state);
            Assert.Equal(VehicleConnectionState.CONNECTED, MennekesVehicleConnection.State);
        }

        [Theory]
        [InlineData("vehicle_charging_schuko")]
        [InlineData("vehicle_charging_type2")]
        public void TestTheoryCharging(string state)
        {
            MennekesVehicleConnection MennekesVehicleConnection = new();
            MennekesVehicleConnection.CheckState(state);
            Assert.Equal(VehicleConnectionState.CHARGING, MennekesVehicleConnection.State);
        }

        [Theory]
        [InlineData("vehicle_connected_schuko")]
        [InlineData("vehicle_charging_schuko")]
        public void TestTheoryTypeSchuko(string state)
        {
            MennekesVehicleConnection MennekesVehicleConnection = new();
            MennekesVehicleConnection.CheckState(state);
            Assert.Equal(VehicleConnectionType.SCHUKO, MennekesVehicleConnection.ConnectedType);
        }

        [Theory]
        [InlineData("vehicle_connected_type2")]
        [InlineData("vehicle_charging_type2")]
        public void TestTheoryType2(string state)
        {
            MennekesVehicleConnection MennekesVehicleConnection = new();
            MennekesVehicleConnection.CheckState(state);
            Assert.Equal(VehicleConnectionType.TYPE2, MennekesVehicleConnection.ConnectedType);
        }
    }
}
