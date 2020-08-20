using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TittleAz)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.TittleRu)
                .HasMaxLength(150);
            builder.Property(x => x.TittleEn)
                .HasMaxLength(150);
            builder.Property(x => x.ContentAz)
                .IsRequired();
            builder.Property(x => x.ContentEn)
                .IsRequired();
            builder.Property(x => x.ContentRu)
                .IsRequired();
            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}