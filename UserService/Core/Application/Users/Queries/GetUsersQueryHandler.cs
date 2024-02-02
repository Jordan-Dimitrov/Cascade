using Application.Dtos;
using AutoMapper;
using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.RequestFeatures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, (IEnumerable<UserDto> users, MetaData metaData)>
    {
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IMapper _Mapper;
        public GetUsersQueryHandler(IUserQueryRepository userQueryRepository,
            IMapper mapper)
        {
            _UserQueryRepository = userQueryRepository;
            _Mapper = mapper;
        }

        public async Task<(IEnumerable<UserDto> users, MetaData metaData)> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            PagedList<User> users = await _UserQueryRepository
                .GetUsersWithPaginationAsync(request.RequestParameters, false);

            IEnumerable<UserDto> usersDto = _Mapper.Map<IEnumerable<UserDto>>(users);

            return (users: usersDto, metaData: users.MetaData);
        }
    }
}
