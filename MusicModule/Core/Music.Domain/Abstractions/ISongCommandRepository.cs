using Music.Domain.Aggregates.SongAggregate;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Abstractions
{
    public interface ISongCommandRepository : ICommandRepository<Song>
    {
    }
}
