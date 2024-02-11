using AutoMapper;
using Dapper;
using MediatR;
using Domain.Shared.Abstractions;
using Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Dtos;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Application.Users.Queries
{
    internal sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IMapper _Mapper;
        private readonly ILinkService _LinkService;
        public GetUserByIdQueryHandler(IUserQueryRepository userQueryRepository,
            IMapper mapper,
            ILinkService linkService)
        {
            _UserQueryRepository = userQueryRepository;
            _Mapper = mapper;
            _LinkService = linkService;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            UserDto? user = _Mapper.Map<UserDto>(await _UserQueryRepository.
                GetByIdAsync(request.UserId, false));

            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User));
            }

            AddLinksForUser(user);

            return user;

        }
        private void AddLinksForUser(UserDto userDto)
        {
            userDto.Links.Add(_LinkService
                .Generate("HideUser",
                new { userId = userDto.Id },
                "hide-user",
            "PUT"));

            userDto.Links.Add(_LinkService
                .Generate("PatchUser",
                new { userId = userDto.Id },
                "patch-user",
            "PATCH"));
        }
    }
}
