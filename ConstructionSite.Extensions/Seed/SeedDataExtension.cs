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
        public async static void Seeding(this IApplicationBuilder app)
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

                   ContentAz= "Integer rhoncus hendrerit sem egestas porttitor. " +
                              "Integer et mi sed dolor eleifend pretium quis ut velit. " +
                              "Nam sit amet arcu feugiat, consequat orci at, ultrices magna. " +
                              "Aliquam vestibulum, eros vel venenatis vulputate, erat augue suscipit mi, nec rhoncus velit ipsum sed lorem. " +
                              "Nulla commodo leo eget justo blandit, non sagittis lectus dignissim",
                   
                   ContentRu= "Integer rhoncus hendrerit sem egestas porttitor. " +
                              "Integer et mi sed dolor eleifend pretium quis ut velit. " +
                              "Nam sit amet arcu feugiat, consequat orci at, ultrices magna. " +
                              "Aliquam vestibulum, eros vel venenatis vulputate, erat augue suscipit mi, nec rhoncus velit ipsum sed lorem. " +
                              "Nulla commodo leo eget justo blandit, non sagittis lectus dignissim",

                    ContentEn= "Integer rhoncus hendrerit sem egestas porttitor. " +
                              "Integer et mi sed dolor eleifend pretium quis ut velit. " +
                              "Nam sit amet arcu feugiat, consequat orci at, ultrices magna. " +
                              "Aliquam vestibulum, eros vel venenatis vulputate, erat augue suscipit mi, nec rhoncus velit ipsum sed lorem. " +
                              "Nulla commodo leo eget justo blandit, non sagittis lectus dignissim"
                });
            }

            if(!await context.Images.AnyAsync())
            {
                context.Images.Add(new Image
                {

                });
            
            }

            if(!await context.Services.AnyAsync())
            {
                context.Services.Add(new Service
                {
                    NameAz = "Construction",
                    NameEn = "Construction",
                    NameRu = "Construction",

                    TittleAz = "Sed sit amet sapien sit amet odio lobortis ullamcorper quis vel nisl. " +
                               "Nam blandit maximus tristique. Vivamus enim quam.",

                    TittleEn = "Sed sit amet sapien sit amet odio lobortis ullamcorper quis vel nisl. " +
                               "Nam blandit maximus tristique. Vivamus enim quam.",

                    TittleRu = "Sed sit amet sapien sit amet odio lobortis ullamcorper quis vel nisl. " +
                               "Nam blandit maximus tristique. Vivamus enim quam."
                });
            }

            if(!await context.Contacts.AnyAsync())
            {
                context.Contacts.Add(new Contact
                {
                    TittleAz = "GET IN TOUCH WITH US",
                    TittleEn = "GET IN TOUCH WITH US",
                    TittleRu = "GET IN TOUCH WITH US",

                    ContentAz = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                                "Phasellus sit amet iaculis elit. Nam semper ut arcu non placerat. " +
                                "Praesent nibh massa varius.",

                    ContentEn = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                                "Phasellus sit amet iaculis elit. Nam semper ut arcu non placerat. " +
                                "Praesent nibh massa varius.",

                    ContentRu = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                                "Phasellus sit amet iaculis elit. Nam semper ut arcu non placerat. " +
                                "Praesent nibh massa varius.",

                    Address = "68 Havemeyer St, Brooklyn, NY 11211 United States",
                    Email = "contact@construction.com",
                    PhoneNumber = "+1 718-955-2838 or +1 718-955-3290"
                });
            }
            if(!await context.Descriptions.AnyAsync())
            {
                context.Descriptions.Add(new Description
                {
                    TittleAz = "We use technology to do the job more quickly",
                    TittleEn = "We use technology to do the job more quickly",
                    TittleRu = "We use technology to do the job more quickly",

                    ContentAz = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                               "In a metus pellentesque, scelerisque ex sed, volutpat nisi. " +
                               "Curabitur tortor mi, eleifend ornare lobortis non. " +
                               "Nulla porta purus quis iaculis ultrices. " +
                               "Proin aliquam sem at nibh hendrerit sagittis. " +
                               "Nullam ornare odio eu lacus tincidunt malesuada. " +
                               "Sed eu vestibulum elit. Curabitur tortor mi, eleifend ornare.",

                    ContentEn = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                               "In a metus pellentesque, scelerisque ex sed, volutpat nisi. " +
                               "Curabitur tortor mi, eleifend ornare lobortis non. " +
                               "Nulla porta purus quis iaculis ultrices. " +
                               "Proin aliquam sem at nibh hendrerit sagittis. " +
                               "Nullam ornare odio eu lacus tincidunt malesuada. " +
                               "Sed eu vestibulum elit. Curabitur tortor mi, eleifend ornare.",

                    ContentRu = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                               "In a metus pellentesque, scelerisque ex sed, volutpat nisi. " +
                               "Curabitur tortor mi, eleifend ornare lobortis non. " +
                               "Nulla porta purus quis iaculis ultrices. " +
                               "Proin aliquam sem at nibh hendrerit sagittis. " +
                               "Nullam ornare odio eu lacus tincidunt malesuada. " +
                               "Sed eu vestibulum elit. Curabitur tortor mi, eleifend ornare."
                });
            }
            context.SaveChanges();
        }
    }
}