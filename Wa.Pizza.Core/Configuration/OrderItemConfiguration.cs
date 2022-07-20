using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Configuration
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Id).ValueGeneratedOnAdd();
            builder.Property(oi => oi.CatalogItemId).IsRequired();
            builder.Property(oi => oi.OrderId).IsRequired();

            builder.Property(oi => oi.CatalogItemName).IsRequired();
            builder.Property(oi => oi.CatalogItemName).HasMaxLength(254);

            builder.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,4)");
            builder.Property(oi => oi.Discount).HasColumnType("decimal(18,4)");

            builder.HasOne(oi => oi.Order)
                  .WithMany(o => o.OrderItems)
                  .HasForeignKey(oi => oi.OrderId);
            builder.HasOne(oi => oi.CatalogItem)
                  .WithMany(c => c.OrderItems)
                  .HasForeignKey(oi => oi.CatalogItemId);
        }
    }
}
