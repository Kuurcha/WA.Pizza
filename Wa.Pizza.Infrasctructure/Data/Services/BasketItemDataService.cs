using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa.Pizza.Infrasctructure.Services.Interfaces;
using Mapster;
using Wa.Pizza.Infrasctructure.DTO.Basket;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class BasketItemDataService : IEntityService<BasketItemDTO>
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

        public async Task<BasketItemDTO> GetByIdAsync(int guid)
        {
            BasketItem basketItem = await _context.BasketItem.FirstOrDefaultAsync(x => x.Id == guid);
            return await basketItem
                   .BuildAdapter()
                   .AdaptToTypeAsync<BasketItemDTO>();
        }  

        public async Task<BasketItemDTO> GetByBasketId(int basketId)
        {
            BasketItem basketItem = await _context.BasketItem.FirstOrDefaultAsync(x => x.Id == basketId);
            return await basketItem
                   .BuildAdapter()
                   .AdaptToTypeAsync<BasketItemDTO>();
        } 

        public async Task<List<BasketItemDTO>> GetListByBasketId(int basketId)
        {
            List<BasketItem> basketItems = await _context.BasketItem.Where(o => o.BasketId == basketId).ToListAsync();
            return await basketItems
                   .BuildAdapter()
                   .AdaptToTypeAsync<List<BasketItemDTO>>();
        } 

        private async Task<int> UpdateDateBasket(int basketId)
        {
            Basket basket = await _context.Basket.FirstOrDefaultAsync(x => x.Id == basketId);

            basket.LastModified = DateTime.UtcNow;

            _context.Basket.Update(basket);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddBasketItem(BasketItemDTO basketItemDTO, int basketId)
        {
            BasketItem basketItem = await basketItemDTO
                                          .BuildAdapter()
                                          .AdaptToTypeAsync<BasketItem>();
            
            basketItem.BasketId = basketId;
            _context.BasketItem.Add(basketItem);
            await UpdateDateBasket(basketItem.BasketId);    


            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateBasketItem(BasketItemDTO basketItemDTO, int basketItemId)
        {
            ///Переделать?
            BasketItem originalBasketItem = await _context.BasketItem.FirstOrDefaultAsync(x => x.Id == basketItemId);
          
            BasketItem basketItem = await basketItemDTO
                              .BuildAdapter()
                              .AdaptToTypeAsync<BasketItem>();
            originalBasketItem.Adapt(basketItem);
           _context.BasketItem.Update(originalBasketItem);
            await UpdateDateBasket(originalBasketItem.BasketId);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int basketItemId) {
            BasketItem  basketItem = await _context.BasketItem.FirstOrDefaultAsync(x => x.Id == basketItemId);
            _context.BasketItem.Remove(basketItem);
            await UpdateDateBasket(basketItem.BasketId);
            return await _context.SaveChangesAsync();
        }


    }
}
