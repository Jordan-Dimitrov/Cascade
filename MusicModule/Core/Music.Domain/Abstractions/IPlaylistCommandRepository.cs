using Domain.Shared.Primitives;
using Music.Domain.Aggregates.PlaylistAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Abstractions
{
    public interface IPlaylistCommandRepository : ICommandRepository<Playlist>
    {
    }
}
