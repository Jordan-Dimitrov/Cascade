using Domain.Shared.Primitives;
using Domain.Shared.RequestFeatures;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.RequestFeatures;

namespace Music.Domain.Abstractions
{
    public interface IAlbumQueryRepository : IQueryRepository<Album>
    {
        Task<PagedList<Album>> GetAlbumsWithPaginationAsync(AlbumParameters albumParameters, bool trackChanges);
    }
}
