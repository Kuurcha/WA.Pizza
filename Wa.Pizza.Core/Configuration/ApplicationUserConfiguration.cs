using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.OwnsOne(a => a.refreshToken);

        }
    }
}
