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
        public async static void Seeding(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<ConstructionDbContext>();
            context.Database.Migrate();
            if (!await context.Abouts.AnyAsync())
            {
                context.Abouts.Add(new About
                {
                   TittleAz= "RECENT",
                   TittleRu= "RECENT",
                   TittleEn= "RECENT",
                   ContentAz= "Integer rhoncus hendrerit sem egestas porttitor. Integer et mi sed dolor eleifend pretium quis ut velit. Nam sit amet arcu feugiat, consequat orci at, ultrices magna. Aliquam vestibulum, eros vel venenatis vulputate, erat augue suscipit mi, nec rhoncus velit ipsum sed lorem. Nulla commodo leo eget justo blandit, non sagittis lectus dignissim"
                   ,ContentRu= "Integer rhoncus hendrerit sem egestas porttitor. Integer et mi sed dolor eleifend pretium quis ut velit. Nam sit amet arcu feugiat, consequat orci at, ultrices magna. Aliquam vestibulum, eros vel venenatis vulputate, erat augue suscipit mi, nec rhoncus velit ipsum sed lorem. Nulla commodo leo eget justo blandit, non sagittis lectus dignissim",
                   ContentEn= "Integer rhoncus hendrerit sem egestas porttitor. Integer et mi sed dolor eleifend pretium quis ut velit. Nam sit amet arcu feugiat, consequat orci at, ultrices magna. Aliquam vestibulum, eros vel venenatis vulputate, erat augue suscipit mi, nec rhoncus velit ipsum sed lorem. Nulla commodo leo eget justo blandit, non sagittis lectus dignissim"
                });
            }
            //if (!await context.Images.AnyAsync())
            //{
            //    context.Images.Add(new Image
            //    {
            //        Path=
            //    });
            //}
        }
    }
}