using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.ValueObjects;

namespace Users.Domain.DomainEvents
{
    public sealed record UserCreatedDomainEvent(Guid UserId, string Username, int Role) : IDomainEvent;
}
