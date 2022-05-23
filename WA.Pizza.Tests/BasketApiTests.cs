using FluentAssertions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Wa.Pizza.Infrasctructure.DTO.CatalogItem;
using Xunit;

namespace WA.Pizza.Tests
{    
    public class BasketDataTests: IClassFixture<DataBaseFixture>
    {
        private readonly DataBaseFixture _fixture;
        private readonly BasketDataService _basketDataService;

        private Basket basketTest;
        private CatalogItem catalogItemTest;
        private void data_seeding()
        {

            ApplicationUser applicationUser = _fixture.applicationDbContext.ApplicationUser.Add(new ApplicationUser { }).Entity;
             _fixture.applicationDbContext.SaveChanges();
            basketTest = _fixture.applicationDbContext.Basket.Add(new Basket { LastModified = new DateTime(2066, 11, 11), ApplicationUserId = applicationUser.Id }).Entity;
            catalogItemTest = _fixture.applicationDbContext.CatalogItem.Add( new CatalogItem { Quantity = 150, Name = "Cheeze", Description = "Classic", Price = 150, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Pizza }).Entity;
            _fixture.applicationDbContext.SaveChanges();
        }
        private void addTestBasketItem()
        {
            var basketItem = new BasketItem { BasketId = basketTest.Id, Quantity = Faker.RandomNumber.Next(1, 100), CatalogType = Core.CatalogType.CatalogType.Pizza, UnitPrice = 150, CatalogItemName = "Classic", CatalogItemId = catalogItemTest.Id };
            _fixture.applicationDbContext.BasketItem.Add(basketItem);
            _fixture.applicationDbContext.SaveChanges();
        }
        public BasketDataTests(DataBaseFixture fixture)
        {
            _fixture = fixture;
            _basketDataService = new BasketDataService(_fixture.applicationDbContext);
            //_fixture.applicationDbContext.Database.Migrate();
            data_seeding();
        }
        [Fact]
        public async void basket_item_is_added_to_basket()
        {
            //Arrange
            var basketItemDTO = (catalogItemTest.Adapt<CatalogItemDTO>()).ToBasketItemDto(5, basketTest.Id);

            DateTime previousDate = basketTest.LastModified;
            //Act
            await _basketDataService.AddItem(basketItemDTO);
            //Assert
            var updatedBasket = await _basketDataService.GetById(basketTest.Id);

            updatedBasket.LastModified.Should().NotBe(previousDate);

            updatedBasket.BasketItems.Should().HaveCount(1);
        }


        [Fact]
        public async void basket_item_is_deleted_from_basket()
        {
            addTestBasketItem();
            //Arrange
            var basket = await _basketDataService.GetById(basketTest.Id);
            var basketItems = basket.BasketItems;
            BasketItemDTO basketItemDTO = basketItems.Last().Adapt<BasketItemDTO>();
            //Act
            await _basketDataService.DeleteItem(basketItemDTO);
            //Assert
            basket = await _basketDataService.GetById(basketTest.Id);
            basket.BasketItems.Should().HaveCount(basketItems.Count - 1);
        }

        [Fact]
        public async void basket_was_updated()
        {
            //Arrange
            addTestBasketItem();
            var basket = await _basketDataService.GetById(basketTest.Id);
            var basketItems = basket.BasketItems;
            var basketItem = basketItems.Last();
            basketItem.Quantity = 666;
            BasketItemDTO basketItemDTO = basketItem.Adapt<BasketItemDTO>();

            //Act
            await _basketDataService.UpdateItem(basketItem);
            //Assert
            basket = await _basketDataService.GetById(basketTest.Id);
            basketItems = basket.BasketItems;
            //Be vs Equals?
            basketItems.Last().Should().Be(basketItem);
        }

    }
}
