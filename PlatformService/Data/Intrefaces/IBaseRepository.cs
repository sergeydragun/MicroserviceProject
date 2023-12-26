using System.Linq.Expressions;

namespace PlatformService.Data.Intrefaces
{
    public interface IBaseRepository<T> : IDisposable
    {
        IQueryable<T> GetAll();
        void Create(T entity);
        Task CreateAsync(T entity);
        T? FirstOrDefault(Expression<Func<T, bool>> expression);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        void Delete(T entity);
    }
}
