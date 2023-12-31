using PlatformService.BLL.DTO;
using PlatformService.Entities;

namespace PlatformService.BLL.Interfaces
{
    public interface IPlatformService
    {
        Task<PlatformReadDTO?> GetPlatformReadByIdAsync(int id);
        Task<bool> CreatePlatformAsync(Platform platform);
        Task<List<Platform>> GetAllPlatformsAsync();
        Task<List<PlatformReadDTO>> GetAllPlatformReadAsync();
        Task<bool> DeletePlatformAsync(int id);
    }
}
