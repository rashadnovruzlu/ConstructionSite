using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionSite.Entity.Configuration
{
    public class NewsImageConfiguration : IEntityTypeConfiguration<NewsImage>
    {
        public void Configure(EntityTypeBuilder<NewsImage> builder)
        {
            builder.HasOne(ni => ni.Image)
                                 .WithMany(i => i.NewsImages);
            builder.HasOne(ni => ni.News)
                                .WithMany(n => n.NewsImages);
        }
    }
}