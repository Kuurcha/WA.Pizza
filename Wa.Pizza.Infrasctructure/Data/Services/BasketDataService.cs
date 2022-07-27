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
using Wa.Pizza.Infrasctructure.Validators;
using FluentValidation.Results;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
#nullable disable
    public class BasketDataService : IEntityService<BasketDTO> 
    {
        private readonly ApplicationDbContext _context;
        private readonly BasketItemValidator _basketItemValidator;
        public BasketDataService(ApplicationDbContext ctx, BasketItemValidator basketItemValidator )
        {
           _context = ctx;
           _basketItemValidator = basketItemValidator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Task<BasketDTO> GetById(int guid)
        {
            //.Basket.Where(x => x.Id == guid).IProjectToType<BasketDTO>().FirstOrDefaultAsync();
            Task<BasketDTO> basket = _context.Basket.AsNoTracking()
                                              .Include(b => b.BasketItems)
                                              .ProjectToType<BasketDTO>()
                                              .Where(x => x.Id == guid)
                                              .FirstOrDefaultAsync();
            if (basket == null)
                throw new EntityNotFoundException("basket with user id: " + guid + " does not exist");
            return basket;
        }

        public Task<BasketDTO> GetByUserId(string userId)
        {
            Task<BasketDTO> basket = _context.Basket.AsNoTracking()
                                              .Include(b => b.BasketItems)
                                              .ProjectToType<BasketDTO>()
                                              .Where(x => x.ApplicationUserId == userId)
                                              .FirstOrDefaultAsync();
            if (basket == null)
                throw new EntityNotFoundException("basket with user id: " + userId + " does not exist");
            return basket;
        }

      
        /// <summary>
        /// Использовать в контроллере для уменьшения количества обновлений.
        /// </summary>
        /// <param name="basketId"></param> 
        /// <returns></returns>
        public async Task<int> AddItem(BasketItemDTO basketItemDTO)
        {

                CatalogItem catalogItem = await _context.CatalogItem
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(ci => ci.Id == basketItemDTO.CatalogItemId);

                if (catalogItem == null)
                    throw new EntityNotFoundException("Catalog item by id: " + basketItemDTO.CatalogItemId + " does not exist");
                BasketItem basketItem = await _context.BasketItem
                                        .FirstOrDefaultAsync(bi => bi.Id == basketItemDTO.Id);

                Basket basket = await _context.Basket
                    .Include(b => b.BasketItems)
                    .FirstOrDefaultAsync(b => b.Id == basketItemDTO.BasketId);

                if (basketItem != null)
                {
                    basketItem.Quantity = basketItemDTO.Quantity;
                    basket!.LastModified = DateTime.UtcNow;
                    return await _context.SaveChangesAsync();
                }
                else
                {
                    //Мапстер перемапить?
                    basketItem = catalogItem.Adapt<BasketItem>();
                    basketItem.Id = 0;
                    basketItem.Quantity = basketItemDTO.Quantity;
                    basketItem.CatalogItemName = catalogItem.Name;
                    basketItem.CatalogItemId = catalogItem.Id;
                }


                if (basket == null)
                {
                    basket = new Basket { };
                    basketItem.BasketId = basket.Id;
                    _context.Basket.Adapt(basket);
                    _context.Basket.Attach(basket).State = EntityState.Added;
                }

                if (basket!.BasketItems == null)
                    basket.BasketItems = new List<BasketItem>();
                basket.BasketItems.Add(basketItem);
                basket.LastModified = DateTime.UtcNow;
                return await _context.SaveChangesAsync();
    


        }
        public async Task<int> UpdateItem(BasketItemDTO basketItemDTO)
        {


            BasketItem originalBasketItem = await _context.BasketItem.Include(bi => bi.Basket)
                                                                       .FirstOrDefaultAsync(x => x.Id == basketItemDTO.Id);
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

            Basket basket = await _context.Basket.Include(b => b.BasketItems).FirstOrDefaultAsync(b => b.Id == basketItemDTO.BasketId);
            if (basket == null)
                throw new EntityNotFoundException("Basket with id: " + basketItemDTO.BasketId + "does not exists. Unable to delete");
            if (basket.BasketItems == null || basket.BasketItems.Where(bi => bi.Id == basketItemDTO.Id) == null)
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
            if (basket.BasketItems != null)
                basket.BasketItems.Clear();
            return await _context.SaveChangesAsync();
        }
        public async Task<int> BindBuyerToBasket(BasketDTO basketDTO, string applicationUserId)
        {
            Basket basket = await _context.Basket.FirstOrDefaultAsync(b => b.Id == basketDTO.Id);
            if (basket != null)
            {
                ApplicationUser applicationUser = await _context.ApplicationUser.FirstOrDefaultAsync(a => a.Id == applicationUserId);
                if (basket.ApplicationUser == null)
                {
                    basket.ApplicationUser = applicationUser;
                    return await _context.SaveChangesAsync();
                }
            }
            return 0; //?
        }

    }
}
