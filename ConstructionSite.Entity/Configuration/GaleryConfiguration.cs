using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Configuration
{
    class GaleryConfiguration : IEntityTypeConfiguration<Galery>
    {
        public void Configure(EntityTypeBuilder<Galery> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TitleAz)
                    .IsRequired()
                        .HasMaxLength(250);

            builder.Property(x => x.TitleEn)
                    .HasMaxLength(250);

            builder.Property(x => x.TitleRu)
                    .HasMaxLength(250);

            
        }
    }
}
