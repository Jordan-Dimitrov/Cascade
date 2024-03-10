using System.Linq.Expressions;

namespace Domain.Shared.Primitives
{
    public interface IQueryRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, bool trackChanges);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> condition);
        Task<ICollection<T>> GetAllAsync(bool trackChanges);
        Task<T?> GetByNameAsync(string username);
    }
}
