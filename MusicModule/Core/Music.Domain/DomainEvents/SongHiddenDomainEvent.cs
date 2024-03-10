using Domain.Shared.Primitives;

namespace Music.Domain.DomainEvents
{
    public sealed record SongHiddenDomainEvent(string FileName) : IDomainEvent;
}
