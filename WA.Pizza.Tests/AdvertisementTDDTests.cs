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

        private async Task<int> _addAdvertisement()
        {
            Advertisement testAdvertisement = _testAdvertisementDTO.Adapt<Advertisement>();
            _testAdvertisement =   applicationDbContext.Advertisements.Add(testAdvertisement).Entity;
            applicationDbContext.Entry(_testAdvertisement).State = EntityState.Detached;
            return await applicationDbContext.SaveChangesAsync();
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
            await _advertisementService.CreateAdvertisement(_testAdvertisementDTO, _testApiKey);
            //assert
            applicationDbContext.Advertisements.Count().Should().Be(currentItemCount+1);
        }

        [Fact]
        public async void advertisement_failed_to_add_invalid_api_key()
        {
   
            //arrange
            Func <Task> act = async () => await  _advertisementService.CreateAdvertisement(_testAdvertisementDTO, "meow");
            //Act && Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }



        [Fact]
        public async void advertisement_is_updated()
        {
            //arrange
            await _addAdvertisement();

            const string newDescription = "New test description";
            _testAdvertisementDTO.Description = newDescription;
            _testAdvertisementDTO.Id = _testAdvertisement.Id;
            //act
            await _advertisementService.UpdateAdvertisement(_testAdvertisementDTO, _testApiKey);

            var updatedTestAdvertisement = applicationDbContext.Advertisements.FirstOrDefault(a => a.Id == _testAdvertisementClient.Id);
            updatedTestAdvertisement.Description.Should().BeEquivalentTo(newDescription);
        }

        [Fact]
        public async void advertisement_failed_to_update_advertisement_does_not_exist()
        {
            //arrange
            await _addAdvertisement();

            const string newDescription = "New test description";
            _testAdvertisementDTO.Description = newDescription;

            //act
            Func<Task> act  =  async () => await _advertisementService.UpdateAdvertisement(_testAdvertisementDTO, _testApiKey);

            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async void advertisement_failed_to_update_incorrect_or_illegal_api_key()
        {
            //arrange
            await _addAdvertisement();

            const string newDescription = "New test description";
            _testAdvertisementDTO.Description = newDescription;
            _testAdvertisementDTO.Id = _testAdvertisement.Id;
            //act
            Func<Task> act  =  async () => await _advertisementService.UpdateAdvertisement(_testAdvertisementDTO, "Meow");

            await act.Should().ThrowAsync<WrongDataFormatException>();
        }



        /*        [Fact]
        public async void advertisement_is_removed()
        {
            //arrange
            var testAdvertisement = _addAdvertisement().Adapt<AdvertisementDTO>();

            //act
            await _advertisementService.RemoveAdvertisement(testAdvertisement, _testApiKey);
            //assert


        }*/

        /*        [Fact]
                public async void advertisement_is_retrieved()
                {
                    var testAdvertisement = _addAdvertisement().Adapt<AdvertisementDTO>();
                }*/

    }
}
