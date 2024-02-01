using Application.Dtos;
using AutoMapper;
using Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IMapper _Mapper;
        public GetUsersQueryHandler(IUserQueryRepository userQueryRepository,
            IMapper mapper)
        {
            _UserQueryRepository = userQueryRepository;
            _Mapper = mapper;
        }
        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            List<UserDto> users = _Mapper.Map<List<UserDto>>(await _UserQueryRepository
                .GetUsersWithPaginationAsync(request.RequestParameters, false));

            return users;
        }
    }
}
