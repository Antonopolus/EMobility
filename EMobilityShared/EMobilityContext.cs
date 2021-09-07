using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility
{
    public class EMobilityContext
    {

        public static List<ChargingPoint> GetChargingPoints()
        {
            var ChargingPoints = new List<ChargingPoint>
            {
                new(-4, "TG Stellplatz 4", "http://172.16.0.146/rest/", "1384202.00082"),     // Besucherparkplatz TG 4
                new(-5, "TG Stellplatz 5", "http://172.16.0.147/rest/", "1384202.00080"),     // Mayer Thomas
                new(1, "Stellplatz 1", "http://172.16.0.148:81/rest/", "140812422.00057"),    // Besucherparkplatz 1
                new(2, "Stellplatz 2", "http://172.16.0.148:82/rest/", "140812422.00057#2"),  // Besucherparkplatz 2  / slave
                new(3, "Stellplatz 3", "http://172.16.0.149:81/rest/", "140612412.00041"),    // Besucherparkplatz 3
                new(4, "Stellplatz 4", "http://172.16.0.149:82/rest/", "140612412.00041#2")   // Besucherparkplatz 4  / slave
            };
            return ChargingPoints;
        }


    }
}
