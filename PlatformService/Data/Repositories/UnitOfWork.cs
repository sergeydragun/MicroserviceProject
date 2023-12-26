using PlatformService.Data.Intrefaces;

namespace PlatformService.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyDbContext _db;
        public UnitOfWork(MyDbContext context)
        {
            _db = context;
        }
        private IPlatformRepo _platformRepo;
        public IPlatformRepo Platforms
        {
            get
            {
                _platformRepo ??= new PlatformRepo(_db);
                return _platformRepo;
            }
        }

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if(!disposed) 
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                disposed = true;
            }
        }

        public async Task<bool> SaveAsync()
        {
            var result = await _db.SaveChangesAsync();
            return result >= 0;
        }
    }
}
