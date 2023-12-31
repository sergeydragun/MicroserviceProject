using CommandsService.Entities;

namespace CommandsService.SyncDataServices.gRPC.Interfaces
{
    public interface IPlatformDataClient
    {
        List<Platform> ReturnAllPlatforms();
    }
}
