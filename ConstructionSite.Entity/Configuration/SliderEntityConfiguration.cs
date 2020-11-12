using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class SliderEntityConfiguration : IEntityTypeConfiguration<Sliders>
    {
        public void Configure(EntityTypeBuilder<Sliders> builder)
        {
           
            builder.Property(x => x.ImagePath)
                   .HasMaxLength(300);
        }
    }
}
