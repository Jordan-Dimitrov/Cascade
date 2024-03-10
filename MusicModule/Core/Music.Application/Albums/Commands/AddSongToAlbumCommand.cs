using MediatR;
using Music.Application.Dtos;

namespace Music.Application.Albums.Commands
{
    public sealed record AddSongToAlbumCommand(CreateSongDto CreateSongDto, string JwtToken,
         byte[] File, string FileName, Guid AlbumId) : IRequest;
}
