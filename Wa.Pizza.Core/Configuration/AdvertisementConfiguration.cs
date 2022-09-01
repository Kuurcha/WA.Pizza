using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa.Pizza.Core.Model.Advertisement;

namespace Wa.Pizza.Core.Configuration
{
    internal class AdvertisementClientConfiguration : IEntityTypeConfiguration<AdvertisementClient>
    {
        public void Configure(EntityTypeBuilder<AdvertisementClient> builder)
        {
            builder.HasKey(ac => ac.Id);
            builder.Property(ac => ac.Id).ValueGeneratedOnAdd();

            builder.Property(ac => ac.Website).IsRequired();
            
            builder.Property(ac => ac.ApiKey).IsRequired();
            builder.Property(ac => ac.ApiKey).HasMaxLength(256);


            builder.Property(ac => ac.Name).IsRequired();
            builder.Property(ac => ac.Name).HasMaxLength(256);

            builder.HasMany(ac => ac.Advertisements)
                .WithOne(a => a.AdvertisementClient)
                .HasForeignKey(a => a.AdvertisementClientId);
        }
    }
}
