using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    internal class PortfolioImageConfiguration : IEntityTypeConfiguration<PortfolioImage>
    {
        public void Configure(EntityTypeBuilder<PortfolioImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageId).IsRequired();
            builder.Property(x => x.PortfolioId).IsRequired();
            builder.HasOne(pi => pi.Image)
                                .WithMany(i => i.PortfolioImages);
            builder.HasOne(pi => pi.Portfolio)
                                .WithMany(p => p.PortfolioImages);
        }
    }
}