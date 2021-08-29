using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility
{
    public class ChargingSession
    {
        public int Id { get; init; }

        public int SessionId { get; init; }

        [DisplayName("UTC Datum")]
        public DateTime StartDate { get; init; }        // UTC datetime

        public TimeSpan LocalStartTime { get; init; }   // local time

        public TimeSpan DurationOf { get; init; }

        public Decimal Energy { get; init; }

        public string? RfidTag { get; init; }

        public string? ChargingStation { get; init; }
    }
}