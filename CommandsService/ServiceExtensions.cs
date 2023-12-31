using CommandsService.Data;
using CommandsService.Data.Intrefaces;
using CommandsService.Data.Repositories;
using CommandsService.EventProcessing;
using CommandsService.SyncDataServices.gRPC.Clients;
using CommandsService.SyncDataServices.gRPC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommandsService
{
    public static class ServiceExtensions
    {
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyDbContext>(options =>
                    options.UseInMemoryDatabase("InMem"));

        }

        public static void ConfigureSingletonEventProcessor(this IServiceCollection services)
        {
            services.AddSingleton<IEventProcessor, EventProcessor>();
        }

        public static void ConfigureHostedMessageBusSubscriber(this IServiceCollection services)
            => services.AddHostedService<MessageBusSubscriber>();

        public static void ConfigurePlatformDataClient(this IServiceCollection services)
            => services.AddScoped<IPlatformDataClient, PlatformDataClient>();
    }
}
