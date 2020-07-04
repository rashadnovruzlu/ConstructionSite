using ConstructionSite.Entity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionSite.Extensions.Seed
{
    public static class SeedDataExtension
    {
        public static void Seeding(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<ConstructionDbContext>();
            context.Database.Migrate();
        }
    }
}