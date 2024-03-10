using Domain.Shared.Primitives;
using Domain.Shared.RequestFeatures;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.RequestFeatures;

namespace Music.Domain.Abstractions
{
    public interface IArtistQueryRepository : IQueryRepository<Artist>
    {
        Task<PagedList<Artist>> GetArtistsWithPaginationAsync(ArtistParameters artistParameters, bool trackChanges);
    }
}
