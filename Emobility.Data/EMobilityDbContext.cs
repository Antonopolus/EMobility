using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility.Data
{
    /// <summary>
    /// <b>Electric Vehicle Charger Context </b><br/>
    /// The database context for: <br/>
    ///     Charging Stations <br/>
    ///     Charging Sessions <br/>
    ///     Charging Logs <br/>
    /// </summary>
    public class EMobilityDbContext : DbContext
    {
        public EMobilityDbContext(DbContextOptions<EMobilityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EMobilityDbContext).Assembly);

            //modelBuilder.Entity<ChargingSession>().HasOne(cs => cs.ChargingStation).WithOne(cp => cp.)
        }

        public DbSet<ChargingPoint> ChargingPoints { get; set; }

        public DbSet<ChargingSession> ChargingSessions { get; set; }

        public DbSet<ConnectionState> ConnectionStates { get; set; }

    }

    public class EMobilityDbContextFactory : IDesignTimeDbContextFactory<EMobilityDbContext>
    {
        public EMobilityDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                            //.AddJsonFile($"appsettings.{Environment.MachineName}.json")
                                                            .Build();
            var optionsBuilder = new DbContextOptionsBuilder<EMobilityDbContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            return new EMobilityDbContext(optionsBuilder.Options);
        }
    }
}
