using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NameAz)
                .IsRequired()
                .HasMaxLength(75);

            builder.Property(x => x.NameAz)
              .HasMaxLength(75);

            builder.Property(x => x.NameAz)
              .HasMaxLength(75);
        }
    }
}