using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenConfiguration>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenConfiguration> builder)
        {
          
        }
    }
}
