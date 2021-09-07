using EMobility;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMobilityTestProject
{
    public class ChargingPointManagerTest
    {
        [Fact]
        public async void Test()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("vehicle_charging_type2"),
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            ChargingPointManager manager = new(httpClient);
            Assert.NotNull(manager);
            await manager.CheckVehicleConnectionStatesAsync(new CancellationToken());
        }

        [Fact]
        public void TestHasChargingSessionEnded()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

           
        }
    }
}
