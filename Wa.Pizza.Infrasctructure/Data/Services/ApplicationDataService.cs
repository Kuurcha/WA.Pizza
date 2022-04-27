﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Wa.Pizza.Infrasctructure.Services.Interfaces;
using Wa.Pizza.Infrasctructure.DTO.ApplicationUser;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class ApplicationUserDataService : IEntityService<ApplicationUserDTO>
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public Task<ApplicationUserDTO> GetById(int guid) => _context.ApplicationUser.Where(x => x.Id == guid).ProjectToType<ApplicationUserDTO>().FirstAsync();

        

    }
}
