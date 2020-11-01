using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ConstructionSite.Extensions.Seed
{
    public static class SeedDataExtension
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<ConstructionDbContext>();
            // context.Database.Migrate();

            if (!context.Contacts.Any())
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
                    PhoneNumber = "+1 718-955-2838 or +1 718-955-3290",
                    lat = "40.409264",
                    lng = "49.867092"
                });
                if (context.Contacts.Count() > 1)
                {
                    var result = context.Contacts.OrderByDescending(x => x.Id)
                         .Take(1).FirstOrDefault();
                    var ss = context.Contacts.Where(x => x.Id != result.Id).ToList();
                    context.Contacts.RemoveRange(ss);
                }
            }

            context.SaveChanges();
        }
    }
}