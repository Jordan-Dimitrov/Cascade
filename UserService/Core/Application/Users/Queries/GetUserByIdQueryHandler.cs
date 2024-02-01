using Application.Dtos;
using AutoMapper;
using Dapper;
using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
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
    internal sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IMapper _Mapper;
        public GetUserByIdQueryHandler(IUserQueryRepository userQueryRepository, IMapper mapper)
        {
            _UserQueryRepository = userQueryRepository;
            _Mapper = mapper;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            UserDto? user = _Mapper.Map<UserDto>(await _UserQueryRepository.
                GetByIdAsync(request.UserId, true));

            if(user is null)
            {
                throw new EntityNotFoundException(typeof(User));
            }

            return user;

        }
    }
}
