using Domain.Shared.Constants;
using Domain.Shared.Primitives;

namespace Users.Domain.DomainEvents
{
    public sealed record UserHiddenDomainEvent(Guid UserId, UserRole Role) : IDomainEvent;
}
