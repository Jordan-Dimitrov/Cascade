using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Commands
{
    public sealed record RemoveSongFromAlbumCommand(Guid AlbumId, Guid SongId, string JwtToken) : IRequest;
}
