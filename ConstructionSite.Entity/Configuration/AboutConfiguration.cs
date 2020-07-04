using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class AboutConfiguration : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.HasKey(x=>x.Id);
            builder
              .Property(x => x.TittleAz)
              .IsRequired()
              .HasMaxLength(250);

            builder
            .Property(x => x.TittleRu)
             .HasMaxLength(250)
            .IsRequired();

            builder
            .Property(x => x.TittleEn)
             .HasMaxLength(250)
            .IsRequired();

            builder
                .Property(x => x.ContentAz)
                .IsRequired();
            builder.Property(x=>x.ContentRu);
            builder.Property(x=>x.ContentEn);
                


        }
    }
   







}
