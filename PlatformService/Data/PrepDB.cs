using Microsoft.EntityFrameworkCore;
using PlatformService.Entities;

namespace PlatformService.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<MyDbContext>(), environment);

            }
        }

        private static void SeedData(MyDbContext context, IWebHostEnvironment environment)
        {
            if (environment.IsProduction())
            {
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(
                    new Platform() { Name = ".NET", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "CNCF", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }
    }
}
