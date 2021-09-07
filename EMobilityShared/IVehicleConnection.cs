namespace EMobility
{
    public interface IVehicleConnection
    {
        public VehicleConnectionState State { get; set; }

        public bool HasNewState(string newState);

        public bool HasChargingSessionEnded();
    }
}