using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class ServiceImageCofiguration : IEntityTypeConfiguration<ServiceImage>
    {
        public void Configure(EntityTypeBuilder<ServiceImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageId).IsRequired();
            builder.Property(x => x.ServiceId).IsRequired();

            builder.HasKey(x => new
            {
                x.ImageId,
                x.ServiceId
            });
            builder.HasOne(si => si.Image)
                                .WithMany(i => i.ServiceImages);
            builder.HasOne(si => si.Service)
                                .WithMany(s => s.ServiceImages);
        }
    }
}