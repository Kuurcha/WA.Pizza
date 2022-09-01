using FluentAssertions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.CatalogItem;
using Wa.Pizza.Infrasctructure.Validators;
using WA.Pizza.Core.CatalogType;
using Xunit;

namespace WA.Pizza.Tests
{
    [Collection("Test database collection")]
    public class CatalogItemApiTest: BaseDatabaseTestClass
    {
#nullable disable
        private readonly CatalogDataService _catalogDataService;
        private CatalogItemValidator _catalogItemValidator;

        private CatalogItem _catalogItemTest = new CatalogItem { CatalogType = CatalogType.Pizza, Name = "TestCatalogObject", Price = 666, Quantity = 15, Description = "TestDecription" };


        // TODO: OrderItems
        private async Task AddTestBasketAndOrderItems()
        {

           
            _catalogItemTest = applicationDbContext.CatalogItem.Add(_catalogItemTest).Entity;
            await applicationDbContext.SaveChangesAsync();
            Basket basket = applicationDbContext.Basket.Add(new Basket { LastModified = new DateTime(2066, 11, 11) }).Entity;

            var basketItems = new List<BasketItem>()
            {
                new BasketItem { Quantity = Faker.RandomNumber.Next(1, 100), CatalogType = _catalogItemTest.CatalogType, UnitPrice = _catalogItemTest.Price, CatalogItemName = _catalogItemTest.Name, CatalogItem = _catalogItemTest },
                new BasketItem { Quantity = Faker.RandomNumber.Next(1, 100), CatalogType = _catalogItemTest.CatalogType, UnitPrice = _catalogItemTest.Price, CatalogItemName = _catalogItemTest.Name, CatalogItem = _catalogItemTest }
             };
            basket.BasketItems = basketItems;
            applicationDbContext.Basket.Add(basket);
            await  applicationDbContext.SaveChangesAsync();
    
        }
        public CatalogItemApiTest() : base()
        {
            _catalogItemValidator = new CatalogItemValidator();
            _catalogDataService = new CatalogDataService(applicationDbContext, _catalogItemValidator);
            // applicationDbContext.Database.Migrate();
        }
        [Fact]
        public async void catalog_item_is_added()
        {
            //Arrange
            var catalogItemDTO = _catalogItemTest.Adapt<CatalogItemDTO>();
            var beforeAddingItemCount = await   applicationDbContext.CatalogItem.AsNoTracking().CountAsync();
            //Act
            await _catalogDataService.AddItem(catalogItemDTO);
            //Assert
            var afterAddingItemCount = await  applicationDbContext.CatalogItem.AsNoTracking().CountAsync();
            afterAddingItemCount.Should().Be(beforeAddingItemCount + 1);
        }
        // TODO: OrderItems
        [Fact]
        public async void catalog_item_is_updated()
        {

            //Arrange
            _catalogItemTest =   applicationDbContext.CatalogItem.Add(_catalogItemTest).Entity;
            await  applicationDbContext.SaveChangesAsync();
            var catalogItemDTO = _catalogItemTest.Adapt<CatalogItemDTO>();
            catalogItemDTO.Description = "New test description";
            //Act
            await _catalogDataService.UpdateItem(catalogItemDTO);
            //Assert
            var catalogItemAfterUpdating = await  applicationDbContext.CatalogItem.FirstOrDefaultAsync(x => x.Id == _catalogItemTest.Id);
            catalogItemAfterUpdating.Description.Should().Be(catalogItemDTO.Description);
        }

        [Fact]
        public async void catalog_item_is_deleted_with_basketItems()
        {
            //Arrange
            await AddTestBasketAndOrderItems();
            int basketItemsCount = await  applicationDbContext.BasketItem.AsNoTracking().Where(bi => bi.CatalogItemId == _catalogItemTest.Id).CountAsync();
            int catalogItemCount = await  applicationDbContext.CatalogItem.AsNoTracking().CountAsync();
            var catalogItemToDeleteDTO = _catalogItemTest.Adapt<CatalogItemDTO>();
            //Act
            await _catalogDataService.DeleteItem(catalogItemToDeleteDTO);
            //Assert
            int afterDeletionBasketItemsCount = await  applicationDbContext.BasketItem.AsNoTracking().Where(bi => bi.CatalogItemId == _catalogItemTest.Id).CountAsync();
            int afterDeletionCatalogItemCount = await  applicationDbContext.CatalogItem.AsNoTracking().CountAsync();

            afterDeletionBasketItemsCount.Should().Be(basketItemsCount - 2);
            afterDeletionCatalogItemCount.Should().Be(catalogItemCount - 1);
        }

    }
}
