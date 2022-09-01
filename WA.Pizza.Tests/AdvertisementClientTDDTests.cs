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
    public class AdvertisementClientTDDTests : AdvertisementTestsBaseClass
    {
        private readonly AdvertisementClientService _advertisementClientService;

        public AdvertisementClientTDDTests(): base()
        {
            _advertisementClientService = new AdvertisementClientService(new ApiKeyService(applicationDbContext), applicationDbContext);
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
            var testAdvertisementClient = (await AddTestAdvertisementClientAsync()).Adapt<AdvertisementClientDTO>();
            //act
            string regerenatedApiKey = await _advertisementClientService.RegenerateApiKey(testAdvertisementClient.Id, testAdvertisementClient.ApiKey);
            testAdvertisementClient.ApiKey = regerenatedApiKey;
            //assert
            var updatedAdvertisementClient = await isAdvertisementClientNotNull(testAdvertisementClient);
            updatedAdvertisementClient.ApiKey.Should().Be(regerenatedApiKey);
        }

        [Fact]
        public async Task advertisement_throws_exception_on_null()
        {
            //arrange
          CreateAdvertisementClientDTO advertisementClientDTO = null;
          Func<Task> act = async () => await _advertisementClientService.CreateAdvertisementClientAsync(advertisementClientDTO);
          //act && assert
          await act.Should().ThrowAsync<WrongDataFormatException>();
        }
        [Fact]
        public async Task advertisement_created()
        {
            //arrange
            CreateAdvertisementClientDTO advertsiementClientDTO = new CreateAdvertisementClientDTO { Name = "test", Website = "google.com" };
            int itemCount = applicationDbContext.AdvertisementClients.Count();
            //act
            await _advertisementClientService.CreateAdvertisementClientAsync(advertsiementClientDTO);
            //assert
            applicationDbContext.AdvertisementClients.Count().Should().Be(itemCount+1);
        }


        [Fact]
        public async void advertisement_is_retrieved_admin()
        {
            var addedAdvertisementClient = await AddTestAdvertisementClientAsync();
            //act
            
            AdvertisementClientDTO advertisementClientDTO = await _advertisementClientService.GetAdvertisementClientById(addedAdvertisementClient.Id, _testApiKey);
            //assert
            advertisementClientDTO.Should().NotBeNull();
            advertisementClientDTO.ApiKey.Should().BeEquivalentTo(_testApiKey);
        }

        [Fact]
        public async void advertisement_is_retrieved()
        {
            await AddTestAdvertisementClientAsync();
            //act
            AdvertisementClientDTO advertisementClientDTO = await _advertisementClientService.GetAdvertisementClient(_testApiKey);
            //assert
            advertisementClientDTO.Should().NotBeNull();
            advertisementClientDTO.ApiKey.Should().BeEquivalentTo(_testApiKey);
        }
        [Fact]
        public async void advertisement_failed_to_retrieve_invalid_apiKey()
        {
            //arrange
            await AddTestAdvertisementClientAsync();
            Func<Task> act =  async () => await _advertisementClientService.GetAdvertisementClient("meow");
            //act && assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }


        [Fact]
        public async void advertisement_update_failed_invalid_api_key()
        {
            //arrange
            var testAdvertisementClient = (await AddTestAdvertisementClientAsync()).Adapt<AdvertisementClientDTO>();
            testAdvertisementClient.ApiKey = "meow";
            Func<Task> act = async () => await _advertisementClientService.UpdateAdvertisementClient(testAdvertisementClient.Id, testAdvertisementClient);
            //act && assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }
        [Fact]
        public async void advertisement_update_failed_param_is_null()
        {
            //arrange
            AdvertisementClientDTO testAdvertisementClient = null;
            Func<Task> act = async () => await _advertisementClientService.UpdateAdvertisementClient(-1, testAdvertisementClient);
           
            await act.Should().ThrowAsync<WrongDataFormatException>();
        }
        [Fact]
        public async void advertisement_is_updated()
        {
            //arrange
            var testAdvertisementClient = (await AddTestAdvertisementClientAsync()).Adapt<AdvertisementClientDTO>();
            testAdvertisementClient.Website = "www.myspace.com";
            //act
            await _advertisementClientService.UpdateAdvertisementClient(testAdvertisementClient.Id, testAdvertisementClient);
            //asert
            var updatedTestAdvertisementClient = await isAdvertisementClientNotNull(testAdvertisementClient);
            updatedTestAdvertisementClient.Adapt<AdvertisementClientDTO>().Should().BeEquivalentTo(testAdvertisementClient);
        }

        [Fact]
        public async void advertisement_remove_failed_invalid_api_key()
        {
            //arrange
            var testAdvertisementClient = (await AddTestAdvertisementClientAsync()).Adapt<AdvertisementClientDTO>();
            testAdvertisementClient.ApiKey = "meow";
            Func<Task> act = async () => await _advertisementClientService.DeleteAdvertisementClient(testAdvertisementClient.Id, testAdvertisementClient);
            //act && assert
            await act.Should().ThrowAsync<EntityNotFoundException>();

        }
        [Fact]
        public async void advertisement_remove_failed_param_is_null()
        {
            //arrange
            AdvertisementClientDTO testAdvertisementClient = null;
            Func<Task> act = async () => await _advertisementClientService.DeleteAdvertisementClient(-1, testAdvertisementClient);
            //act && assert
            await act.Should().ThrowAsync<WrongDataFormatException>();
        }

        [Fact]
        public async void advertisement_is_removed()
        {
           //arrange 
            var testAdvertisementClient = (await AddTestAdvertisementClientAsync()).Adapt<AdvertisementClientDTO>();
            var items = applicationDbContext.AdvertisementClients.Count();
            //act
            await _advertisementClientService.DeleteAdvertisementClient(testAdvertisementClient.Id, testAdvertisementClient);
            //assert
            applicationDbContext.AdvertisementClients.Count().Should().Be(items-1);
        }


    }
}
