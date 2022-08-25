using FluentAssertions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Core.Model.Advertisement;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO;
using Xunit;


namespace WA.Pizza.Tests
{
#nullable disable
    [Collection("Test database collection")]
    public class AdvertisementClientTDDTests : BaseDatabaseTestClass
    {
        private readonly ApiKeyService _apiKeyService;
        private readonly AdvertisementClientService _advertisementClientService;
        private readonly string _testApiKey = "AD-Qmfa6zl4ZSrjoshwetEfCqmBgfVyGXI0f";
        private async Task<AdvertisementClient> AddTestAdvertisementClient()
        {
            AdvertisementClient testAdvertisementClient = new AdvertisementClient { Name = "TestProviderToRead", Website = "www.yandex.ru", ApiKey = _testApiKey };
            var _testAdvertisementClient = applicationDbContext.AdvertisementClients.Add(testAdvertisementClient).Entity;
            await applicationDbContext.SaveChangesAsync();
            return _testAdvertisementClient;
        }
        public AdvertisementClientTDDTests(): base()
        {
            _apiKeyService = new ApiKeyService(applicationDbContext);
            _advertisementClientService = new AdvertisementClientService(_apiKeyService, applicationDbContext);
        }

        private async Task<AdvertisementClient> isAdvertisementClientNotNull(AdvertisementClientDTO testAdvertisementClient)
        {
            var updatedTestAdvertisementClient = await applicationDbContext.AdvertisementClients
                        .AsNoTracking()
                        .FirstOrDefaultAsync(ac => ac.ApiKey == testAdvertisementClient.ApiKey);
            updatedTestAdvertisementClient.Should().NotBeNull();
            return updatedTestAdvertisementClient;
        }
        [Fact]
        public async void regerenerate_api_key()
        {
            //arrange
            var testAdvertisementClient = (await AddTestAdvertisementClient()).Adapt<AdvertisementClientDTO>();
            //act
            string regerenatedApiKey = await _advertisementClientService.RegenerateApiKey(testAdvertisementClient.ApiKey);
            testAdvertisementClient.ApiKey = regerenatedApiKey;
            //assert
            var updatedAdvertisementClient = await isAdvertisementClientNotNull(testAdvertisementClient);
            updatedAdvertisementClient.ApiKey.Should().Be(regerenatedApiKey);
        }

        [Fact]
        public async Task advertisement_throws_exception_on_null()
        {
          //arrange
          AdvertisementClientDTO advertisementClientDTO = null;
          //act
          //assert
          await Assert.ThrowsAsync<WrongDataFormatException> ( async ()=> await  _advertisementClientService.CreateAdvertisementClientAsync(advertisementClientDTO));  
        }



        [Fact]
        public async void advertisement_is_retrieved()
        {
            await AddTestAdvertisementClient();
            //act
            AdvertisementClientDTO advertisementClientDTO = await _advertisementClientService.GetAdvertisementClientAsync(_testApiKey);
            //assert
            advertisementClientDTO.Should().NotBeNull();
            advertisementClientDTO.ApiKey.Should().BeEquivalentTo(_testApiKey);
        }
        [Fact]
        public async void advertisement_failed_to_retrieve_invalid_apiKey()
        {
            //arrange
            await AddTestAdvertisementClient();
            //act //assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _advertisementClientService.GetAdvertisementClientAsync("meow"));
        }


        [Fact]
        public async void advertisement_update_failed_invalid_api_key()
        {
            //arrange
            var testAdvertisementClient = (await AddTestAdvertisementClient()).Adapt<AdvertisementClientDTO>();
            testAdvertisementClient.ApiKey = "meow";
            //act
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _advertisementClientService.UpdateAdvertisementClient(testAdvertisementClient));
            //asert
        }
        [Fact]
        public async void advertisement_update_failed_param_is_null()
        {
            //arrange
            AdvertisementClientDTO testAdvertisementClient = null;
            //act
            await Assert.ThrowsAsync<WrongDataFormatException>(async () => await _advertisementClientService.UpdateAdvertisementClient(testAdvertisementClient));
            //asert
        }
        [Fact]
        public async void advertisement_is_updated()
        {
            //arrange
            var testAdvertisementClient = (await AddTestAdvertisementClient()).Adapt<AdvertisementClientDTO>();
            testAdvertisementClient.Website = "www.myspace.com";
            //act
            await _advertisementClientService.UpdateAdvertisementClient(testAdvertisementClient);
            //asert
            var updatedTestAdvertisementClient = await isAdvertisementClientNotNull(testAdvertisementClient);
            updatedTestAdvertisementClient.Adapt<AdvertisementClientDTO>().Should().BeEquivalentTo(testAdvertisementClient);
        }

        [Fact]
        public async void advertisement_remove_failed_invalid_api_key()
        {
            //arrange
            var testAdvertisementClient = (await AddTestAdvertisementClient()).Adapt<AdvertisementClientDTO>();
            testAdvertisementClient.ApiKey = "meow";
            //act
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _advertisementClientService.DeleteAdvertisementClient(testAdvertisementClient));
            //asert
        }
        [Fact]
        public async void advertisement_remove_failed_param_is_null()
        {
            //arrange
            AdvertisementClientDTO testAdvertisementClient = null;
            //act
            await Assert.ThrowsAsync<WrongDataFormatException>(async () => await _advertisementClientService.DeleteAdvertisementClient(testAdvertisementClient));
            //asert
        }

        [Fact]
        public async void advertisement_is_removed()
        {
           //arrange 
            var testAdvertisementClient = (await AddTestAdvertisementClient()).Adapt<AdvertisementClientDTO>();
            var items = applicationDbContext.AdvertisementClients.Count();
            //act
            await _advertisementClientService.DeleteAdvertisementClient(testAdvertisementClient);
            //assert
            applicationDbContext.AdvertisementClients.Count().Should().Be(items-1);
        }


    }
}
