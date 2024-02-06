using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Shared.Primitives;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.DomainEntities;

namespace Users.Domain.Abstractions
{
    public interface IUserCommandRepository : ICommandRepository<User>
    {
        Task RemoveOldRefreshTokenAsync(RefreshToken refreshToken);
        Task UpdateRefreshTokenAsync(User value);
    }
}
