using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class BasketDataService : IEntityService<Basket>
    {
        private readonly ApplicationDbContext _context;

        public BasketDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }


        public async Task<Basket> GetByIdAsync(int guid) => await _context.Basket.FirstOrDefaultAsync(x => x.Id == guid);

        public async Task<Basket> GetByApplicationUserId(int applicationUserId) => await _context.Basket.FirstOrDefaultAsync(x => x.ApplicationUserId == applicationUserId);

        public async Task<int> AddBasket(Basket basket)
        {
            _context.Basket.Add(basket);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateBasket(Basket basket)
        {
            _context.Basket.Update(basket);
            return await _context.SaveChangesAsync();
        }


    }
}
