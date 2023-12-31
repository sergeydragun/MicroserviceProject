using CommandsService.Data.Intrefaces;
using CommandsService.Data.Repositories;

namespace CommandsService.Data.Repositories
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
        private ICommandRepo _commandRepo;
        public ICommandRepo Commands
        {
            get
            {
                _commandRepo ??= new CommandRepo(_db);
                return _commandRepo;
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

        public bool Save()
        {
            var result = _db.SaveChanges();
            return result >= 0;
        }
    }
}
