using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Configuration
{
    public class DescriptionConfiguration : IEntityTypeConfiguration<Description>
    {
        public void Configure(EntityTypeBuilder<Description> builder)
        {
           builder.HasKey(x=>x.Id);
            builder.Property(x=>x.TittleAz)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.TittleEn)
                
                .HasMaxLength(150);
            builder.Property(x => x.TittleRu)

             .HasMaxLength(150);
            builder.Property(x=>x.ContentAz)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(x => x.ContentRu)
             
              .HasMaxLength(500);
            builder.Property(x => x.ContentEn)
             
              .HasMaxLength(500);

            
        }
    }
}
