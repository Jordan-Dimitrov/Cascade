using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Primitives
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> condition);

        Task InsertAsync(T value);

        Task UpdateAsync(T value);

        Task DeleteAsync(T value);
    }
}
