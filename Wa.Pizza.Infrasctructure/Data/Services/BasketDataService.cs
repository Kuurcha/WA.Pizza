using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Wa.Pizza.Infrasctructure.Services.Interfaces;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Mapster;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class BasketDataService : IEntityService<BasketDTO>
    {
        private readonly ApplicationDbContext _context;

        public BasketDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }


        public Task<BasketDTO> GetById(int guid)
        {
            return _context.Basket.Where(x => x.Id == guid).ProjectToType<BasketDTO>().FirstOrDefaultAsync();
        }
        public Task<BasketDTO> GetByApplicationUserId(int applicationUserId)
        {
            return _context.Basket.Where(x => x.ApplicationUserId == applicationUserId).ProjectToType<BasketDTO>().FirstOrDefaultAsync();
        }
        public Task<BasketItemDTO> GetBasketItemById(int basketItemId)
        {
            return _context.BasketItem.Where(x => x.Id == basketItemId).ProjectToType<BasketItemDTO>().FirstOrDefaultAsync();
        }
        public Task<List<BasketItemDTO>> GetBasketItemListByBasketId(int basketId)
        {
            return _context.BasketItem.Where(o => o.BasketId == basketId).ProjectToType<BasketItemDTO>().ToListAsync();
        }
        /// <summary>
        /// Использовать в контроллере для уменьшения количества обновлений.
        /// </summary>
        /// <param name="basketId"></param>
        /// <returns></returns>
        public async Task<int> UpdateDateBasket(int basketId)
        {
            Basket basket = await _context.Basket.FirstOrDefaultAsync(x => x.Id == basketId);

            basket.LastModified = DateTime.UtcNow;

            _context.Basket.Update(basket);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> AddItem(BasketItemDTO basketItemDTO)
        {
            BasketItem basketItem = basketItemDTO.Adapt<BasketItem>();
            _context.BasketItem.Add(basketItem);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateItem(BasketItemDTO basketItemDTO)
        {
            BasketItem originalBasketItem = await _context.BasketItem.FirstOrDefaultAsync(x => x.Id == basketItemDTO.Id);
            BasketItem basketItem = basketItemDTO.Adapt<BasketItem>();
            originalBasketItem.Adapt(basketItem);
            _context.BasketItem.Update(originalBasketItem);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteItem(BasketItemDTO basketItemDTO)
        {
            BasketItem basketItem = await _context.BasketItem.FirstOrDefaultAsync(x => x.Id == basketItemDTO.Id);
            _context.BasketItem.Remove(basketItem);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> ClearBasket(int basketId)
        {
            List<BasketItemDTO> basketItemsToClear = await GetBasketItemListByBasketId(basketId);
            foreach (var basketItem in basketItemsToClear)
            {
                await DeleteItem(basketItem);
            }
            return await _context.SaveChangesAsync();
        }
        public async Task<int> BindBuyetToBasket(int basketId, int applicationUserId)
        {
            Basket basket = await _context.Basket.FirstOrDefaultAsync(x => x.Id == basketId );
            basket.ApplicationUserId = applicationUserId;
            _context.Basket.Update(basket);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddBasket(BasketDTO basketDTO)
        {
            Basket basket = basketDTO.Adapt<Basket>();
            _context.Basket.Add(basket);
            return await _context.SaveChangesAsync();
        }   
    }
}
