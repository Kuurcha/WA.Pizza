using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    internal class BasketItemDataService : IEntityService<BasketItem>
    {
        private readonly ApplicationDbContext _context;

        public BasketItemDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }



        public async Task<BasketItem> GetByIdAsync(int guid) => await _context.BasketItem.FirstOrDefaultAsync(x => x.Id == guid);

        public async Task<BasketItem> GetBytByBasketId(int basketId) => await _context.BasketItem.FirstOrDefaultAsync(x => x.Id == basketId);

        public async Task<List<BasketItem>> GetListByBasketId(int basketId)
        {
           return await _context.BasketItem.Where(o => o.BasketId == basketId).ToListAsync();
        } 

        public Task<int> AddOrder(BasketItem basketItem)
        {
            _context.BasketItem.Add(basketItem);
            return _context.SaveChangesAsync();
        }
        public async Task<int> UpdateBasketItem(BasketItem basketItem)
        {
            _context.BasketItem.Update(basketItem);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteByIdAsync(int id) {
            BasketItem basketItem = _context.BasketItem.FirstOrDefault(x => x.Id == id);
            _context.BasketItem.Remove(basketItem);
            return await _context.SaveChangesAsync();
        }


    }
}
