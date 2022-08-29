using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa.Pizza.Core.Model.Advertisement;

namespace WA.Pizza.Tests
{
    public class AdvertisementTestsBaseClass: BaseDatabaseTestClass
    {
        internal readonly string _testApiKey = "AD-Qmfa6zl4ZSrjoshwetEfCqmBgfVyGXI0f";

       private AdvertisementClient _addTestAdvertisementClient()
        {
            AdvertisementClient testAdvertisementClient = new AdvertisementClient { Name = "TestProviderToRead", Website = "www.yandex.ru", ApiKey = _testApiKey };
            var testAdvertisementClientAdded = applicationDbContext.AdvertisementClients.Add(testAdvertisementClient).Entity;
            return testAdvertisementClientAdded;
        }
        internal AdvertisementClient AddTestAdvertisementClient()
        {
            var testAdvertisementClientAdded = _addTestAdvertisementClient();
            applicationDbContext.SaveChanges();
            return testAdvertisementClientAdded;
        }
        internal async Task<AdvertisementClient> AddTestAdvertisementClientAsync()
        {
            var testAdvertisementClientAdded = _addTestAdvertisementClient();
            await applicationDbContext.SaveChangesAsync();
            return testAdvertisementClientAdded;
        }

        public AdvertisementTestsBaseClass(): base()
        {

        }

    }
}
