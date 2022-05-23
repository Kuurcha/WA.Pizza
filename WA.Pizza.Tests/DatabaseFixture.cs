using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Pizza.Tests
{
    public class DataBaseFixture : IDisposable
    {
        public ApplicationDbContext applicationDbContext { get; private set; }
        public DataBaseFixture()
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
}
