using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    internal class GaleryConfiguration : IEntityTypeConfiguration<Galery>
    {
        public void Configure(EntityTypeBuilder<Galery> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TitleAz)
                    .IsRequired()
                        .HasMaxLength(250);

            builder.Property(x => x.TitleEn)
                    .HasMaxLength(250);

            builder.Property(x => x.TitleRu);
        }
    }
}