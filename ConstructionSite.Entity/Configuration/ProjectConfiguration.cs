using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(x => x.NameAz)
               .IsRequired()
               .HasMaxLength(75);

            builder.Property(x => x.NameRu)
           .HasMaxLength(75);

            builder.Property(x => x.NameEn)
              .HasMaxLength(75);

            builder.Property(x => x.ContentAz)
                .IsRequired();

            builder.HasOne(p => p.Portfolio)
                                .WithMany(p => p.Projects);
        }
    }
   







}
