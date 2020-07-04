using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class SubServiceConfiguration : IEntityTypeConfiguration<SubService>
    {
        public void Configure(EntityTypeBuilder<SubService> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.NameAz)
                .IsRequired()
                .HasMaxLength(75);

            builder.Property(x => x.NameRu)
               
               .HasMaxLength(75);

            builder.Property(x => x.NameEn)
               
               .HasMaxLength(75);
            builder.Property(x=>x.ContentAz)
                .IsRequired();

            builder.HasOne(ss => ss.Service)
                                .WithMany(s => s.SubServices);
        
        }
    }
   







}
