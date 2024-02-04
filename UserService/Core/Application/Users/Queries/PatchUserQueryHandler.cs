using Application.Dtos;
using AutoMapper;
using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
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
                throw new ApplicationException("User not found");
            }

            UserPatchDto userToPatch = _Mapper.Map<UserPatchDto>(user);

            return (userToPatch, user);
        }
    }
}
