using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class BasketItemDataService : IEntityService<BasketItem>
    {
        private readonly ApplicationDbContext _context;

        public BasketItemDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public BasketItem catalogItemToBasketItem(CatalogItem catalogItem, int basketId, int quantity)
        {
            return new BasketItem { BasketId = basketId, CatalogItemId = catalogItem.Id, CatalogType = catalogItem.CatalogType, Quantity = quantity, UnitPrice = catalogItem.Price, CatalogItemName = catalogItem.Name };
        }

        public async Task<BasketItem> GetByIdAsync(int guid) => await _context.BasketItem.FirstOrDefaultAsync(x => x.Id == guid);

        public async Task<BasketItem> GetBytByBasketId(int basketId) => await _context.BasketItem.FirstOrDefaultAsync(x => x.Id == basketId);

        public async Task<List<BasketItem>> GetListByBasketId(int basketId)
        {
           return await _context.BasketItem.Where(o => o.BasketId == basketId).ToListAsync();
        } 

        private async Task<int> UpdateDateBasket(int basketId)
        {
            Basket basket = await _context.Basket.FirstOrDefaultAsync(x => x.Id == basketId);

            basket.LastModified = DateTime.UtcNow;

            _context.Basket.Update(basket);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddBasketItem(BasketItem basketItem)
        {
            _context.BasketItem.Add(basketItem);
            await UpdateDateBasket(basketItem.Id);    


            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateBasketItem(BasketItem basketItem)
        {
            _context.BasketItem.Update(basketItem);
            await UpdateDateBasket(basketItem.Id);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(BasketItem basketItem) {
            _context.BasketItem.Remove(basketItem);
            await UpdateDateBasket(basketItem.Id);
            return await _context.SaveChangesAsync();
        }


    }
}
