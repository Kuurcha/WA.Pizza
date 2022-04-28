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

        public Task<BasketItemDTO> GetById(int guid)
        {
            return _context.BasketItem.Where(x => x.Id == guid).ProjectToType<BasketItemDTO>().FirstAsync();
        }

        public Task<BasketItemDTO> GetByBasketId(int basketId)
        { 
            return _context.BasketItem.Where(x => x.BasketId == basketId).ProjectToType<BasketItemDTO>().FirstAsync();
        }
        public Task<List<BasketItemDTO>> GetListByBasketId(int basketId)
        {
            return _context.BasketItem.Where(o => o.BasketId == basketId).ProjectToType<BasketItemDTO>().ToListAsync();
        }

        private async Task<int> UpdateDateBasket(int basketId)
        {
            Basket basket = await _context.Basket.Where(x => x.Id == basketId).FirstAsync();

            basket.LastModified = DateTime.UtcNow;

            _context.Basket.Update(basket);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddBasketItem(BasketItemDTO basketItemDTO, int basketId)
        {
            BasketItem basketItem = basketItemDTO.Adapt<BasketItem>();
            basketItem.BasketId = basketId;
            _context.BasketItem.Add(basketItem);
            await UpdateDateBasket(basketItem.BasketId);    


            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateBasketItem(BasketItemDTO basketItemDTO, int basketItemId)
        {
            ///Переделать?
            BasketItem originalBasketItem = await _context.BasketItem.FirstAsync(x => x.Id == basketItemId);
          
            BasketItem basketItem = basketItemDTO.Adapt<BasketItem>();

            originalBasketItem.Adapt(basketItem);
           _context.BasketItem.Update(originalBasketItem);
            await UpdateDateBasket(originalBasketItem.BasketId);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int basketItemId) {
            BasketItem  basketItem = await _context.BasketItem.FirstAsync(x => x.Id == basketItemId);
            _context.BasketItem.Remove(basketItem);
            await UpdateDateBasket(basketItem.BasketId);
            return await _context.SaveChangesAsync();
        }


    }
}
