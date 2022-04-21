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


        public async Task<BasketDTO> GetByIdAsync(int guid)
        {
            Basket basket = await _context.Basket.FirstOrDefaultAsync(x => x.Id == guid);
            return await basket
                   .BuildAdapter()
                   .AdaptToTypeAsync<BasketDTO>();
        } 

        public async Task<BasketDTO> GetByApplicationUserId(int applicationUserId)
        {
            Basket basket = await _context.Basket.FirstOrDefaultAsync(x => x.ApplicationUserId == applicationUserId);
            return await basket
                        .BuildAdapter()
                        .AdaptToTypeAsync<BasketDTO>();
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
