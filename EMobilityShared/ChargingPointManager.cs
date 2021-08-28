using EMobility.Mennekes;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMobility
{
    public class ChargingPointManager
    {
        readonly HttpClient HttpClientConnection;

        record ChargingPoint(int Id, string Name, string RestUrl, string ChargePointId, VehicleConnection Connection);
        readonly List<ChargingPoint> ChargingPoints;

        public ChargingPointManager(HttpClient httpClient)
        {
            this.HttpClientConnection = httpClient;

            ChargingPoints = new();
            ChargingPoints.Add(new(-4, "TG Stellplatz 4", "http://172.16.0.146/rest/", "1384202.00082", new MennekesVehicleConnection()));     // Besucherparkplatz TG 4
            ChargingPoints.Add(new(-5, "TG Stellplatz 5", "http://172.16.0.147/rest/", "1384202.00080", new MennekesVehicleConnection()));     // Mayer Thomas
            ChargingPoints.Add(new(1, "Stellplatz 1", "http://172.16.0.148:81/rest/", "140812422.00057", new MennekesVehicleConnection()));    // Besucherparkplatz 1
            ChargingPoints.Add(new(2, "Stellplatz 2", "http://172.16.0.148:82/rest/", "140812422.00057#2", new MennekesVehicleConnection()));  // Besucherparkplatz 2  / slave
            ChargingPoints.Add(new(3, "Stellplatz 3", "http://172.16.0.149:81/rest/", "140612412.00041", new MennekesVehicleConnection()));    // Besucherparkplatz 3
            ChargingPoints.Add(new(4, "Stellplatz 4", "http://172.16.0.149:82/rest/", "140612412.00041#2", new MennekesVehicleConnection()));  // Besucherparkplatz 4  / slave
        }

        public void CheckVehicleConnectionStates(CancellationToken cancelationToken)
        {
            var result = Parallel.ForEach<ChargingPoint>(ChargingPoints, async item =>
            {
                var state = await RequestStatus(item, cancelationToken);
                if (item.Connection.HasNewState(state))
                {
                    HandleNewState(item);
                }
            });

        }

        protected void CheckVehicleConnectionStates()
        {
            var cancelationToken = new CancellationToken();
            CheckVehicleConnectionStates(cancelationToken);
        }

        private static void HandleNewState(ChargingPoint chargingPoint)
        {
            Log.Information("charge point {Name} has new state -> {State}", chargingPoint.Name, chargingPoint.Connection.State);
        }

        private async Task<string> RequestStatus(ChargingPoint chargingPoint, CancellationToken cancelationToken)
        {
            string resultText = String.Empty;
            try
            {
                var command = "conn_state";
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
