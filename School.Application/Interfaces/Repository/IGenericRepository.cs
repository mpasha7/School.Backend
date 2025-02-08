using System.Linq.Expressions;

namespace School.Application.Interfaces.Repository
{
    public interface IGenericRepository<T> : IDisposable
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(
            CancellationToken cancellationToken,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");
        Task<T?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken,
            string includeReference = "",
            string includeCollection = "");
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);
    }
}
