using Domain.Shared.Primitives;

namespace Users.Domain.DomainEvents
{
    public sealed record UserCreatedDomainEvent(Guid UserId, string Username, int Role) : IDomainEvent;
}
