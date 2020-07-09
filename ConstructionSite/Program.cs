using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ConstructionSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).UseDefaultServiceProvider(x=>x.ValidateScopes=false).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
           
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}