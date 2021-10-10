using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel;

namespace EMobility.Data
{
#nullable enable
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

    public class ChargingSessionEntityConfiguration : IEntityTypeConfiguration<ChargingSession>
    {

        void IEntityTypeConfiguration<ChargingSession>.Configure(EntityTypeBuilder<ChargingSession> builder)
        {
            builder.HasKey(cs=> cs.Id);

        }
    }
}
