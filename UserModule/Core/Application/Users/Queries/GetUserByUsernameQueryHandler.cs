using Users.Application.Users.Commands;
using AutoMapper;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Dtos;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Queries
{
    internal sealed class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserDto>
    {
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IMapper _Mapper;
        public GetUserByUsernameQueryHandler(IUserQueryRepository userQueryRepository,
            IMapper mapper)
        {
            _UserQueryRepository = userQueryRepository;
            _Mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            UserDto? user = _Mapper.Map<UserDto>
                (await _UserQueryRepository.GetByNameAsync(request.Username));

            return user;
        }
    }
}
