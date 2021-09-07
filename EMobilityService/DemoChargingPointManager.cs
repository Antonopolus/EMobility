using EMobility;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMobilityService
{
    internal class DemoChargingPointManager : IChargingPointManager

    {
        private readonly HttpClient HttpClientConnection;

        private CancellationToken cancelationToken;

        public DemoChargingPointManager(HttpClient connection)
        {
            HttpClientConnection = connection;
        }

        public void CheckVehicleConnectionStates(CancellationToken cancelationToken)
        {
            this.cancelationToken = cancelationToken;
            _ = DoWork(cancelationToken);   // bad demo code: sync over async 
        }

        public async Task CheckVehicleConnectionStatesAsync(CancellationToken cancelationToken)
        {
            this.cancelationToken = cancelationToken;
            await DoWork(cancelationToken);
        }

        public async Task DoWork(CancellationToken cancelationToken)
        {
            try
            {
                string requestUrl = "http://heise.de";
                var result = await HttpClientConnection.GetAsync(requestUrl, cancelationToken);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseBody = await result.Content.ReadAsStringAsync(cancelationToken);
                    Log.Debug("Request[{RequestUrl}]  OK", requestUrl);
                    HandleNewState(result.StatusCode);
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

        private void HandleNewState(System.Net.HttpStatusCode statusCode)
        {
            bool isCharging = true;

            if(isCharging)
            {
            //    RequestAndStoreInfo();
            //}
            //if(nextChargingCount)
            //{
            //    RequestCpInfo();
            //}
            //if (chargingEnded)
            //{
            //    RequestTransaktionInfo();
            }
        }
    }
}
