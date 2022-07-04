using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System;
using System.Threading.Tasks;
using Wa.Pizza.Infrasctructure.Validators;
using Xunit;

namespace WA.Pizza.Tests
{
	public class BaseDatabaseTestClass : IDisposable
	{
		public ApplicationDbContext applicationDbContext;
	
		/*		public Checkpoint checkpoint = new Checkpoint()
				{
					SchemasToInclude = new[]
					{
						"Test"
					}
				};*/
		protected BaseDatabaseTestClass()
		{
			var config = new ConfigurationBuilder()
			.AddJsonFile("appsettings_test.json")
			.Build();
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
			   .UseSqlServer(config.GetConnectionString("Test"));
			applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);


		}

        public void Dispose()
        {
			//await checkpoint.Reset(applicationDbContext.Database.GetDbConnection()); - causes deadlock?
			applicationDbContext.Dispose();


		}


    }
}

