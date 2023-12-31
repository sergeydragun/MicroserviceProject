using System.Linq.Expressions;

namespace CommandsService.Data.Intrefaces
{
    public interface IBaseRepository<T> : IDisposable
    {
        IQueryable<T> GetAll();
        void Create(T entity);
        Task CreateAsync(T entity);
        void CreateRange(IEnumerable<T> entity);
        T? FirstOrDefault(Expression<Func<T, bool>> expression);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        void Delete(T entity);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition);
        bool Save();
    }
}
