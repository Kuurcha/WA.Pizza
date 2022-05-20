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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Wa.Pizza.Core.Exceptions;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class BasketDataService : IEntityService<BasketDTO>
    {
        private readonly ApplicationDbContext _context;

        public BasketDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<BasketDTO> GetById(int guid)
        {
            //.Basket.Where(x => x.Id == guid).IProjectToType<BasketDTO>().FirstOrDefaultAsync();
            BasketDTO basket = await _context.Basket.AsNoTracking()
                .Include(b => b.BasketItems)
                .ProjectToType<BasketDTO>()
                .Where(x => x.Id == guid)
                .FirstOrDefaultAsync();
            return basket;
        }

        public async Task<BasketDTO> GetByUserId(int userId)
        {
            BasketDTO basket = await _context.Basket.AsNoTracking()
                .Include(b => b.BasketItems)
                .ProjectToType<BasketDTO>()
                .Where(x => x.ApplicationUserId == userId)
                .FirstOrDefaultAsync();
            return basket;
        }

        //Получение Basket => получение списка 

        /// <summary>
        /// Использовать в контроллере для уменьшения количества обновлений.
        /// </summary>
        /// <param name="basketId"></param> 
        /// <returns></returns>
        public async Task<int> AddItem(BasketItemDTO basketItemDTO, int basketId)
        {
            CatalogItem catalogItem = await _context.CatalogItem.AsNoTracking()
                .Where(ci => ci.Id == basketItemDTO.CatalogItemId)
                .FirstOrDefaultAsync();
            if (catalogItem == null)
                throw new EntityNotFoundException("Catalog item by id: " + basketItemDTO.CatalogItemId + " does not exist");
           
            BasketItem basketItem = await _context.BasketItem.Where(bi => bi.Id == basketItemDTO.Id).FirstOrDefaultAsync();
            Basket basket = await _context.Basket.Where(b => b.Id == basketItemDTO.BasketId).FirstOrDefaultAsync();
            if (basketItem != null)
            {
                basketItem.Quantity = basketItemDTO.Quantity;
                basket.LastModified = DateTime.UtcNow;
                return await _context.SaveChangesAsync();
            }
            else
            {
                basketItem = catalogItem.Adapt<BasketItem>();
                if (basket == null)
                {
                    basket = new Basket { };
                    basketItem.BasketId = basket.Id;
                    _context.Basket.Adapt(basket);
                }
                basketItem.Quantity = basketItemDTO.Quantity;
                basket.BasketItems.Add(basketItem);
                basket.LastModified = DateTime.UtcNow;
                return await _context.SaveChangesAsync();
            }
        }
        public async Task<int> UpdateItem(BasketItemDTO basketItemDTO)
        {
            BasketItem originalBasketItem = await _context.BasketItem.Include(bi => bi.Basket).FirstOrDefaultAsync(x => x.Id == basketItemDTO.Id);
            if (originalBasketItem == null)
                throw new EntityNotFoundException("Basket item with id: " + basketItemDTO.Id + " not found. Nothing to update.");
            if (basketItemDTO.Quantity <= 0)
                _context.BasketItem.Remove(originalBasketItem);
            else
                originalBasketItem.Quantity = basketItemDTO.Quantity;
            originalBasketItem.Basket.LastModified = DateTime.UtcNow;
            return await _context.SaveChangesAsync();

        }
        public async Task<int> DeleteItem(BasketItemDTO basketItemDTO)
        {
            Basket basket = await _context.Basket.Include(b => b.BasketItems).FirstOrDefaultAsync(b => b.Id == basketItemDTO.Id);
            if (basket == null)
                throw new EntityNotFoundException("Basket with id: " + basketItemDTO.BasketId + "does not exists. Unable to delete");
            if (basket.BasketItems.Where(bi => bi.Id == basketItemDTO.Id) == null)
                throw new EntityNotFoundException("BasketItem with id: " + basketItemDTO.Id + "does not exists. Unable to delete");

            basket.LastModified= DateTime.UtcNow;
            var basketItems = basket.BasketItems.Where(bi => bi.Id == basketItemDTO.Id);
            foreach (var basketItem in basketItems)
            {
                basket.BasketItems.Remove(basketItem);
            }
            return await _context.SaveChangesAsync();
        }
        public async Task<int> ClearBasket(BasketDTO basketDTO)
        {
            Basket basket = await _context.Basket.Include(b => b.BasketItems).FirstOrDefaultAsync(b => b.Id == basketDTO.Id);
            if (basket == null)
                throw new EntityNotFoundException("Basket with id: " + basketDTO.Id + "does not exists. Unable to delete");
            basket.BasketItems.Clear();
            return await _context.SaveChangesAsync();
        }
        public async Task<int> BindBuyerToBasket(BasketDTO basketDTO, int applicationUserId)
        {
            Basket basket = await _context.Basket.FirstOrDefaultAsync(b => b.Id == basketDTO.Id);
            if (basket.ApplicationUserId == null)
            {
                basket.ApplicationUserId = applicationUserId;
                return await _context.SaveChangesAsync();
            }
            return 0; //?
        }


    }
}
