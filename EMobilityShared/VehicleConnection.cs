using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility
{
    public  enum VehicleConnectionState { UNKNOWN, FREE, CONNECTED, CHARGING, ERROR };
    enum VehicleConnectionType { NONE, TYPE2, SCHUKO }

    public enum VehicleConnectionStateChanges { UNDEFINED, START_CHARGING, STOP_CHARGING, END_CHARGING, ON_ERROR };


    public class VehicleConnection 
    {
        public VehicleConnectionState State { get; set; }

        public VehicleConnectionStateChanges LastStateChange = VehicleConnectionStateChanges.UNDEFINED;
        private VehicleConnectionState PreviousState { get; set; }

        internal VehicleConnectionType ConnectedType = VehicleConnectionType.NONE;

        string CurrentStateString = String.Empty;

        public List<StateChangedHandler> StateChangedHandler = new();

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
                LastStateChange = CheckChange();
                return true;
            }
        }

        public virtual VehicleConnectionState CheckState(string newState)
        {
            return VehicleConnectionState.UNKNOWN;
        }

        public virtual VehicleConnectionStateChanges CheckChange()
        {
            if(State == VehicleConnectionState.ERROR)
                return VehicleConnectionStateChanges.ON_ERROR;

            if (State == VehicleConnectionState.CHARGING)
                return VehicleConnectionStateChanges.START_CHARGING;

            if (PreviousState == VehicleConnectionState.CHARGING && State == VehicleConnectionState.CONNECTED)
                return VehicleConnectionStateChanges.STOP_CHARGING;

            if (PreviousState == VehicleConnectionState.CHARGING && State == VehicleConnectionState.FREE)
                return VehicleConnectionStateChanges.STOP_CHARGING;


            return VehicleConnectionStateChanges.UNDEFINED;
        }

        internal virtual bool HasChargingSessionEnded()
        {
            if (PreviousState == VehicleConnectionState.CHARGING) return true;

            return false;
        }

        

    }

    public class StateChangedHandler
    {
        public Action<ChargingPointManager.ChargerConnection> ?ExecuteChangetask { get; set; }

        public VehicleConnectionState Previous { get; set; }
        public VehicleConnectionState State { get; set; }

    }
}
