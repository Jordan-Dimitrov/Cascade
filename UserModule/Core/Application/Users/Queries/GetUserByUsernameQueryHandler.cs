using Application.Dtos;
using Application.Users.Commands;
using AutoMapper;
using Dapper;
using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
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
                (await _UserQueryRepository.GetUserByNameAsync(request.Username));
               
            return user;
        }
    }
}
