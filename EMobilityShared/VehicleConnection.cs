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

        string CurrentStateString = String.Empty;

        public bool HasNewState(string newState)
        {
            if (newState.Equals(CurrentStateString))
            {
                return false;
            }
            else
            {
                CurrentStateString = newState;
                State = CheckState(newState);
                return true;
            }
        }

        internal virtual VehicleConnectionState CheckState(string newState)
        {
            return VehicleConnectionState.UNKNOWN;
        }

    }
}
