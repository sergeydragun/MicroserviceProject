using Microsoft.EntityFrameworkCore;
using PlatformService.Data.Intrefaces;
using System.Linq.Expressions;

namespace PlatformService.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected MyDbContext _db;
        public BaseRepository(MyDbContext context) 
        {
            _db = context;
        }
        public IQueryable<T> GetAll()
        {
            return _db.Set<T>().AsNoTracking();
        }

        public void Create(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
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
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
        }

        public T? FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _db.Set<T>().FirstOrDefault(expression);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _db.Set<T>().FirstOrDefaultAsync(expression);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
    }
}
