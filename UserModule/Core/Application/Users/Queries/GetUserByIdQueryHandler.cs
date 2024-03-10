using Application.Shared.CustomExceptions;
using AutoMapper;
using Domain.Shared.Abstractions;
using MediatR;
using System.Net;
using Users.Application.Dtos;
using Users.Domain.Abstractions;

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
                throw new AppException("User not found", HttpStatusCode.NotFound);
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
