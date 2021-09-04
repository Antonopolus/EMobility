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
    public class ChargingPointManager
    {
        readonly HttpClient HttpClientConnection;

        record ChargerConnection(int Id, string Name, string RestUrl, string ChargePointId, VehicleConnection Connection);
        readonly List<ChargerConnection> ChargerConnections;
        private record SessionInfo(string StationId, string AuthenticationId, int ElapsedTime, int TotalPower, int Power, int TransactionPower);
        readonly List<SessionInfo> SessionInfos;

        public ChargingPointManager(HttpClient httpClient)
        {
            SessionInfos = new();
            this.HttpClientConnection = httpClient;

            ChargerConnections = new();
            ChargerConnections.Add(new(-4, "TG Stellplatz 4", "http://172.16.0.146/rest/", "1384202.00082", new MennekesVehicleConnection()));     // Besucherparkplatz TG 4
            ChargerConnections.Add(new(-5, "TG Stellplatz 5", "http://172.16.0.147/rest/", "1384202.00080", new MennekesVehicleConnection()));     // Mayer Thomas
            ChargerConnections.Add(new(1, "Stellplatz 1", "http://172.16.0.148:81/rest/", "140812422.00057", new MennekesVehicleConnection()));    // Besucherparkplatz 1
            ChargerConnections.Add(new(2, "Stellplatz 2", "http://172.16.0.148:82/rest/", "140812422.00057#2", new MennekesVehicleConnection()));  // Besucherparkplatz 2  / slave
            ChargerConnections.Add(new(3, "Stellplatz 3", "http://172.16.0.149:81/rest/", "140612412.00041", new MennekesVehicleConnection()));    // Besucherparkplatz 3
            ChargerConnections.Add(new(4, "Stellplatz 4", "http://172.16.0.149:82/rest/", "140612412.00041#2", new MennekesVehicleConnection()));  // Besucherparkplatz 4  / slave
        }

        public async Task CheckVehicleConnectionStates(CancellationToken cancelationToken, bool proceedParallel = false)
        {
            //if (proceedParallel)
            //{
            //    Parallel.ForEach<ChargerConnection>(ChargerConnections, async item =>
            //    {
            //        var state = await RequestStatus(item, cancelationToken);
            //        if (item.Connection.HasNewState(state))
            //        {
            //            await HandleNewState(item, cancelationToken);
            //        }
            //    });
            //}
            //else
            //{
            //    var tasks = ChargerConnections.Select(async point =>
            //    {
            //        var state = await RequestStatus(point, cancelationToken);
            //        if (point.Connection.HasNewState(state))
            //        {
            //            await HandleNewState(point, cancelationToken);
            //        }
            //    });
            //    await Task.WhenAll(tasks);
            //}
            await DoWork(cancelationToken);
        }

        private async Task DoWork(CancellationToken cancelationToken)
        {
            try
            {
                string requestUrl = "http://heise.de";
                var result = await HttpClientConnection.GetAsync(requestUrl, cancelationToken);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseBody = await result.Content.ReadAsStringAsync(cancelationToken);
                    Log.Debug("Request[{RequestUrl}]  OK", requestUrl);
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
        }

        protected async Task CheckVehicleConnectionStates()
        {
            var cancelationToken = new CancellationToken();
            await CheckVehicleConnectionStates(cancelationToken);
        }

        private async Task HandleNewState(ChargerConnection chargingPoint, CancellationToken cancelationToken)
        {
            Log.Information("charge point {Name} has new state -> {State}", chargingPoint.Name, chargingPoint.Connection.State);

            //ConnectionStateHandler handler = new ConnectionStateHandler();
            //handler.OnNewState(chargingPoint);

            if (chargingPoint.Connection.State == VehicleConnectionState.CHARGING)
            {
                var info = await RequestInfo(chargingPoint, cancelationToken);
                SessionInfos.Add(info);
            }

            if (chargingPoint.Connection.HasChargingSessionEnded())
            {
                Log.Information("charge point {Name} charging ended", chargingPoint.Name);
                string fileName = String.Format("{0}_Sessions.json", DateTime.Now.ToLongDateString());
                using FileStream createStream = File.Create(fileName);
                await JsonSerializer.SerializeAsync(createStream, SessionInfos, cancellationToken: cancelationToken);
                await createStream.DisposeAsync();
                SessionInfos.Clear();
            }
        }

        private async Task<string> RequestStatus(ChargerConnection chargingPoint, CancellationToken cancelationToken)
        {
            string resultText = await Request(chargingPoint, "conn_state", cancelationToken);
            return resultText;
        }

        private async Task<SessionInfo> RequestInfo(ChargerConnection chargingPoint, CancellationToken cancelationToken)
        {
            var stationId = await Request(chargingPoint, "cp_id", cancelationToken);                // station ID
            var authenticationId = await Request(chargingPoint, "auth_uid", cancelationToken);      // RFID of current user
            var duration = await Request(chargingPoint, "time_since_charging_start", cancelationToken);
            var meterWh = await Request(chargingPoint, "meter_wh", cancelationToken);
            var powerW = await Request(chargingPoint, "power_w", cancelationToken);
            var transactionWh = await Request(chargingPoint, "transaction_wh", cancelationToken);

            return new SessionInfo(stationId, authenticationId, Int32.Parse(duration), Int32.Parse(meterWh), Int32.Parse(powerW), Int32.Parse(transactionWh));
        }

        private async Task<string> Request(ChargerConnection chargingPoint, string command, CancellationToken cancelationToken)
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
