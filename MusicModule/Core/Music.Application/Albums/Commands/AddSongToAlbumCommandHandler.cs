using Application.Shared.Abstractions;
using Application.Shared.CustomExceptions;
using MediatR;
using Music.Application.Abstractions;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.DomainEntities;
using System.Net;

namespace Music.Application.Albums.Commands
{
    internal sealed class AddSongToAlbumCommandHandler : IRequestHandler<AddSongToAlbumCommand>
    {
        private readonly IAlbumCommandRepository _AlbumCommandRepository;
        private readonly IAlbumQueryRepository _AlbumQueryRepository;
        private readonly IArtistQueryRepository _ArtistQueryRepository;
        private readonly IMusicUnitOfWork _MusicUnitOfWork;
        private readonly IFileConversionService _FileConversionService;
        private readonly IUserInfoService _UserInfoService;
        private readonly IFtpClient _FtpServer;
        public AddSongToAlbumCommandHandler(IAlbumCommandRepository albumCommandRepository,
            IAlbumQueryRepository albumQueryRepository,
            IArtistQueryRepository artistQueryRepository,
            IMusicUnitOfWork musicUnitOfWork,
            IUserInfoService userInfoService,
            IFileConversionService fileConversionService,
            IFtpClient ftpServer)
        {
            _AlbumCommandRepository = albumCommandRepository;
            _AlbumQueryRepository = albumQueryRepository;
            _ArtistQueryRepository = artistQueryRepository;
            _MusicUnitOfWork = musicUnitOfWork;
            _UserInfoService = userInfoService;
            _FileConversionService = fileConversionService;
            _FtpServer = ftpServer;
        }

        public async Task Handle(AddSongToAlbumCommand request, CancellationToken cancellationToken)
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

            string generated = _FileConversionService.GenerateRandomString();

            Song song = Song.CreateSong(request.CreateSongDto.SongName,
                request.FileName,
                request.CreateSongDto.SongCategory, generated);

            string fileName = _FileConversionService.OriginalFileName(request.FileName, generated);

            string path = await _FtpServer.UploadAsync(fileName, request.File);

            album.AddSong(song, request.CreateSongDto.Lyrics, path);

            await _AlbumCommandRepository.UpdateAsync(album);

            if (!await _MusicUnitOfWork.SaveChangesAsync())
            {
                throw new ApplicationException("Unexpected error");
            }
        }
    }
}
