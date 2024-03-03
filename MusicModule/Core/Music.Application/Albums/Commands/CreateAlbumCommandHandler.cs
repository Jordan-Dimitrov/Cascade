using Application.Shared;
using Application.Shared.CustomExceptions;
using Domain.Shared.Abstractions;
using MediatR;
using Music.Application.Abstractions;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.DomainEntities;
using Music.Domain.DomainServices;
using Music.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Commands
{
    internal sealed class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, Guid>
    {
        private readonly IArtistQueryRepository _ArtistQueryRepository;
        private readonly IArtistCommandRepository _ArtistCommandRepository;
        private readonly IMusicUnitOfWork _MusicUnitOfWork;
        private readonly IAlbumQueryRepository _AlbumQueryRepository;
        public CreateAlbumCommandHandler(IArtistCommandRepository artistCommandRepository,
            IArtistQueryRepository artistQueryRepository,
            IMusicUnitOfWork musicUnitOfWork,
            IAlbumQueryRepository albumQueryRepository)
        {
            _ArtistCommandRepository = artistCommandRepository;
            _ArtistQueryRepository = artistQueryRepository;
            _MusicUnitOfWork = musicUnitOfWork;
            _AlbumQueryRepository = albumQueryRepository;
        }

        public async Task<Guid> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            Artist? artist = await _ArtistQueryRepository
                .GetByNameAsync(Utils.GetUsernameFromJwtToken(request.JwtToken));

            if(artist is null)
            {
                throw new AppException("No such artist exists!", HttpStatusCode.NotFound);
            }

            Song song = Song.CreateSong(request.CreateAlbumDto.SongName,
                request.FileName,
                request.CreateAlbumDto.SongCategory);

            AlbumName name = new AlbumName(request.CreateAlbumDto.AlbumName);

            if(await _AlbumQueryRepository.ExistsAsync(x => x.AlbumName == name))
            {
                throw new AppException("Such album name already exists!", HttpStatusCode.BadRequest);
            }

            Album album = Album.CreateAlbum(request.CreateAlbumDto.AlbumName, artist.Id, song);
            album.AddSong(song);

            artist.AddAlbum(album);

            await _ArtistCommandRepository.UpdateAsync(artist);

            if (await _MusicUnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }

            return album.Id;
        }
    }
}
