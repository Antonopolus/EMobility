using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility
{
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
            modelBuilder.Entity<ChargingPoint>().HasData(EMobilityContext.GetChargingPoints());

            #endregion
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ChargingPoint> ChargingPoints { get; set; }

        public DbSet<ChargingSession> ChargingSessions { get; set; }

        public class EvChargerContextFactory : IDesignTimeDbContextFactory<EvChargerContext>
        {
            public EvChargerContext CreateDbContext(string[] args)
            {
                var configuration = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
                var optionsBuilder = new DbContextOptionsBuilder<EvChargerContext>();
                optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
                return new EvChargerContext(optionsBuilder.Options);
            }
        }
    }

}
