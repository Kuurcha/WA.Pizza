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
    public class CatalogDataService : IEntityService<CatalogItemDTO>
    {
        // Маппинг происходит на уровне сервисов 
        private readonly ApplicationDbContext _context;

        public CatalogDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public Task<CatalogItemDTO> GetById(int guid)
        {
            return _context.CatalogItem.Where(x => x.Id == guid).ProjectToType <CatalogItemDTO>().FirstOrDefaultAsync();
        }
        public Task<List<CatalogItemDTO>> getCatalogAsync()
        {
            return _context.CatalogItem.ProjectToType<CatalogItemDTO>().ToListAsync();
        }


    }
}
