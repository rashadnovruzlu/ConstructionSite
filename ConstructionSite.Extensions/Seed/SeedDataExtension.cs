using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionSite.Extensions.Seed
{
    public static class SeedDataExtension
    {
        public static void Seeding(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<ConstructionDbContext>();
            context.Database.Migrate();
            if (!context.Abouts.Any())
            {
                context.Abouts.Add(new About
                {

                });
            }
        }
    }
}