using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class SliderImageConfiguration : IEntityTypeConfiguration<SliderImage>
    {
        public void Configure(EntityTypeBuilder<SliderImage> builder)
        {
            //builder.HasOne(x => x.Sliders)
            //    .WithMany(x => x.SliderImages)
            //    ;
            builder.HasOne(x => x.Image)
                 .WithMany(x => x.SliderImages)
                 ;
        }
    }
}