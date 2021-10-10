using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace EMobility.Data
{
    public class ChargingPoint
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string RestUrl { get; set; } = string.Empty;

        public string ChargingPointId { get; set; } = string.Empty;

        public ChargingPoint() { }

        public ChargingPoint(int id, string name, string restUrl, string chargingPointId)
        {
            Id = id;
            Name = name;
            RestUrl = restUrl;
            ChargingPointId = chargingPointId;
        }
    }

    public class ChargingPointEntityConfiguration : IEntityTypeConfiguration<ChargingPoint>
    {

        void IEntityTypeConfiguration<ChargingPoint>.Configure(EntityTypeBuilder<ChargingPoint> builder)
        {
            //builder.ToTable("CP");
            builder.HasKey(cp => cp.Id);
            builder.HasIndex(cp => cp.ChargingPointId).IsUnique();

            foreach (var cp in CreateData())
            {
                builder.HasData(cp);
            }
        }
        // TODO private
        public  static List<ChargingPoint> CreateData()
        {
            List<ChargingPoint> chargingPoints = new();
            chargingPoints.Add(new(-4, "TG Stellplatz 4", "http://172.16.0.146/rest/", "1384202.00082"));     // Besucherparkplatz TG 4
            chargingPoints.Add(new(-5, "TG Stellplatz 5", "http://172.16.0.147/rest/", "1384202.00080"));     // Mayer Thomas
            chargingPoints.Add(new(1, "Stellplatz 1", "http://172.16.0.148:81/rest/", "140812422.00057"));    // Besucherparkplatz 1
            chargingPoints.Add(new(2, "Stellplatz 2", "http://172.16.0.148:82/rest/", "140812422.00057#2"));  // Besucherparkplatz 2  / slave
            chargingPoints.Add(new(3, "Stellplatz 3", "http://172.16.0.149:81/rest/", "140612412.00041"));    // Besucherparkplatz 3
            chargingPoints.Add(new(4, "Stellplatz 4", "http://172.16.0.149:82/rest/", "140612412.00041#2"));  // Besucherparkplatz 4  / slave
            return chargingPoints;
        }

    }
}
