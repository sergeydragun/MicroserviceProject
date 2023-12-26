using PlatformService.Data.Intrefaces;
using PlatformService.Models;

namespace PlatformService.Data.Repositories
{
    public class PlatformRepo : BaseRepository<Platform>, IPlatformRepo
    {
        public PlatformRepo(MyDbContext context) : base(context)
        {
        }

    }
}
