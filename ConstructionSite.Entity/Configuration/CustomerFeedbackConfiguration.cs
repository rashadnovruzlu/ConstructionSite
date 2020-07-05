using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class CustomerFeedbackConfiguration : IEntityTypeConfiguration<CustomerFeedback>
    {
        public void Configure(EntityTypeBuilder<CustomerFeedback> builder)
        {
           builder.HasKey(x=>x.Id);
            builder.Property(x=>x.ContentAz)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(x => x.ContentEn)
               .HasMaxLength(500);
            builder.Property(x => x.ContentRu)
               .HasMaxLength(500);
            builder.Property(x=>x.FullName)
                .HasMaxLength(35);
            builder.Property(x=>x.Position)
                .HasMaxLength(50);

        }
    }
   







}
