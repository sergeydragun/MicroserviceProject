using Microsoft.EntityFrameworkCore;
using PlatformService.BLL.AsyncDataServices.Clients;
using PlatformService.BLL.AsyncDataServices.Interfaces;
using PlatformService.BLL.Interfaces;
using PlatformService.BLL.Services;
using PlatformService.Data;
using PlatformService.Data.Intrefaces;
using PlatformService.Data.Repositories;
using PlatformService.SyncDataServices.Http.Clients;
using PlatformService.SyncDataServices.Http.Interfaces;

namespace PlatformService
{
    public static class ServiceExtensions
    {
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigurePlatformService(this IServiceCollection services)
        {
            services.AddScoped<IPlatformService, PlatformsService>();
        }

        public static void AddHttpCommandDataClient(this IServiceCollection services)
        {
            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IWebHostEnvironment web, IConfiguration configuration)
        {
            if (web.IsProduction())
            {
                Console.WriteLine("Using MS SQL Server Database");
                services.AddDbContext<MyDbContext>(options => 
                    options.UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("PlatformsConn")));
            }
            else
            {
                Console.WriteLine("Using in memory database");
                services.AddDbContext<MyDbContext>(options =>
                options.UseLazyLoadingProxies()
                .UseInMemoryDatabase("InMem"));
            }

        }

        public static void ConfigureMessageBusClient(this IServiceCollection services)
            => services.AddSingleton<IMessageBusClient, MessageBusClient>();
    }
}
