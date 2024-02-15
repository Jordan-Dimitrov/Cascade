using Music.Domain.Aggregates.ArtistAggregate;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Shared.RequestFeatures;
using Music.Domain.RequestFeatures;

namespace Music.Domain.Abstractions
{
    public interface IArtistQueryRepository : IQueryRepository<Artist>
    {
        Task<PagedList<Artist>> GetArtistsWithPaginationAsync(ArtistParameters artistParameters, bool trackChanges);
    }
}
