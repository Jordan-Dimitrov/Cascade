using Domain.Shared.Primitives;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Domain.Abstractions
{
    public interface IUserCommandRepository : ICommandRepository<User>
    {
    }
}
