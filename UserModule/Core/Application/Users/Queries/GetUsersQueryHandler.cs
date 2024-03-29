﻿using Application.Shared.CustomExceptions;
using AutoMapper;
using Domain.Shared.Abstractions;
using Domain.Shared.RequestFeatures;
using MediatR;
using System.Dynamic;
using System.Net;
using Users.Application.Dtos;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Application.Users.Queries
{
    internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, (IEnumerable<ExpandoObject> users, MetaData metaData)>
    {
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IMapper _Mapper;
        private readonly IDataShaper<UserDto> _DataShaper;
        public GetUsersQueryHandler(IUserQueryRepository userQueryRepository,
            IMapper mapper,
            IDataShaper<UserDto> dataShaper)
        {
            _UserQueryRepository = userQueryRepository;
            _Mapper = mapper;
            _DataShaper = dataShaper;
        }

        public async Task<(IEnumerable<ExpandoObject> users, MetaData metaData)> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            if (request.RequestParameters.MaxRole < request.RequestParameters.MinRole)
            {
                throw new AppException("Invalid range", HttpStatusCode.BadRequest);
            }
            PagedList<User> users = await _UserQueryRepository
                .GetUsersWithPaginationAsync(request.RequestParameters, false);

            IEnumerable<UserDto> usersDto = _Mapper.Map<IEnumerable<UserDto>>(users);

            IEnumerable<ExpandoObject> shapedData = _DataShaper
                .ShapeData(usersDto, request.RequestParameters.Fields);

            return (users: shapedData, metaData: users.MetaData);
        }
    }
}
