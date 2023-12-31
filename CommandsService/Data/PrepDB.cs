using Microsoft.EntityFrameworkCore;
using CommandsService.Entities;
using CommandsService.SyncDataServices.gRPC.Interfaces;
using CommandsService.Data.Intrefaces;

namespace CommandsService.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
                var platforms = grpcClient.ReturnAllPlatforms();

                var database = serviceScope.ServiceProvider.GetService<IUnitOfWork>();

                SeedData(database, platforms);
            }
        }

        private static void SeedData(IUnitOfWork database, List<Platform>  platforms)
        {
            var ExtIdExists = database.Platforms.GetAll().Select(p => p.ExternalID).ToList();

            database.Platforms.CreateRange(platforms.Where(p => !ExtIdExists.Contains(p.ExternalID)));
            database.Save();
        }
    }
}
