using MediatR;
using Music.Application.Dtos;
using Music.Domain.Aggregates.AlbumAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Queries
{
    public sealed record PatchAlbumQuery(Guid AlbumId, string JwtToken) : IRequest<(AlbumPatchDto AlbumToPatch, Album Album)>;
}
