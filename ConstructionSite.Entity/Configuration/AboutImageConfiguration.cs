using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class AboutImageConfiguration : IEntityTypeConfiguration<AboutImage>
    {
        public void Configure(EntityTypeBuilder<AboutImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AboutId).IsRequired();
            builder.Property(x => x.ImageId).IsRequired();

            builder.HasOne(ai => ai.About)
                                .WithMany(a => a.AboutImages);
            builder.HasOne(ai => ai.Image)
                               .WithMany(i => i.AboutImages);
        }
    }
}