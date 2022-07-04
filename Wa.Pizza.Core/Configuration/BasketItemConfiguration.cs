using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Configuration
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(bi => bi.Id);
            builder.Property(bi => bi.Id).ValueGeneratedOnAdd();

            builder.Property(bi => bi.CatalogItemName).IsRequired();
            builder.Property(bi => bi.CatalogItemName).HasMaxLength(30);

            builder.Property(bi => bi.UnitPrice).IsRequired();
            builder.Property(bi => bi.UnitPrice).HasColumnType("decimal(18,4)");

            builder.Property(bi => bi.CatalogType).IsRequired();
            builder.Property(bi => bi.Quantity).IsRequired();
            builder.Property(bi => bi.BasketId).IsRequired();
            builder.Property(bi => bi.CatalogItemId).IsRequired();

            builder.HasOne(bi => bi.CatalogItem)
                  .WithMany(ci => ci.BasketItems)
                  .HasForeignKey(bi => bi.CatalogItemId);
        }
    }
}
