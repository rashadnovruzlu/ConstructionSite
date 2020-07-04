using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
           builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Name)
                .HasMaxLength(75);
            builder.Property(x=>x.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Subject)
              .IsRequired()
              .HasMaxLength(150);
            builder.Property(x => x.UserMessage)
            .IsRequired()
            .HasMaxLength(2000);
        }
    }
   







}
