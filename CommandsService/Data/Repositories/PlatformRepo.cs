using CommandsService.Data.Intrefaces;
using CommandsService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data.Repositories
{
    public class PlatformRepo : BaseRepository<Platform>, IPlatformRepo
    {
        public PlatformRepo(MyDbContext context) : base(context)
        {
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return _db.Platforms.Any(p => p.ExternalID == externalPlatformId);
        }

        public async Task<bool> PlatformExits(int platformId)
        {
            var platform = await _db.Platforms.FirstOrDefaultAsync(p => p.Id == platformId);
            return platform != null;
        }
    }
}
