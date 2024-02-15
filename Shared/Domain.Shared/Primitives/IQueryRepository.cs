using Domain.Shared.Abstractions;
using Domain.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
