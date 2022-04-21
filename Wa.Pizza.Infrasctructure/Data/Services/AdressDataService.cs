

using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Infrasctructure.DTO.Adress;
using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class AdressDataService : IEntityService<AdressDTO>
    {
        private readonly ApplicationDbContext _context;

        public AdressDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public ApplicationDbContext Get_context()
        {
            return _context;
        }

        public async Task<AdressDTO> GetByIdAsync(int guid)
        {
            Adress adress = (await _context.Adress.FirstOrDefaultAsync(x => x.Id == guid));
            return await adress
                        .BuildAdapter()
                        .AdaptToTypeAsync<AdressDTO>();
        }

    }
}
