using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Shared.Primitives;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Domain.Abstractions
{
    public interface IUserCommandRepository : ICommandRepository<User>
    {
    }
}
