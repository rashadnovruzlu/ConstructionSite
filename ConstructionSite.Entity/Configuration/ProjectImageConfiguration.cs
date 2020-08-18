using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class ProjectImageConfiguration : IEntityTypeConfiguration<ProjectImage>
    {
        public void Configure(EntityTypeBuilder<ProjectImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsMain).IsRequired();
            builder.Property(x => x.ImageId).IsRequired();
            builder.Property(x => x.ProjectId).IsRequired();
            builder.HasOne(pi => pi.Image)
                                .WithMany(i => i.ProjectImages);
            builder.HasOne(pi => pi.Project)
                                .WithMany(p => p.ProjectImages);
        }
    }
}