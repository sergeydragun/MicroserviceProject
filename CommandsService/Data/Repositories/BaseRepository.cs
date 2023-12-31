using Microsoft.EntityFrameworkCore;
using CommandsService.Data.Intrefaces;
using System.Linq.Expressions;

namespace CommandsService.Data.Repositories
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
        public void CreateRange(IEnumerable<T> entities)
        {
            _db.Set<T>().AddRange(entities);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)
            => _db.Set<T>().Where(condition);

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

        public bool Save()
        {
            var result = _db.SaveChanges();
            return result > 0;
        }
    }
}
