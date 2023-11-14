using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly MyDbContext _db;
        public PlatformRepo(MyDbContext context)
        {
            _db = context;
        }

        public void CreatePlatform(Platform platform)
        {
            _db.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _db.Platforms.ToList();
        }

        public Platform? GetPlatformById(int id)
        {
            return _db.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return _db.SaveChanges() >= 0;
        }
    }
}
