using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMobility.Data.IntegrationTest
{
    public abstract class DatabaseFixture : IDisposable
    {
        public EMobilityDbContext Context { get; protected set; }
        public DatabaseFixture()
        {
           
        }


        public void Dispose()
        {
            Context.Dispose();
        }
    }

    public class DatabaseFixtureSqlServer : DatabaseFixture
    {
        public DatabaseFixtureSqlServer()
        {
            var facrory = new EMobilityDbContextFactory();
            Context = facrory.CreateDbContext(new string[0]);

            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }
    }

        public class DatabaseFixtureInMemory : DatabaseFixture
    {
        public DatabaseFixtureInMemory()
        {
            Context = CreateDbContext(Guid.NewGuid().ToString());

            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        internal EMobilityDbContext CreateDbContext(string databaseName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EMobilityDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName);
            return new EMobilityDbContext(optionsBuilder.Options);
        }
    }


}
