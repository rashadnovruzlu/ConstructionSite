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
            builder.Property(x=>x.Title)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x=>x.Content)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(x => x.SubService)
                      .WithMany(s => s.Descriptions);
        }
    }
}
