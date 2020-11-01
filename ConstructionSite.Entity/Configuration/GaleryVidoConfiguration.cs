using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class GaleryVidoConfiguration : IEntityTypeConfiguration<GaleryVido>
    {
        public void Configure(EntityTypeBuilder<GaleryVido> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("GaleryVido");
            builder.HasOne(x => x.Galery)
                .WithMany(x => x.GaleryVidos);

        }
    }
}