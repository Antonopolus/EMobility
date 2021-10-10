using System;

namespace EMobility.Data
{
#nullable enable
    public class ChargingSessionLog
    {
        public int Id { get; init; } = 0;

        public int SessionId { get; init; } = 0;

        public DateTime TimeStamp { get; init; } = DateTime.MinValue;   // UTC datetime

        public TimeSpan LocalTimeStamp { get; init; } = TimeSpan.Zero;  // local time

        public TimeSpan DurationOf { get; init; } = TimeSpan.Zero;      // 

        public Decimal Energy { get; init; } = 0;

        public string? ChargingStation { get; init; }
    }
}
