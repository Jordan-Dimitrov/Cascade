using Domain.Shared.Primitives;
using Music.Domain.Aggregates.ArtistAggregate;

namespace Music.Domain.Abstractions
{
    public interface IArtistCommandRepository : ICommandRepository<Artist>
    {
    }
}
