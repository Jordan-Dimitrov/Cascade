using Application.Shared;
using AutoMapper;
using MediatR;
using Music.Application.Dtos;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Queries
{
    internal sealed class PatchAlbumQueryHandler : IRequestHandler<PatchAlbumQuery, (AlbumPatchDto, Album Album)>
    {
        private readonly IAlbumQueryRepository _AlbumQueryRepository;
        private readonly IArtistQueryRepository _ArtistQueryRepository;
        private readonly IMapper _Mapper;
        public PatchAlbumQueryHandler(IAlbumQueryRepository albumQueryRepository,
            IMapper mapper,
            IArtistQueryRepository artistQueryRepository)
        {
            _AlbumQueryRepository = albumQueryRepository;
            _Mapper = mapper;
            _ArtistQueryRepository = artistQueryRepository;
        }
        public async Task<(AlbumPatchDto, Album Album)> Handle(PatchAlbumQuery request,
            CancellationToken cancellationToken)
        {
            Artist? artist = await _ArtistQueryRepository
                .GetByNameAsync(Utils.GetUsernameFromJwtToken(request.JwtToken));

            if (artist is null)
            {
                throw new ApplicationException("No such artist exists!");
            }

            Album? album = await _AlbumQueryRepository.GetByIdAsync(request.AlbumId, true);

            if (album is null)
            {
                throw new ApplicationException("No such album exists!");
            }

            if (album.ArtistId != artist.Id)
            {
                throw new ApplicationException("Album does not belong to artist!");
            }

            AlbumPatchDto albumPatchDto = _Mapper.Map<AlbumPatchDto>(album);

            return (albumPatchDto, album);
        }
    }
}
