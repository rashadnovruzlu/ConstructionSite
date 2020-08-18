using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class SubServiceImageConfiguration : IEntityTypeConfiguration<SubServiceImage>
    {
        public void Configure(EntityTypeBuilder<SubServiceImage> builder)
        {
            builder.HasOne(ssi => ssi.Image)
                                 .WithMany(i => i.SubServiceImages);
            builder.HasOne(ssi => ssi.SubService)
                                .WithMany(ss => ss.SubServiceImages);
        }
    }
}