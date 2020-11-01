using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class GaleryFileConfiguration : IEntityTypeConfiguration<GaleryFile>
    {
        public void Configure(EntityTypeBuilder<GaleryFile> builder)
        {
            builder.HasKey(x => new
            {
                x.ImageId,
                x.GaleryId
            });
        }
    }
}