using MediatR;
using Music.Application.Dtos;

namespace Music.Application.Albums.Queries
{
    public sealed record GetAlbumQuery(Guid AlbumId) : IRequest<AlbumWithSongsDto>;
}
