using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class SlideConfiguration : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Img)
                    .IsRequired();

            builder.Property(x => x.TitleAz)
                    .HasMaxLength(250)
                        .IsRequired();

            builder.Property(x => x.TitleRu)
                    .HasMaxLength(250);

            builder.Property(x => x.TitleEn)
                    .HasMaxLength(250);
        }
    }
}