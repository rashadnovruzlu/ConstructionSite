using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionSite.Entity.Data
{
    public class ConstructionDbContext : DbContext
    {
        public ConstructionDbContext(DbContextOptions<ConstructionDbContext> options) : base(options)
        { 
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<AboutImage> AboutImages { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }
        public virtual DbSet<HomePage> HomePages { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsImage> NewsImages { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectImage> ProjectImages { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<StaticField> StaticFields { get; set; }
        public virtual DbSet<SubService> SubServices { get; set; }
        public virtual DbSet<SubServiceImage> SubServiceImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Service ve Image cedvelleri arasinda "One to One" elaqesi ucun
            modelBuilder.Entity<Service>()
                            .HasOne<Image>(i => i.Image)
                                .WithOne(x => x.Service);
            #endregion

            modelBuilder.Entity<AboutImage>()
                            .HasOne(ai => ai.About)
                                .WithMany(a => a.AboutImages);

            modelBuilder.Entity<AboutImage>()
                            .HasOne(ai => ai.Image)
                                .WithMany(i => i.AboutImages);

            modelBuilder.Entity<HomePage>()
                            .HasOne(hp => hp.Image)
                                .WithMany(i => i.HomePages);

            modelBuilder.Entity<NewsImage>()
                            .HasOne(ni => ni.Image)
                                .WithMany(i => i.NewsImages);

            modelBuilder.Entity<NewsImage>()
                            .HasOne(ni => ni.News)
                                .WithMany(n => n.NewsImages);

            modelBuilder.Entity<Project>()
                            .HasOne(p => p.Portfolio)
                                .WithMany(p => p.Projects);

            modelBuilder.Entity<ProjectImage>()
                            .HasOne(pi => pi.Image)
                                .WithMany(i => i.ProjectImages);

            modelBuilder.Entity<ProjectImage>()
                            .HasOne(pi => pi.Project)
                                .WithMany(p => p.ProjectImages);

            modelBuilder.Entity<SubService>()
                            .HasOne(ss => ss.Service)
                                .WithMany(s => s.SubServices);

            modelBuilder.Entity<SubServiceImage>()
                            .HasOne(ssi => ssi.Image)
                                .WithMany(i => i.SubServiceImages);

            modelBuilder.Entity<SubServiceImage>()
                            .HasOne(ssi => ssi.SubService)
                                .WithMany(ss => ss.SubServiceImages);

            base.OnModelCreating(modelBuilder);
        }
    }
}