using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Configuration
{
    public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Id).ValueGeneratedOnAdd();

            builder.Property(ci => ci.Name).IsRequired();
            builder.Property(ci => ci.Name).HasMaxLength(254);

            builder.Property(ci => ci.Description).IsRequired();
            builder.Property(ci => ci.Description).HasMaxLength(2000);

            builder.Property(ci => ci.Price).HasColumnType("decimal(18,4)");

            builder.Property(ci => ci.CatalogType).IsRequired();

            builder.HasMany(ci => ci.BasketItems)
                  .WithOne(bi => bi.CatalogItem);

            builder.HasMany(ci => ci.OrderItems)
                  .WithOne(oi => oi.CatalogItem);
        }
    }
}
