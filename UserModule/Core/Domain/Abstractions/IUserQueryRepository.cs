using Domain.Shared.Primitives;
using Domain.Shared.RequestFeatures;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.RequestFeatures;

namespace Users.Domain.Abstractions
{
    public interface IUserQueryRepository : IQueryRepository<User>
    {
        Task<PagedList<User>> GetUsersWithPaginationAsync(UserParameters userParameters, bool trackChanges);
    }
}
