using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility.Mennekes
{
    class MennekesVehicleConnection : VehicleConnection
    {
        static readonly string VEHICLE_CONNECTOR_ERROR = "vehicle_connector_error";
        static readonly string NO_VEHICLE_CONNECTED = "no_vehicle_connected";
        //static readonly string VEHICLE_CONNECTED_SCHUKO = "vehicle_connected_schuko";
        //static readonly string VEHICLE_CHARGING_SCHUKO = "vehicle_charging_schuko";
        //static readonly string VEHICLE_CONNECTED_TYPE2 = "vehicle_connected_type2";
        //static readonly string VEHICLE_CHARGING_TYPE2 = "vehicle_charging_type2";

        static readonly string VEHICLE_CHARGING = "vehicle_charging";
        static readonly string VEHICLE_CONNECTED = "vehicle_connected";

        static readonly string CONNECTOR_TYPE_SCHUKO = "schuko";
        static readonly string CONNECTOR_TYPE_TYPE2 = "type2";


        internal VehicleConnectionState CurrentState = VehicleConnectionState.UNKNOWN;
        internal VehicleConnectionType ConnectedType = VehicleConnectionType.NONE;


        internal override VehicleConnectionState CheckState(string state)
        {
            if (state.Equals(NO_VEHICLE_CONNECTED))
            {
                ConnectedType = VehicleConnectionType.NONE;
                CurrentState = VehicleConnectionState.FREE;
            }

            if (state.Equals(VEHICLE_CONNECTOR_ERROR))
            {
                ConnectedType = VehicleConnectionType.NONE;
                CurrentState = VehicleConnectionState.ERROR;
            }

            if (state.Contains(CONNECTOR_TYPE_SCHUKO))
            {
                ConnectedType = VehicleConnectionType.SCHUKO;
            }

            if (state.Contains(CONNECTOR_TYPE_TYPE2))
            {
                ConnectedType = VehicleConnectionType.TYPE2;
            }

            if (state.Contains(VEHICLE_CHARGING))
            {
                CurrentState = VehicleConnectionState.CHARGING;
            }

            if (state.StartsWith(VEHICLE_CONNECTED))
            {
                CurrentState = VehicleConnectionState.CONNECTED;
            }

            State = CurrentState;
            return this.CurrentState;
        }
    }
}
