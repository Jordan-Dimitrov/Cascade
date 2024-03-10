using Domain.Shared.Primitives;

namespace Music.Domain.DomainEvents
{
    public sealed record SongCreatedDomainEvent(string FileName, string[] Lyrics) : IDomainEvent;
}
