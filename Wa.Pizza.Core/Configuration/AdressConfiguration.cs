using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Configuration
{
    public class AdressConfiguration : IEntityTypeConfiguration<Adress>
    {
        public void Configure(EntityTypeBuilder<Adress> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.AdressString).IsRequired();
            builder.Property(a => a.AdressString).HasMaxLength(254);


            builder.HasOne(au => au.ApplicationUser)
                  .WithMany(a => a.Adresses)
                  .HasForeignKey(a => a.ApplicationUserId);

            
        }
    }
}
