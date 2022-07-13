using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.CreationDate).IsRequired();
            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.Description).HasMaxLength(2000);

            builder.HasMany(o => o.OrderItems)
                  .WithOne(oi => oi.Order);
            builder.HasOne(o => o.ApplicationUser)
                 .WithMany(a => a.Orders)
                 .HasForeignKey(o => o.ApplicationUserId);
        }
    }
}
