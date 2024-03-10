using Application.Shared.CustomExceptions;
using AutoMapper;
using Domain.Shared.Abstractions;
using MediatR;
using Music.Application.Dtos;
using Music.Domain.Abstractions;
using System.Net;

namespace Music.Application.Albums.Queries
{
    internal sealed class GetAlbumQueryHandler : IRequestHandler<GetAlbumQuery, AlbumWithSongsDto>
    {
        private readonly IAlbumQueryRepository _AlbumQueryRepository;
        private readonly ILinkService _LinkService;
        private readonly IMapper _Mapper;
        public GetAlbumQueryHandler(IAlbumQueryRepository albumQueryRepository,
            ILinkService linkService,
            IMapper mapper)
        {
            _AlbumQueryRepository = albumQueryRepository;
            _LinkService = linkService;
            _Mapper = mapper;
        }
        public async Task<AlbumWithSongsDto> Handle(GetAlbumQuery request, CancellationToken cancellationToken)
        {
            AlbumWithSongsDto? album = _Mapper
                .Map<AlbumWithSongsDto>(await _AlbumQueryRepository
                .GetByIdAsync(request.AlbumId, false));

            if (album is null)
            {
                throw new AppException("No such album exists!", HttpStatusCode.NotFound);
            }

            AddLinksForAlbum(album);

            return album;
        }

        private void AddLinksForAlbum(AlbumWithSongsDto albumDto)
        {
            albumDto.Links.Add(_LinkService
                .Generate("AddSong",
                new { albumId = albumDto.Id },
                "add-song",
            "PUT"));

            albumDto.Links.Add(_LinkService
                .Generate("RemoveSong",
                new { albumId = albumDto.Id },
                "add-song",
            "PUT"));

            albumDto.Links.Add(_LinkService
                .Generate("PatchAlbum",
                new { albumId = albumDto.Id },
                "patch-album",
            "PATCH"));
        }
    }
}
