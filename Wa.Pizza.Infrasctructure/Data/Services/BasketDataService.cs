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
            return _context.Basket.Where(x => x.Id == guid).ProjectToType<BasketDTO>().FirstAsync();
        }
        public Task<BasketDTO> GetByApplicationUserId(int applicationUserId)
        {
            return _context.Basket.Where(x => x.ApplicationUserId == applicationUserId).ProjectToType<BasketDTO>().FirstAsync();
        }

        public async Task<int> AddBasket(BasketDTO basketDTO, int applicationUserId)
        {
            Basket basket = await basketDTO
                            .BuildAdapter()
                            .AdaptToTypeAsync<Basket>();
            basket.ApplicationUserId = applicationUserId;
            _context.Basket.Add(basket);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateBasket(BasketDTO basketDTO, int applicationUserId)
        {
            Basket basket = await basketDTO
                .BuildAdapter()
                .AdaptToTypeAsync<Basket>();
            basket.ApplicationUserId = applicationUserId;
            _context.Basket.Update(basket);
            return await _context.SaveChangesAsync();
        }


    }
}
