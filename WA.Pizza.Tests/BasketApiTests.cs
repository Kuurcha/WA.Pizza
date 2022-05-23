using FluentAssertions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Wa.Pizza.Infrasctructure.DTO.CatalogItem;
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
/*        private async Task<int> addTestBasketItem()
        {
            var basketItem = new BasketItem { BasketId = testBasket.Id, Quantity = Faker.RandomNumber.Next(1, 100), CatalogType = Core.CatalogType.CatalogType.Pizza, UnitPrice = 150, CatalogItemName = "Classic", CatalogItemId = catalogTestItem.Id };
            _fixture.applicationDbContext.BasketItem.Add(basketItem);
            basketItemId = _fixture.applicationDbContext.BasketItem.Where(bi => bi.BasketId == basketItem.BasketId).FirstOrDefault().Id;
            return await  _fixture.applicationDbContext.SaveChangesAsync();
        }*/
        public BasketDataTests(BasketDataBaseFixture fixture)
        {
            _fixture = fixture;
            _basketDataService = new BasketDataService(_fixture.applicationDbContext);
            _fixture.applicationDbContext.Database.EnsureDeleted();
            _fixture.applicationDbContext.Database.Migrate();
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


      /*  [Fact]
        public async void basket_item_is_deleted_from_basket()
        {
            addTestBasketItem();
            //Arrange
            var basketItem = await _basketDataService.GetBasketItemById(basketItemId);
            //Act
            await _basketDataService.DeleteItem(basketItem);
            //Assert
            var basketItemsById = await _basketDataService.GetBasketItemListByBasketId(1);
            basketItemsById.Should().HaveCount(0);
        }

        [Fact]
        public async void basket_was_updated()
        {
            //Arrange
            addTestBasketItem();
            var basketItem = new BasketItemDTO { Id = basketItemId, BasketId = testBasket.Id, Quantity = Faker.RandomNumber.Next(1, 100), CatalogType = Core.CatalogType.CatalogType.Pizza, UnitPrice = 450, CatalogItemName = "Classic", CatalogItemId = catalogTestItem.Id };
            //Act
            await _basketDataService.UpdateItem(basketItem);
            //Assert
            var updatedBasketItem = await _basketDataService.GetBasketItemById(basketItemId);
            //Be vs Equals?
            updatedBasketItem.Should().Be(basketItem);
        }
*/
    }
}
