using Application.Shared.CustomExceptions;
using AutoMapper;
using MediatR;
using System.Net;
using Users.Application.Dtos;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Application.Users.Queries
{
    internal sealed class PatchUserQueryHandler : IRequestHandler<PatchUserQuery, (UserPatchDto UserToPatch, User User)>
    {
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IMapper _Mapper;
        public PatchUserQueryHandler(IUserQueryRepository userQueryRepository,
            IMapper mapper)
        {
            _UserQueryRepository = userQueryRepository;
            _Mapper = mapper;
        }
        public async Task<(UserPatchDto UserToPatch, User User)> Handle(PatchUserQuery request,
            CancellationToken cancellationToken)
        {
            User? user = await _UserQueryRepository.GetByIdAsync(request.Id, true);

            if (user is null)
            {
                throw new AppException("No such user exists!", HttpStatusCode.NotFound);
            }

            UserPatchDto userToPatch = _Mapper.Map<UserPatchDto>(user);

            return (userToPatch, user);
        }
    }
}
