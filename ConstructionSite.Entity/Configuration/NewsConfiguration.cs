using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
           builder.HasKey(x=>x.Id);

            builder.Property(x=>x.TittleAz)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.TittleEn)
             .HasMaxLength(150);

            builder.Property(x => x.TittleRu)
             .HasMaxLength(150);

            builder.Property(x=>x.ContentAz)
                .IsRequired();

            builder.Property(x=>x.ContentEn);
            builder.Property(x=>x.ContentRu);
               
        }
    }
   







}
