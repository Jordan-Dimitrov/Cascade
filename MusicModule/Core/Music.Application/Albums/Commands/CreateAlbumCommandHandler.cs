using Application.Shared;
using Domain.Shared.Abstractions;
using MediatR;
using Music.Application.Abstractions;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.Aggregates.SongAggregate;
using Music.Domain.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Commands
{
    internal sealed class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, Guid>
    {
        private readonly IAlbumCommandRepository _AlbumCommandRepository;
        private readonly IAlbumQueryRepository _AlbumQueryRepository;
        private readonly IArtistQueryRepository _ArtistQueryRepository;
        private readonly IArtistCommandRepository _ArtistCommandRepository;
        private readonly AlbumService _AlbumService;
        private readonly IMusicUnitOfWork _MusicUnitOfWork;
        public CreateAlbumCommandHandler(IAlbumCommandRepository albumCommandRepository,
            IAlbumQueryRepository albumQueryRepository,
            IArtistCommandRepository artistCommandRepository,
            IArtistQueryRepository artistQueryRepository,
            AlbumService albumService,
            IMusicUnitOfWork musicUnitOfWork)
        {
            _AlbumCommandRepository = albumCommandRepository;
            _AlbumQueryRepository = albumQueryRepository;
            _ArtistCommandRepository = artistCommandRepository;
            _ArtistQueryRepository = artistQueryRepository;
            _AlbumService = albumService;
            _MusicUnitOfWork = musicUnitOfWork;
        }

        public async Task<Guid> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            Artist? artist = await _ArtistQueryRepository
                .GetByNameAsync(Utils.GetUsernameFromJwtToken(request.JwtToken));

            if(artist is null)
            {
                throw new ApplicationException("No such artist exists!");
            }

            Song song = Song.CreateSong(request.CreateAlbumDto.SongName,
                request.FileName,
                artist.Id,
                request.CreateAlbumDto.SongCategory);

            Album album = Album.CreateAlbum(request.CreateAlbumDto.AlbumName, song.Id);

            await _AlbumService.CreateAlbum(artist, album, song);

            if (await _MusicUnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }

            return album.Id;
        }
    }
}
