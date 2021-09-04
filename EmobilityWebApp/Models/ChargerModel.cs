using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmobilityWebApp.Models
{
    public record ChargerModel (int Id, string Name, string RestUrl, string ChargingPointId)
    {
        //int Id { get; set; }
    }
}
