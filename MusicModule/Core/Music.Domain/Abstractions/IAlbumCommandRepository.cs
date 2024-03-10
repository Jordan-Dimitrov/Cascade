using Domain.Shared.Primitives;
using Music.Domain.Aggregates.AlbumAggregate;

namespace Music.Domain.Abstractions
{
    public interface IAlbumCommandRepository : ICommandRepository<Album>
    {
    }
}
