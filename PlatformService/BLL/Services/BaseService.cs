using AutoMapper;
using PlatformService.Data.Intrefaces;

namespace PlatformService.BLL.Services
{
    public class BaseService : IDisposable
    {
        protected IMapper _mapper;
        protected IUnitOfWork _database;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper) : this(mapper)
        {
            _database = unitOfWork;
        }

        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_database != null)
                {
                    _database.Dispose();
                    _database = null;
                }
            }
        }
    }
}
