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
    internal sealed class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery>
    {
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IAuthService _AuthService;
        private readonly IMapper _Mapper;
        private readonly IMediator _Mediator;
        public GetUserByUsernameQueryHandler(IUserQueryRepository userQueryRepository,
            IAuthService authService,
            IMapper mapper,
            IMediator mediator)
        {
            _UserQueryRepository = userQueryRepository;
            _AuthService = authService;
            _Mapper = mapper;
            _Mediator = mediator;
        }

        public async Task Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            User? user = await _UserQueryRepository.GetUserByNameAsync(request.Username);
                
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User));
            }

            string token = _AuthService.GenerateJwtToken(user);

            await _Mediator.Publish(new RefreshTokenUpdatedToUser(user, token));
        }
    }
}
