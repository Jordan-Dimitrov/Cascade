using MediatR;
using Music.Application.Dtos;

namespace Music.Application.Albums.Commands
{
    public sealed record CreateAlbumCommand(CreateAlbumDto CreateAlbumDto, string JwtToken,
         byte[] File, string FileName, string[] Lyrics) : IRequest<Guid>;
}
