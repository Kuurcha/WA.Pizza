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

namespace WA.Pizza.Tests.API
{
#nullable disable
    [Collection("Test database collection")]
    public class AdvertisementTDDTests : AdvertisementTestsBaseClass
    {
        private readonly AdvertisementService _advertisementService;
        private readonly AdvertisementDTO _testAdvertisementDTO;
        private Advertisement _testAdvertisement;
        private readonly AdvertisementClient _testAdvertisementClient;

        private async Task AddAdvertisement()
        {
            Advertisement testAdvertisement = _testAdvertisementDTO.Adapt<Advertisement>();
            _testAdvertisement =   applicationDbContext.Advertisements.Add(testAdvertisement).Entity;
            await applicationDbContext.SaveChangesAsync();
        }

        public AdvertisementTDDTests(): base()
        {
            _advertisementService = new AdvertisementService(new ApiKeyService(applicationDbContext), applicationDbContext);
            _testAdvertisementClient = AddTestAdvertisementClient();
            _testAdvertisementDTO = new AdvertisementDTO { Description = "Buy our toys", ImageURL = "https://i.imgur.com/UcFijO8.png", RedicrectURL = "https://products.hasbro.com", AdvertisementClientId = _testAdvertisementClient.Id };

        }

        [Fact]  
        public async void advertisement_is_added()
        {
            int currentItemCount = applicationDbContext.Advertisements.Count();
            //act
            await _advertisementService.CreateAdvertisement(_testAdvertisementDTO.Adapt<CUDAdvertisementDTO>(), _testApiKey);
            //assert 
            applicationDbContext.Advertisements.Count().Should().Be(currentItemCount+1);
        }

        [Fact]
        public async void advertisement_failed_to_add_invalid_api_key()
        {
   
            //arrange
            Func <Task> act = async () => await  _advertisementService.CreateAdvertisement(_testAdvertisementDTO.Adapt<CUDAdvertisementDTO>(), "meow");
            //Act && Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }



        [Fact]
        public async void advertisement_is_updated()
        {
            //arrange
            await AddAdvertisement();

            const string newDescription = "New test description";
            _testAdvertisementDTO.Description = newDescription;
            _testAdvertisementDTO.Id = _testAdvertisement.Id;
            //act
            await _advertisementService.UpdateAdvertisement(_testAdvertisementDTO.Adapt<CUDAdvertisementDTO>(), _testAdvertisement.Id, _testApiKey);

            var updatedTestAdvertisement = applicationDbContext.Advertisements.FirstOrDefault(a => a.Id == _testAdvertisement.Id);

            updatedTestAdvertisement.Description.Should().BeEquivalentTo(newDescription);
        }

        [Fact]
        public async void advertisement_failed_to_update_advertisement_does_not_exist()
        {
            //arrange
            await AddAdvertisement();

            const string newDescription = "New test description";
            _testAdvertisementDTO.Description = newDescription;
            //act
            Func<Task> act  =  async () => await _advertisementService.UpdateAdvertisement(_testAdvertisementDTO.Adapt<CUDAdvertisementDTO>(), -1,  _testApiKey);

            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async void advertisement_failed_to_update_incorrect_or_illegal_api_key()
        {
            //arrange
            await AddAdvertisement();

            const string newDescription = "New test description";
            _testAdvertisementDTO.Description = newDescription;
            _testAdvertisementDTO.Id = _testAdvertisement.Id;
            //act
            Func<Task> act  =  async () => await _advertisementService.UpdateAdvertisement(_testAdvertisementDTO.Adapt<CUDAdvertisementDTO>(), _testAdvertisement.Id, "Meow");

            await act.Should().ThrowAsync<WrongDataFormatException>();
        }




        [Fact]
        public async void advertisement_is_retrieved()
        {
            await AddAdvertisement();

            //act
            var retrievedAdvertisement = await _advertisementService.GetAdvertisement(_testApiKey, _testAdvertisement.Id);

            //assert
            retrievedAdvertisement.Should().NotBeNull();
            retrievedAdvertisement.Should().BeEquivalentTo(_testAdvertisement.Adapt<AdvertisementDTO>());
        }

        [Fact]
        public async void advertisement_is_not_retrieved_invalid_api_key()
        {
            await AddAdvertisement();

            Func<Task> act = async () => await _advertisementService.GetAdvertisement("meow", _testAdvertisement.Id);

            await act.Should().ThrowAsync<WrongDataFormatException>();
        }

        [Fact]
        public async void advertisement_is_not_retrieved_advertisement_does_not_exist()
        {
            await AddAdvertisement();

            Func<Task> act = async () => await _advertisementService.GetAdvertisement(_testApiKey, -1);

            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async void advertisement_is_removed()
        {
            //arrange
            await AddAdvertisement();

            //act
            await _advertisementService.RemoveAdvertisement(_testAdvertisement.Id, _testApiKey);
            //assert
            applicationDbContext.Advertisements.Any(a => a.Id == _testAdvertisement.Id).Should().BeFalse();

        }

        [Fact]
        public async void advertisement_failed_to_remove_incorrect_or_illegal_api_key()
        {
            //arrange
            await AddAdvertisement();
            
            //act
            Func<Task> act = async () => await _advertisementService.RemoveAdvertisement( _testAdvertisement.Id, "Meow");
            //assert
            await act.Should().ThrowAsync<WrongDataFormatException>();

        }

        [Fact]
        public async void advertisement_failed_to_remove_advertisement_does_not_exist()
        {

            //arrange
            await AddAdvertisement();
            _testAdvertisement.Id = -1;
            //act
            Func<Task> act = async () => await _advertisementService.RemoveAdvertisement( -1, _testApiKey);
            //assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

    }
}
