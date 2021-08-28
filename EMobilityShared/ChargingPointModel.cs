using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility
{
    public record ChargingPointModel(int Id, string Name, string RestUrl, string ChargePointId)
    {
    }
}
