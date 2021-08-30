using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility
{
    enum VehicleConnectionState { UNKNOWN, FREE, CONNECTED, CHARGING, ERROR };
    enum VehicleConnectionType { NONE, TYPE2, SCHUKO }

    class VehicleConnection
    {
        public VehicleConnectionState State { get; internal set; }
        private VehicleConnectionState PreviousState { get; set; }

        internal VehicleConnectionType ConnectedType = VehicleConnectionType.NONE;

        string CurrentStateString = String.Empty;

        public bool HasNewState(string newState)
        {
            if (newState.Equals(CurrentStateString))
            {
                return false;
            }
            else
            {
                PreviousState = State;
                CurrentStateString = newState;
                State = CheckState(newState);
                return true;
            }
        }

        internal virtual VehicleConnectionState CheckState(string newState)
        {
            return VehicleConnectionState.UNKNOWN;
        }

        internal virtual bool HasChargingSessionEnded()
        {
            if (PreviousState == VehicleConnectionState.CHARGING) return true;

            return false;
        }
    }
}
