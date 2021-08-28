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

         public ChargingPointManager(HttpClient httpClient)
        {
            this.HttpClientConnection = httpClient;
        }

        public async void CheckVehicleConnectionStates(CancellationToken cancelationToken)
        {
            await DoWork(cancelationToken);
        }

        public  void CheckVehicleConnectionStates()
        {
            var cancelationToken = new CancellationToken();
            CheckVehicleConnectionStates(cancelationToken);
        }

        private async Task<string> DoWork(CancellationToken cancelationToken)
        {
            var result = await HttpClientConnection.GetAsync("http://172.16.0.147/rest/conn_state", cancelationToken);
            //var res = await HttpClientConnection.GetAsync("http://heise.de");
            if (result.IsSuccessStatusCode)
            {
                Log.Information("WebSite is up statuscode[{StatusCode}]", result.StatusCode);
                string responseBody = await result.Content.ReadAsStringAsync(cancelationToken);
                return responseBody;
            }
            else
            {
                Log.Error("WebSite is down statuscode[{StatusCode}]", result.StatusCode);
            }
            return String.Empty;
        }

    }
}
