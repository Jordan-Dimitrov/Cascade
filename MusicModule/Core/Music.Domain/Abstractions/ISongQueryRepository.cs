using Music.Domain.Aggregates.SongAggregate;
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
    public interface ISongQueryRepository : IQueryRepository<Song>
    {
        Task<PagedList<Song>> GetSongsWithPaginationAsync(SongParameters songParameters, bool trackChanges);
    }
}
