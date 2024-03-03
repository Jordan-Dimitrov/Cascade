using Application.Shared;
using Application.Shared.CustomExceptions;
using MediatR;
using Music.Application.Abstractions;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Commands
{
    internal sealed class AddSongToAlbumCommandHandler : IRequestHandler<AddSongToAlbumCommand>
    {
        private readonly IAlbumCommandRepository _AlbumCommandRepository;
        private readonly IAlbumQueryRepository _AlbumQueryRepository;
        private readonly IArtistQueryRepository _ArtistQueryRepository;
        private readonly IMusicUnitOfWork _MusicUnitOfWork;
        public AddSongToAlbumCommandHandler(IAlbumCommandRepository albumCommandRepository,
            IAlbumQueryRepository albumQueryRepository,
            IArtistQueryRepository artistQueryRepository,
            IMusicUnitOfWork musicUnitOfWork)
        {
            _AlbumCommandRepository = albumCommandRepository;
            _AlbumQueryRepository = albumQueryRepository;
            _ArtistQueryRepository = artistQueryRepository;
            _MusicUnitOfWork = musicUnitOfWork;
        }

        public async Task Handle(AddSongToAlbumCommand request, CancellationToken cancellationToken)
        {
            Artist? artist = await _ArtistQueryRepository
                .GetByNameAsync(Utils.GetUsernameFromJwtToken(request.JwtToken));

            if (artist is null)
            {
                throw new AppException("No such artist exists!", HttpStatusCode.NotFound);
            }

            Album? album = await _AlbumQueryRepository.GetByIdAsync(request.AlbumId, true);

            if (album is null)
            {
                throw new AppException("No such album exists!", HttpStatusCode.NotFound);
            }

            if(album.ArtistId != artist.Id)
            {
                throw new AppException("Album does not belong to artist!", HttpStatusCode.BadRequest);
            }

            Song song = Song.CreateSong(request.CreateSongDto.SongName,
                request.FileName,
                request.CreateSongDto.SongCategory);

            album.AddSong(song);

            await _AlbumCommandRepository.UpdateAsync(album);

            if (await _MusicUnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }
        }
    }
}
