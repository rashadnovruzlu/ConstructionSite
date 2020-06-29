using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Configuration
{
    public class AboutConfiguration : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {

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
    public class AboutImageConfiguration : IEntityTypeConfiguration<AboutImage>
    {
        public void Configure(EntityTypeBuilder<AboutImage> builder)
        {
        
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.AboutId).IsRequired();
            builder.Property(x=>x.ImageId).IsRequired();
                
            builder.HasOne(ai => ai.About)
                                .WithMany(a => a.AboutImages);
            builder.HasOne(ai => ai.Image)
                               .WithMany(i => i.AboutImages);

        }
    }
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.TittleAz)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x=>x.TittleRu)
                .HasMaxLength(150);
            builder.Property(x=>x.TittleEn)
                .HasMaxLength(150);
            builder.Property(x=>x.ContentAz)
                .IsRequired();
            builder.Property(x=>x.Address)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x=>x.Email)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x=>x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
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
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {    
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.NameAz)
                .IsRequired()
                .HasMaxLength(75);
            
            builder.Property(x => x.NameRu)
             .HasMaxLength(75);

            builder.Property(x => x.NameEn)
              .HasMaxLength(75);

            builder.Property(x=>x.TittleAz)
                .IsRequired();

            builder
                .HasOne<Image>(i => i.Image)
                                  .WithOne(x => x.Service)
                                  .HasForeignKey<Image>(i => i.ServiceId);
        }
    }
   
    public class HomePageConfiguration : IEntityTypeConfiguration<HomePage>
    {
        public void Configure(EntityTypeBuilder<HomePage> builder)
        {
            builder.HasOne(hp => hp.Image)
                                .WithMany(i => i.HomePages);
        }
    }
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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(x => x.NameAz)
               .IsRequired()
               .HasMaxLength(75);

            builder.Property(x => x.NameRu)
           .HasMaxLength(75);

            builder.Property(x => x.NameEn)
              .HasMaxLength(75);

            builder.Property(x=>x.ContentAz)
                .IsRequired();

            builder.HasOne(p => p.Portfolio)
                                .WithMany(p => p.Projects);
        }
    }
    public class ProjectImageConfiguration : IEntityTypeConfiguration<ProjectImage>
    {
        public void Configure(EntityTypeBuilder<ProjectImage> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.IsMain).IsRequired();
            builder.Property(x=>x.ImageId).IsRequired();
            builder.Property(x=>x.ProjectId).IsRequired();
            builder.HasOne(pi => pi.Image)
                                .WithMany(i => i.ProjectImages);
            builder.HasOne(pi => pi.Project)
                                .WithMany(p => p.ProjectImages);
        }
    }

}
