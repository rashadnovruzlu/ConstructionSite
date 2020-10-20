using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConstructionSite.Entity.Data
{
    public class ConstructionDbContext : DbContext
    {
        
        public ConstructionDbContext(DbContextOptions<ConstructionDbContext> options) : base(options)
        {
           
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<AboutImage> AboutImages { get; set; }
        public  DbSet<Contact> Contacts { get; set; }
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
        public virtual DbSet<ServiceImage> ServiceImages { get; set; }
       
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Service ve Image cedvelleri arasinda "One to One" elaqesi ucun

           
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion
           
         

            base.OnModelCreating(modelBuilder);
        }
    }
}