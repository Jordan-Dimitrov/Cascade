using Domain.Aggregates.UserAggregate;
using Domain.Primitives;
using Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IUserQueryRepository : IQueryRepository<User>
    {
        Task<User?> GetUserByNameAsync(string username);
        Task<ICollection<User>> GetUsersWithPaginationAsync(RequestParameters userParameters, bool trackChanges);
    }
}
