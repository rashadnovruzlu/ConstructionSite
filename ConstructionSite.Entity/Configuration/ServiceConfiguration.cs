using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {    
           
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.NameAz)
                    .IsRequired()
                        .HasMaxLength(100);
            
            builder.Property(x => x.NameRu)
                    .HasMaxLength(100);

            builder.Property(x => x.NameEn)
                    .HasMaxLength(100);

            builder.Property(x => x.TitleAz)
                    .IsRequired()
                        .HasMaxLength(255);

            builder.Property(x => x.TitleRu)
              .HasMaxLength(255);

            builder.Property(x => x.TitleEn)
              .HasMaxLength(255);

            builder.Property(x => x.ContentAz)
                    .IsRequired();

            builder.Property(x => x.ContentRu);

            builder.Property(x => x.ContentEn);
        }
    }
   







}
