using Application.Shared.CustomExceptions;
using AutoMapper;
using Domain.Shared.Abstractions;
using Domain.Shared.RequestFeatures;
using MediatR;
using Music.Application.Dtos;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using System.Dynamic;
using System.Net;

namespace Music.Application.Albums.Queries
{
    internal sealed class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery,
        (IEnumerable<ExpandoObject> albums, MetaData metaData)>
    {
        private readonly IAlbumQueryRepository _AlbumQueryRepository;
        private readonly IMapper _Mapper;
        private readonly IDataShaper<AlbumDto> _DataShaper;
        public GetAlbumsQueryHandler(IAlbumQueryRepository albumQueryRepository,
            IMapper mapper,
            IDataShaper<AlbumDto> dataShaper)
        {
            _AlbumQueryRepository = albumQueryRepository;
            _Mapper = mapper;
            _DataShaper = dataShaper;
        }

        public async Task<(IEnumerable<ExpandoObject> albums, MetaData metaData)> Handle(GetAlbumsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.RequestParameters.MaxDateCreated < request.RequestParameters.MinDateCreated)
            {
                throw new AppException("Invalid range", HttpStatusCode.BadRequest);
            }

            PagedList<Album> albums = await _AlbumQueryRepository
                .GetAlbumsWithPaginationAsync(request.RequestParameters, false);

            IEnumerable<AlbumDto> usersDto = _Mapper.Map<IEnumerable<AlbumDto>>(albums);

            IEnumerable<ExpandoObject> shapedData = _DataShaper
                .ShapeData(usersDto, request.RequestParameters.Fields);

            return (albums: shapedData, metaData: albums.MetaData);
        }
    }
}
