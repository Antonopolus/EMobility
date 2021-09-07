using EMobility.Mennekes;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EMobility
{
    public class ChargingPointManager : IChargingPointManager
    {
        readonly HttpClient HttpClientConnection;

        CancellationToken CancelationToken;

        public record ChargerConnection(ChargingPoint ChargingPoint, MennekesVehicleConnection Connection);
        readonly List<ChargerConnection> ChargerConnections;

        private record SessionInfo(string StationId, string AuthenticationId, int ElapsedTime, int TotalPower, int Power, int TransactionPower);
        readonly List<SessionInfo> SessionInfos;

        public ChargingPointManager(HttpClient httpClient)
        {
            SessionInfos = new();
            this.HttpClientConnection = httpClient;

            ChargerConnections = new();
            foreach (var cp in EMobilityContext.GetChargingPoints())
            {
                var item = new ChargerConnection(cp, new MennekesVehicleConnection());
                ChargerConnections.Add(item);

                StateChangedHandler handler = new();
                handler.Previous = VehicleConnectionState.CHARGING;
                handler.State = VehicleConnectionState.CONNECTED;
                handler.ExecuteChangetask = HandleChargingEnded;
                item.Connection.StateChangedHandler.Add(handler);
            }
        }

        public void CheckVehicleConnectionStates(CancellationToken cancelationToken)
        {
            this.CancelationToken = cancelationToken;
            Parallel.ForEach<ChargerConnection>(ChargerConnections, async connection =>
            {
                await CheckVehicleConnectionState(connection);
            });
        }

        public async Task CheckVehicleConnectionStatesParallelAsync(CancellationToken cancelationToken)
        {
            this.CancelationToken = cancelationToken;
            await Task.Run(() => {
                CheckVehicleConnectionStates(cancelationToken);
            });
        }

        public async Task CheckVehicleConnectionStatesAsync(CancellationToken cancelationToken)
        {
            this.CancelationToken = cancelationToken;
            var tasks = ChargerConnections.Select(async connection =>
            {
                await CheckVehicleConnectionState(connection);
            });
            await Task.WhenAll(tasks);
        }

        private async Task CheckVehicleConnectionState(ChargerConnection connection)
        {
            var state = await RequestStatus(connection, this.CancelationToken);
            if (connection.Connection.HasNewState(state))
            {
                await HandleNewState(connection, this.CancelationToken);
            }
        }

         private async Task HandleNewState(ChargerConnection chargerConnection, CancellationToken cancelationToken)
        {
            Log.Information("charge point {Name} has new state -> {State}", chargerConnection.ChargingPoint.Name, chargerConnection.Connection.State);

            //ConnectionStateHandler handler = new ConnectionStateHandler();
            //handler.OnNewState(chargingPoint);

            foreach (var item in chargerConnection.Connection.StateChangedHandler)
            {
                if(chargerConnection.Connection.State == item.State && chargerConnection.Connection.State == item.Previous)
                {
                    item.ExecuteChangetask?.Invoke(chargerConnection);
                }
            }

            if (chargerConnection.Connection.State == VehicleConnectionState.CHARGING)
            {
                var info = await RequestInfo(chargerConnection.ChargingPoint, cancelationToken);
                SessionInfos.Add(info);
            }

            if (chargerConnection.Connection.HasChargingSessionEnded())
            {
                Log.Information("charge point {Name} charging ended", chargerConnection.ChargingPoint.Name);
                string fileName = String.Format("{0}_Sessions.json", DateTime.Now.ToLongDateString());
                using FileStream createStream = File.Create(fileName);
                await JsonSerializer.SerializeAsync(createStream, SessionInfos, cancellationToken: cancelationToken);
                await createStream.DisposeAsync();
                SessionInfos.Clear();
            }
        }

        private void HandleChargingEnded(ChargerConnection connection)
        {
           
        }

        private async Task<string> RequestStatus(ChargerConnection chargingConnection, CancellationToken cancelationToken)
        {
            string resultText = await Request(chargingConnection.ChargingPoint, "conn_state", cancelationToken);
            return resultText;
        }

        private async Task<SessionInfo> RequestInfo(ChargingPoint chargingPoint, CancellationToken cancelationToken)
        {
            var stationId = await Request(chargingPoint, "cp_id", cancelationToken);                // station ID
            var authenticationId = await Request(chargingPoint, "auth_uid", cancelationToken);      // RFID of current user
            var duration = await Request(chargingPoint, "time_since_charging_start", cancelationToken);
            var meterWh = await Request(chargingPoint, "meter_wh", cancelationToken);
            var powerW = await Request(chargingPoint, "power_w", cancelationToken);
            var transactionWh = await Request(chargingPoint, "transaction_wh", cancelationToken);

            SessionInfo info = new
            (
                StationId: stationId,
                AuthenticationId: authenticationId,
                ElapsedTime: Int32.Parse(duration),
                TotalPower: Int32.Parse(meterWh),
                Power: Int32.Parse(powerW),
                TransactionPower: Int32.Parse(transactionWh)
            );

            return info;
        }

        private async Task<string> Request(ChargingPoint chargingPoint, string command, CancellationToken cancelationToken)
        {
            string resultText = String.Empty;
            try
            {
                var result = await HttpClientConnection.GetAsync(String.Format("{0}{1}", chargingPoint.RestUrl, command), cancelationToken);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseBody = await result.Content.ReadAsStringAsync(cancelationToken);
                    Log.Debug("[{Name}] Request[{Command}]  response -> {ResponseBody}", chargingPoint.Name, command, responseBody);
                    resultText = responseBody;
                }
                else
                {
                    Log.Error("HttpResponseCode: {0} -> {1}", result.StatusCode, result.Content);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "REST GET failed: ");
            }
            return resultText;
        }

    }
}
