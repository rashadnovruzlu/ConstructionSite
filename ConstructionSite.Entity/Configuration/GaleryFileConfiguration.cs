using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Configuration
{
    public class GaleryFileConfiguration : IEntityTypeConfiguration<GaleryFile>
    {
        public void Configure(EntityTypeBuilder<GaleryFile> builder)
        {
            builder.Property(x => x.Type)
                    .IsRequired();
            builder.HasKey(x => new
            {
                x.ImageId,
                x.GaleryId
            });
        }
    }
}
