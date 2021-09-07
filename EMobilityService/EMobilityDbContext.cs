using EMobility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobilityService
{
    class EMobilityDbContext : DbContext
    {
        public EMobilityDbContext(DbContextOptions<EMobilityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ChargingPoint>? ChargingPoints { get; set; }

        public DbSet<ChargingSession>? ChargingSessions { get; set; }

        public class EMobilityDbContextFactory : IDesignTimeDbContextFactory<EMobilityDbContext>
        {
            public EMobilityDbContext CreateDbContext(string[] args)
            {
                var configuration = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
                var optionsBuilder = new DbContextOptionsBuilder<EMobilityDbContext>();
                optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
                return new EMobilityDbContext(optionsBuilder.Options);
            }
        }

    }
}
