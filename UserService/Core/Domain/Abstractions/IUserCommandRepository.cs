using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.Primitives;

namespace Domain.Abstractions
{
    public interface IUserCommandRepository : ICommandRepository<User>
    {
        Task RemoveOldRefreshTokenAsync(RefreshToken refreshToken);
        Task UpdateRefreshTokenAsync(User value);
    }
}
