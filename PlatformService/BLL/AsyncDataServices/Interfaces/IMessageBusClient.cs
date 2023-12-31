using PlatformService.BLL.DTO;
using PlatformService.Entities;

namespace PlatformService.BLL.AsyncDataServices.Interfaces
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDTO platformPublishedDTO);
    }
}
