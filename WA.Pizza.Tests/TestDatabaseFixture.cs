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
    public class TestDatabaseFixture
    {
        public static ApplicationDbContext createContext()
        {
            ApplicationDbContext applicationDbContext;
            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings_test.json")
                       .Build();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlServer(config.GetConnectionString("Test"));
            applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.Migrate();
            return applicationDbContext;
        }

    }
    [CollectionDefinition("Test database collection")]
    public class DatabaseCollection : ICollectionFixture<TestDatabaseFixture>,  IDisposable
    {
        public DatabaseCollection()
        {


        }

        public void Dispose()
        {
        }

        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
