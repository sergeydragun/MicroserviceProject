using CommandsService.Entities;

namespace CommandsService.Data.Intrefaces
{
    public interface IPlatformRepo : IBaseRepository<Platform>
    {
        Task<bool> PlatformExits(int platformId);
        bool ExternalPlatformExists(int externalPlatformId);
    }
}
