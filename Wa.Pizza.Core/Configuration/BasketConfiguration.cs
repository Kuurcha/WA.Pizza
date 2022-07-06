using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Configuration
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.LastModified).IsRequired();

            builder.HasMany(b => b.BasketItems)
                  .WithOne(bi => bi.Basket);

            builder.HasOne(b => b.ApplicationUser)
                  .WithOne(au => au.Basket)
                  .HasForeignKey<Basket>(b => b.ApplicationUserId);

        }
    }
}
