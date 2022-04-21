using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa.Pizza.Infrasctructure.DTO.CatalogItem;
using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class CatalogItemDataService : IEntityService<GetCatalogItemDTO>
    {
        // Маппинг происходит на уровне сервисов 
        private readonly ApplicationDbContext _context;

        public CatalogItemDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<GetCatalogItemDTO> GetByIdAsync(int guid)
        {
            CatalogItem catalogItem = await _context.CatalogItem.FirstOrDefaultAsync(x => x.Id == guid);
            return await catalogItem
                            .BuildAdapter()
                            .AdaptToTypeAsync<GetCatalogItemDTO>();


        }
        public async Task<List<GetCatalogItemDTO>> getCatalogAsync() { 
            List<CatalogItem>  catalogItems = await _context.CatalogItem.ToListAsync();
            return await catalogItems
                            .BuildAdapter()
                            .AdaptToTypeAsync<List<GetCatalogItemDTO>>();
        }


    }
}
