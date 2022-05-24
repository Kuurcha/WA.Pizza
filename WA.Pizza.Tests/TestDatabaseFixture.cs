using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WA.Pizza.Tests
{
    public class TestDatabaseFixture : IDisposable
    {
        public ApplicationDbContext applicationDbContext { get; private set; }
        public TestDatabaseFixture()
        {
            var config = new ConfigurationBuilder()
                               .AddJsonFile("appsettings_test.json")
                               .Build();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlServer(config.GetConnectionString("Test"));
            applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.Migrate();

        }

        public void Dispose()
        {
            applicationDbContext.Database.EnsureDeleted();
        }

        public ApplicationDbContext dbContext { get; private set; }
    }
    [CollectionDefinition("Test database collection")]
    public class DatabaseCollection : ICollectionFixture<TestDatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
