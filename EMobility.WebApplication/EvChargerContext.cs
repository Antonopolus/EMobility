using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility
{
    public class Price
    {
        public int Id { get; set; }

        public string Product { get; set; }

        public decimal ProductPrice { get; set; }
    }

    /// <summary>
    /// Electric Vehicle Charger Context 
    /// The database context for:
    ///     Charging Stations
    ///     Charging Sessions
    ///     Charging Logs
    /// </summary>
    public class EvChargerContext : DbContext
    {

        public EvChargerContext(DbContextOptions<EvChargerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region seed data

            #endregion
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ChargingPoint> ChargingPoints { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<ChargingSession> ChargingSessions { get; set; }
        
    }


}
