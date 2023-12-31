using PlatformService.BLL.DTO;

namespace PlatformService.SyncDataServices.Http.Interfaces
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDTO platform);
    }
}
