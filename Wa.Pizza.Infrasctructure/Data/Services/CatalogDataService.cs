using FluentValidation.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Infrasctructure.DTO.CatalogItem;
using Wa.Pizza.Infrasctructure.Services.Interfaces;
using Wa.Pizza.Infrasctructure.Validators;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class CatalogDataService : IEntityService<CatalogItemDTO>
    {
        // Маппинг происходит на уровне сервисов 
        private readonly ApplicationDbContext _context;
        private CatalogItemValidator _catalogItemValidator;


        public CatalogDataService(ApplicationDbContext ctx, CatalogItemValidator catalogItemValidator)
        {
            _context = ctx;
            _catalogItemValidator = catalogItemValidator;

        }

        private async Task<bool> validateDTOAsync(CatalogItemDTO catalogItemDTO)
        {

            ValidationResult results = await _catalogItemValidator.ValidateAsync(catalogItemDTO.Adapt<CatalogItem>());
            List<ValidationFailure> failures = results.Errors;
            string errorString = "";
            foreach (ValidationFailure failure in failures)
            {
                errorString += System.Environment.NewLine + failure.ErrorMessage;
            }
            throw new WrongDataFormatException(errorString);
            return results.IsValid;
        }

        public Task<CatalogItemDTO> GetById(int guid)
        {
            Task<CatalogItemDTO> catalogItemDTO = _context.CatalogItem.Where(x => x.Id == guid).ProjectToType<CatalogItemDTO>().FirstOrDefaultAsync();
            if (catalogItemDTO == null)
                throw new EntityNotFoundException("CatalogItem with id: " + guid + "is not found");
            return catalogItemDTO;
        }
        public Task<List<CatalogItemDTO>> getCatalogAsync()
        {
            return _context.CatalogItem.ProjectToType<CatalogItemDTO>().ToListAsync();
        }

        public async Task<int> AddItem(CatalogItemDTO catalogItemDTO)
        {
            await validateDTOAsync(catalogItemDTO);
            _context.CatalogItem.Add(catalogItemDTO.Adapt<CatalogItem>());
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateItem(CatalogItemDTO catalogItemDTO)
        {
            await validateDTOAsync(catalogItemDTO);
            CatalogItem originalCatalogItem = await _context.CatalogItem
                .FirstOrDefaultAsync(x => x.Id == catalogItemDTO.Id);

            if (originalCatalogItem == null)
                throw new EntityNotFoundException("Catalog item with id: " + catalogItemDTO.Id + " was not found, unable to update");

            CatalogItem updatedCatalogItem = catalogItemDTO.Adapt<CatalogItem>();

            //Заменить на DTO?
            originalCatalogItem.CatalogType = catalogItemDTO.CatalogType;
            originalCatalogItem.Description = catalogItemDTO.Description;
            originalCatalogItem.Price = catalogItemDTO.Price;
            originalCatalogItem.Quantity = catalogItemDTO.Quantity;
            
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteItem(CatalogItemDTO catalogItemDTO)
        {
            await validateDTOAsync(catalogItemDTO);
            CatalogItem catalogItem = await _context.CatalogItem
                .Include(ci => ci.BasketItems).Include(ci => ci.OrderItems).
                FirstOrDefaultAsync(x => x.Id == catalogItemDTO.Id);

            if (catalogItem == null)
                throw new EntityNotFoundException("Catalog item with id: " + catalogItemDTO.Id + " was not found, unable to delete");

            if (catalogItem.BasketItems != null)
                foreach (BasketItem basketItem in catalogItem.BasketItems)
                    _context.BasketItem.Remove(basketItem);
                    //basketItem.Id = -1;

            _context.CatalogItem.Remove(catalogItem);
            return await _context.SaveChangesAsync();
        }



    }
}
