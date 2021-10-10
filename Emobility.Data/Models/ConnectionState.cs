using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EMobility.Data
{
    public class ConnectionState
    {
        public int Id;
        public string ChargingPointId { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.MinValue;
        public VehicleConnectionState State { get; set; } = VehicleConnectionState.Unknown;
    }

    public class ConnectionStateEntityConfiguration : IEntityTypeConfiguration<ConnectionState>
    {

        void IEntityTypeConfiguration<ConnectionState>.Configure(EntityTypeBuilder<ConnectionState> builder)
        {
            builder.HasKey(cs => cs.Id);
            builder.Property(cs => cs.State).HasConversion<string>();
        }
    }
}
