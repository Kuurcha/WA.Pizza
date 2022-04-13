using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class CatalogItemDataService : IEntityService<CatalogItem>
    {
        private readonly ApplicationDbContext _context;

        public CatalogItemDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public async Task<CatalogItem> GetByIdAsync(int guid) => await _context.CatalogItem.FirstOrDefaultAsync(x => x.Id == guid);

        public async Task<List<CatalogItem>> getCatalogAsync() { 
            return await _context.CatalogItem.ToListAsync();
        }


    }
}
