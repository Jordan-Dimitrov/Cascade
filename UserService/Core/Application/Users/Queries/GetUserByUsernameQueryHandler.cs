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
    internal sealed class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, User>
    {
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IMapper _Mapper;
        public GetUserByUsernameQueryHandler(IUserQueryRepository userQueryRepository,
            IAuthService authService,
            IMapper mapper,
            IMediator mediator)
        {
            _UserQueryRepository = userQueryRepository;
            _Mapper = mapper;
        }

        public async Task<User> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            User? user = _Mapper.Map<User>
                (await _UserQueryRepository.GetUserByNameAsync(request.Username));
               
            return user;
        }
    }
}
