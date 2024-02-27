using Domain.Shared.Constants;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain.DomainEvents
{
    public sealed record UserHiddenDomainEvent(Guid UserId, UserRole Role) : IDomainEvent;
}
