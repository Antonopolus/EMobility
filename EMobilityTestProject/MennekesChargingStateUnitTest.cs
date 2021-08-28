using EMobility;
using EMobility.Mennekes;
using System;
using Xunit;

namespace EMobilityTestProject
{
    public class MennekesChargingStateUnitTest
    {
        [Fact]
        public void Test()
        {
            MennekesVehicleConnection mennekesVehicleConnection = new();
            Assert.Equal(VehicleConnectionState.UNKNOWN, mennekesVehicleConnection.CurrentState);
        }

        [Fact]
        public void TestNoVehicleConnected()
        {
            MennekesVehicleConnection MennekesVehicleConnection = new();
            MennekesVehicleConnection.CheckState("no_vehicle_connected");
            Assert.Equal(VehicleConnectionState.FREE, MennekesVehicleConnection.CurrentState);
        }

        [Fact]
        public void TestError()
        {
            MennekesVehicleConnection MennekesVehicleConnection = new();
            MennekesVehicleConnection.CheckState("vehicle_connector_error");
            Assert.Equal(VehicleConnectionState.ERROR, MennekesVehicleConnection.CurrentState);
        }

        [Theory]
        [InlineData("vehicle_connected_schuko")]
        [InlineData("vehicle_connected_type2")]
        public void TestTheoryConnected(string state)
        {
            MennekesVehicleConnection MennekesVehicleConnection = new();
            MennekesVehicleConnection.CheckState(state);
            Assert.Equal(VehicleConnectionState.CONNECTED, MennekesVehicleConnection.CurrentState);
        }

        [Theory]
        [InlineData("vehicle_charging_schuko")]
        [InlineData("vehicle_charging_type2")]
        public void TestTheoryCharging(string state)
        {
            MennekesVehicleConnection MennekesVehicleConnection = new();
            MennekesVehicleConnection.CheckState(state);
            Assert.Equal(VehicleConnectionState.CHARGING, MennekesVehicleConnection.CurrentState);
        }

        [Theory]
        [InlineData("vehicle_connected_schuko")]
        [InlineData("vehicle_charging_schuko")]
        public void TestTheoryTypeSchuko(string state)
        {
            MennekesVehicleConnection MennekesVehicleConnection = new();
            MennekesVehicleConnection.CheckState(state);
            //Assert.Equal(Type.SCHUKO, MennekesVehicleConnection.);
        }
    }
}
