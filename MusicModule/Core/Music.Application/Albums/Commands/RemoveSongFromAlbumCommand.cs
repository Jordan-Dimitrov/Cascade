using MediatR;

namespace Music.Application.Albums.Commands
{
    public sealed record RemoveSongFromAlbumCommand(Guid AlbumId, Guid SongId, string JwtToken) : IRequest;
}
