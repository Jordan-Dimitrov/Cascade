using Music.Domain.Aggregates.ArtistAggregate;
using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Abstractions
{
    public interface IArtistCommandRepository : ICommandRepository<Artist>
    {
    }
}
