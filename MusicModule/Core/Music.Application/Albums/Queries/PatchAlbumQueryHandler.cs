using Application.Shared;
using Application.Shared.Abstractions;
using Application.Shared.CustomExceptions;
using AutoMapper;
using MediatR;
using Music.Application.Dtos;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Queries
{
    internal sealed class PatchAlbumQueryHandler : IRequestHandler<PatchAlbumQuery, (AlbumPatchDto, Album Album)>
    {
        private readonly IAlbumQueryRepository _AlbumQueryRepository;
        private readonly IArtistQueryRepository _ArtistQueryRepository;
        private readonly IMapper _Mapper;
        private readonly IUserInfoService _UserInfoService;
        public PatchAlbumQueryHandler(IAlbumQueryRepository albumQueryRepository,
            IMapper mapper,
            IArtistQueryRepository artistQueryRepository,
            IUserInfoService userInfoService)
        {
            _AlbumQueryRepository = albumQueryRepository;
            _Mapper = mapper;
            _ArtistQueryRepository = artistQueryRepository;
            _UserInfoService = userInfoService;
        }
        public async Task<(AlbumPatchDto, Album Album)> Handle(PatchAlbumQuery request,
            CancellationToken cancellationToken)
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

            AlbumPatchDto albumPatchDto = _Mapper.Map<AlbumPatchDto>(album);

            return (albumPatchDto, album);
        }
    }
}
