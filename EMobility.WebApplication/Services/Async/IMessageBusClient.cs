using EMobility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMobility.WebApi.Services.Async
{
    public interface IMessageBusClient
    {
        void PublishNewChargingPoint(ChargingPoint chargingPoint);
    }
}
