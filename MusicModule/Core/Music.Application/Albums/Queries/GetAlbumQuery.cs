using MediatR;
using Music.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Queries
{
    public sealed record GetAlbumQuery(Guid AlbumId) : IRequest<AlbumWithSongsDto>;
}
