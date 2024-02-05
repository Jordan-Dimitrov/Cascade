using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Primitives
{
    public interface ICommandRepository<T> where T : class
    {
        Task InsertAsync(T value);

        Task UpdateAsync(T value);

        Task DeleteAsync(T value);
    }
}
