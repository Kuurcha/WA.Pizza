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
    internal class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.AdvertisementClientId).IsRequired();

            builder.Property(a => a.RedicrectURL).IsRequired();

            builder.Property(a => a.ImageURL).IsRequired();
           
        }
    }
}
