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
        public Task<GetCatalogItemDTO> GetById(int guid)
        {
            return _context.CatalogItem.Where(x => x.Id == guid).ProjectToType <GetCatalogItemDTO>().FirstAsync();
        }
        public Task<List<GetCatalogItemDTO>> getCatalogAsync()
        {
            return _context.CatalogItem.ProjectToType<GetCatalogItemDTO>().ToListAsync();
        }


    }
}
