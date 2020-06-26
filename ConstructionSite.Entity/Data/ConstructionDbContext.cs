using ConstructionSite.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Data
{
    class ConstructionDbContext : DbContext
    {
        public ConstructionDbContext(DbContextOptions<ConstructionDbContext> options) : base(options)
        { }

        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutImage> AboutImages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }
        public DbSet<HomePage> HomePages { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsImage> NewsImages { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<StaticField> StaticFields { get; set; }
        public DbSet<SubService> SubServices { get; set; }
        public DbSet<SubServiceImage> SubServiceImages { get; set; }
    }
}