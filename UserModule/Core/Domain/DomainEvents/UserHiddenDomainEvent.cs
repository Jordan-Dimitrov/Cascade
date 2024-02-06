using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain.DomainEvents
{
    public sealed record UserHiddenDomainEvent(Guid UserId) : IDomainEvent;
}
