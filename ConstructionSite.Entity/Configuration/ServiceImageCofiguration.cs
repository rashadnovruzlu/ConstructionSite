using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Configuration
{
    public class ServiceImageCofiguration : IEntityTypeConfiguration<ServiceImage>
    {
        public void Configure(EntityTypeBuilder<ServiceImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageId).IsRequired();
            builder.Property(x => x.ServiceId).IsRequired();
            builder.HasOne(pi => pi.Image)
                                .WithMany(i => i.ServiceImages);
            builder.HasOne(pi => pi.Service)
                                .WithMany(p => p.ServiceImages);
        }
    }
}
