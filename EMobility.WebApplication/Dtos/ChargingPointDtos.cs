using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMobility.WebApi.Dtos
{
    public class ChargingPointReadDto
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string RestUrl { get; set; } = string.Empty;

        public string ChargingPointId { get; set; } = string.Empty;
    }

    public class ChargingPointCreateDto
    {
        public string Name { get; set; } = string.Empty;

        public string RestUrl { get; set; } = string.Empty;

        public string ChargingPointId { get; set; } = string.Empty;
    }

}

