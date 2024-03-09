using Domain.Shared.Primitives;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Abstractions
{
    public interface IAlbumCommandRepository : ICommandRepository<Album>
    {
    }
}
