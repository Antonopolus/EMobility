using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility
{
    // Ladestation
    public record ChargingPoint(int Id, string Name, string RestUrl, string ChargingPointId)
    {
    }
}
