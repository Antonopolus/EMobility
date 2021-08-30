using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility.Mennekes
{
    /// <summary>
    /// The Mennekes Charger has a REST API.
    /// </summary>
    /// The connection state has the following constants:
    /// static readonly string VEHICLE_CONNECTOR_ERROR = "vehicle_connector_error";
    /// static readonly string NO_VEHICLE_CONNECTED = "no_vehicle_connected";
    /// static readonly string VEHICLE_CONNECTED_SCHUKO = "vehicle_connected_schuko";
    /// static readonly string VEHICLE_CHARGING_SCHUKO = "vehicle_charging_schuko";
    /// static readonly string VEHICLE_CONNECTED_TYPE2 = "vehicle_connected_type2";
    /// static readonly string VEHICLE_CHARGING_TYPE2 = "vehicle_charging_type2";
    /// 
    /// </summary>
    class MennekesVehicleConnection : VehicleConnection
    {
        static readonly string VEHICLE_CONNECTOR_ERROR = "vehicle_connector_error";
        static readonly string NO_VEHICLE_CONNECTED = "no_vehicle_connected";

        static readonly string VEHICLE_CHARGING = "vehicle_charging";
        static readonly string VEHICLE_CONNECTED = "vehicle_connected";

        static readonly string CONNECTOR_TYPE_SCHUKO = "schuko";
        static readonly string CONNECTOR_TYPE_TYPE2 = "type2";

        internal override VehicleConnectionState CheckState(string newState)
        {
            if (newState.Equals(NO_VEHICLE_CONNECTED))
            {
                ConnectedType = VehicleConnectionType.NONE;
                State = VehicleConnectionState.FREE;
            }

            if (newState.Equals(VEHICLE_CONNECTOR_ERROR))
            {
                ConnectedType = VehicleConnectionType.NONE;
                State = VehicleConnectionState.ERROR;
            }

            if (newState.Contains(CONNECTOR_TYPE_SCHUKO))
            {
                ConnectedType = VehicleConnectionType.SCHUKO;
            }

            if (newState.Contains(CONNECTOR_TYPE_TYPE2))
            {
                ConnectedType = VehicleConnectionType.TYPE2;
            }

            if (newState.Contains(VEHICLE_CHARGING))
            {
                State = VehicleConnectionState.CHARGING;
            }

            if (newState.StartsWith(VEHICLE_CONNECTED))
            {
               State = VehicleConnectionState.CONNECTED;
            }

            return State;
        }
    }
}
