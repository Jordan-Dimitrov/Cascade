using Application.Shared.Abstractions;
using Application.Shared.CustomExceptions;
using MediatR;
using Music.Application.Abstractions;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using System.Net;

namespace Music.Application.Albums.Commands
{
    internal sealed class RemoveSongFromAlbumCommandHandler : IRequestHandler<RemoveSongFromAlbumCommand>
    {
        private readonly IAlbumCommandRepository _AlbumCommandRepository;
        private readonly IAlbumQueryRepository _AlbumQueryRepository;
        private readonly IArtistQueryRepository _ArtistQueryRepository;
        private readonly IMusicUnitOfWork _MusicUnitOfWork;
        private readonly IUserInfoService _UserInfoService;
        public RemoveSongFromAlbumCommandHandler(IAlbumCommandRepository albumCommandRepository,
            IAlbumQueryRepository albumQueryRepository,
            IArtistQueryRepository artistQueryRepository,
            IMusicUnitOfWork musicUnitOfWork,
            IUserInfoService userInfoService)
        {
            _AlbumCommandRepository = albumCommandRepository;
            _AlbumQueryRepository = albumQueryRepository;
            _ArtistQueryRepository = artistQueryRepository;
            _MusicUnitOfWork = musicUnitOfWork;
            _UserInfoService = userInfoService;
        }
        public async Task Handle(RemoveSongFromAlbumCommand request, CancellationToken cancellationToken)
        {
            Artist? artist = await _ArtistQueryRepository
                .GetByNameAsync(_UserInfoService.GetUsernameFromJwtToken(request.JwtToken));

            if (artist is null)
            {
                throw new AppException("No such artist exists!", HttpStatusCode.NotFound);
            }

            Album? album = await _AlbumQueryRepository.GetByIdAsync(request.AlbumId, true);

            if (album is null)
            {
                throw new AppException("No such album exists!", HttpStatusCode.NotFound);
            }

            if (album.ArtistId != artist.Id)
            {
                throw new AppException("Album does not belong to artist!", HttpStatusCode.BadRequest);
            }

            album.RemoveSong(request.SongId);

            await _AlbumCommandRepository.UpdateAsync(album);

            if (!await _MusicUnitOfWork.SaveChangesAsync())
            {
                throw new ApplicationException("Unexpected error");
            }
        }
    }
}
