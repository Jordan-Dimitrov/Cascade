using MediatR;
using Music.Application.Dtos;
using Music.Domain.Aggregates.AlbumAggregate;

namespace Music.Application.Albums.Queries
{
    public sealed record PatchAlbumQuery(Guid AlbumId, string JwtToken) : IRequest<(AlbumPatchDto AlbumToPatch, Album Album)>;
}
