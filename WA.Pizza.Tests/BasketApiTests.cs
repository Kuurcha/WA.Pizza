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
using Wa.Pizza.Infrasctructure.Validators;
using Xunit;

namespace WA.Pizza.Tests
{
#nullable disable
    [Collection("Test database collection")]
    public class BasketDataTests: BaseDatabaseTestClass
    {
        private readonly BasketDataService _basketDataService;
        private BasketItemValidator _basketItemValidator;

        private Basket basketTest;
        private CatalogItem catalogItemTest;
        private void data_seeding()
        {

            basketTest = applicationDbContext.Basket.Add(new Basket { LastModified = new DateTime(2066, 11, 11) }).Entity;
            catalogItemTest = applicationDbContext.CatalogItem.Add( new CatalogItem { Quantity = 150, Name = "Cheeze", Description = "Classic", Price = 150, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Pizza }).Entity;
            applicationDbContext.SaveChanges();
        }
        private async Task<int> addTestBasketItemAsync()
        {
            var basketItem = new BasketItem { BasketId = basketTest.Id, Quantity = Faker.RandomNumber.Next(1, 100), CatalogType = Core.CatalogType.CatalogType.Pizza, UnitPrice = 150, CatalogItemName = "Classic", CatalogItemId = catalogItemTest.Id };
            applicationDbContext.BasketItem.Add(basketItem);
            return await applicationDbContext.SaveChangesAsync();
        }
        public BasketDataTests(): base()
        {
            _basketItemValidator = new BasketItemValidator();
            _basketDataService = new BasketDataService(applicationDbContext, _basketItemValidator);
            //applicationDbContext.Database.Migrate();
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
            await addTestBasketItemAsync();
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
            await addTestBasketItemAsync();
            var basket = await _basketDataService.GetById(basketTest.Id);
            var basketItems = basket.BasketItems;
            var basketItem = basketItems.Last();
            basketItem.Quantity = 666;
            BasketItemDTO basketItemDTO = basketItem.Adapt<BasketItemDTO>();

            //Act
            await _basketDataService.UpdateItem(basketItem);
            //Assert
            Basket updatedBasket = await applicationDbContext.Basket.AsNoTracking().Include(bi => bi.BasketItems).FirstOrDefaultAsync(bi => bi.Id == basketTest.Id);
            updatedBasket.BasketItems.Last().Adapt<BasketItemDTO>().Should().Be(basketItem);
        }

        [Fact]
        public async void basket_was_cleared()
        {
            //Arrange
            await addTestBasketItemAsync();
            BasketDTO basketToDelete = basketTest.Adapt<BasketDTO>(); 
            //Act
            await _basketDataService.ClearBasket(basketToDelete);
            //Assert
            Basket updatedBasket = await applicationDbContext.Basket.AsNoTracking().Include(bi => bi.BasketItems).FirstOrDefaultAsync(bi => bi.Id == basketTest.Id);
            updatedBasket.BasketItems.Count.Should().Be(0);
        }

        [Fact]
        public async void user_was_binded_to_basket()
        {
            //Arrange
            await addTestBasketItemAsync();
            ApplicationUser user = new ApplicationUser();
            user = applicationDbContext.ApplicationUser.Add(user).Entity;
            await applicationDbContext.SaveChangesAsync();
            BasketDTO basketToBind = basketTest.Adapt<BasketDTO>();
            //Act
            await _basketDataService.BindBuyerToBasket(basketToBind, user.Id);
            //Assert
            Basket bindedBasket = await applicationDbContext.Basket.AsNoTracking().Include(b => b.ApplicationUser).FirstOrDefaultAsync(bi => bi.Id == basketTest.Id);
            bindedBasket.ApplicationUser.Should().NotBeNull();
        }

    }
}
