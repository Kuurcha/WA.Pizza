using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Xunit;
using static WA.Pizza.Tests.BasketTests;

namespace WA.Pizza.Tests
{
    public class BasketTests
    {
        public class BasketDataBaseFixture : IDisposable
        {
            public ApplicationDbContext applicationDbContext { get; private set; }
            public BasketDataBaseFixture()
            {
                 var config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings_test.json")
                                    .Build();
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseSqlServer(config.GetConnectionString("Test"));
                applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);
                // ... initialize data in the test database ...
            }

            public void Dispose()
            {
                applicationDbContext.Database.EnsureDeleted();
            }

            public ApplicationDbContext dbContext { get; private set; }
        }
    }
    public class BasketDataTests: IClassFixture<BasketDataBaseFixture>
    {
        private readonly BasketDataBaseFixture _fixture;
        private readonly BasketDataService _basketDataService;
        public BasketDataTests(BasketDataBaseFixture fixture)
        {
            _fixture = fixture;
            _basketDataService = new BasketDataService(_fixture.applicationDbContext);
        }

        private void clearDataBase()
        {
            _fixture.applicationDbContext.Database.EnsureDeleted();
            _fixture.applicationDbContext.Database.Migrate();
        }
        [Fact]
        public async void basket_item_is_added_to_basket()
        {
            clearDataBase();
            //Тест завязан на OnModelCreating, хрупкий??
            //Arrange
            var basketItem = new BasketItemDTO {BasketId =1, Quantity = Faker.RandomNumber.Next(1, 100), CatalogType = Core.CatalogType.CatalogType.Pizza, UnitPrice = 150, CatalogItemName = "Classic", CatalogItemId = 3 };
            //Act
            await _basketDataService.AddItem(basketItem);
            //Assert
            var basketItemsById = await _basketDataService.GetBasketItemListByBasketId(1);
            basketItemsById.Should().HaveCount(4);
        }

        //Долго ли удалять и создавать в каждом тесте базу?
        //Лучше ли чтоб один зависел от другого?
        //Нужны ли тестовые данные, создать ли новый контекст с минимумом тестовых данных?
        [Fact]
        public async void basket_item_is_deleted_from_basket()
        {
            clearDataBase();
            //Arrange
            var basketItems = await _basketDataService.GetBasketItemListByBasketId(1);
            //Act
            await _basketDataService.DeleteItem(basketItems[0]);
            //Assert
            var basketItemsById = await _basketDataService.GetBasketItemListByBasketId(1);
            basketItemsById.Should().HaveCount(2);
        }

        [Fact]
        public async void basket_was_updated()
        {
            clearDataBase();
            //Arrange
            var basketItemDTO = await _basketDataService.GetBasketItemById(1);

            //Act
            await _basketDataService.UpdateItem(basketItemDTO);
            //Assert
            var updatedBasketItem = await _basketDataService.GetBasketItemById(1);
            //Be vs Equals?
            updatedBasketItem.Should().Be(basketItemDTO);
        }

    }
}
