using Domain.Shared.Primitives;
using Domain.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.RequestFeatures;

namespace Users.Domain.Abstractions
{
    public interface IUserQueryRepository : IQueryRepository<User>
    {
        Task<PagedList<User>> GetUsersWithPaginationAsync(UserParameters userParameters, bool trackChanges);
    }
}
