using PlatformService.BLL.Interfaces;
using PlatformService.BLL.Services;
using PlatformService.Data.Intrefaces;
using PlatformService.Data.Repositories;

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
    }
}
