using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMobility.WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChargingPointController : ControllerBase
    {
        readonly List<ChargingPointModel> ChargingPoints = new();

        private readonly ILogger<ChargingPointController> _logger;

        public ChargingPointController(ILogger<ChargingPointController> logger)
        {
            _logger = logger;
            ChargingPoints = new();
            ChargingPoints.Add(new(-4, "TG Stellplatz 4", "http://172.16.0.146/rest/", "1384202.00082"));     // Besucherparkplatz TG 4
            ChargingPoints.Add(new(-5, "TG Stellplatz 5", "http://172.16.0.147/rest/", "1384202.00080"));     // Mayer Thomas
            ChargingPoints.Add(new(1, "Stellplatz 1", "http://172.16.0.148:81/rest/", "140812422.00057"));    // Besucherparkplatz 1
            ChargingPoints.Add(new(2, "Stellplatz 2", "http://172.16.0.148:82/rest/", "140812422.00057#2"));  // Besucherparkplatz 2  / slave
            ChargingPoints.Add(new(3, "Stellplatz 3", "http://172.16.0.149:81/rest/", "140612412.00041"));    // Besucherparkplatz 3
            ChargingPoints.Add(new(4, "Stellplatz 4", "http://172.16.0.149:82/rest/", "140612412.00041#2"));  // Besucherparkplatz 4  / slave
        }

        [HttpGet]
        public IEnumerable<ChargingPointModel> Get()
        {
            return ChargingPoints.ToArray();
        }
    }
}
