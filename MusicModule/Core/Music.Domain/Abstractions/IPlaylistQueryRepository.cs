using Domain.Shared.Primitives;
using Domain.Shared.RequestFeatures;
using Music.Domain.Aggregates.PlaylistAggregate;
using Music.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Abstractions
{
    public interface IPlaylistQueryRepository : IQueryRepository<Playlist>
    {
        Task<PagedList<Playlist>> GetListenersWithPaginationAsync(PlaylistParameters playlistParameters, bool trackChanges);
    }
}
