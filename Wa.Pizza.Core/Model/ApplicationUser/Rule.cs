using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa.Pizza.Core.Configuration;

namespace Wa.Pizza.Core.Model.ApplicationUser
{
    [Table(nameof(Role))]
    [EntityTypeConfiguration(typeof(RoleConfiguration))]
    public class Role : IdentityRole
    {
    }
}
