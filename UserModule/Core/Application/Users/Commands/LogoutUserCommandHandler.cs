using Application.Shared;
using Domain.Shared.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Abstractions;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.DomainEntities;

namespace Users.Application.Users.Commands
{
    internal sealed class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
    {
        private readonly IAuthService _AuthService;
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IUserCommandRepository _UserCommandRepository;
        private readonly IUserUnitOfWork _UnitOfWork;
        public LogoutUserCommandHandler(IAuthService authService,
            IUserCommandRepository userCommandRepository,
            IUserQueryRepository userQueryRepository,
            IUserUnitOfWork unitOfWork)
        {
            _AuthService = authService;
            _UserQueryRepository = userQueryRepository;
            _UserCommandRepository = userCommandRepository;
            _UnitOfWork = unitOfWork;
        }
        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            string username = Utils.GetUsernameFromJwtToken(request.Jwt);

            User? user = await _UserQueryRepository.GetByNameAsync(username);

            if (user is null)
            {
                throw new ApplicationException("User not found");
            }

            RefreshToken refreshToken = _AuthService.GenerateRefreshToken();

            await _UserCommandRepository.UpdateRefreshTokenAsync(user, refreshToken);

            if (await _UnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }

            _AuthService.ClearTokens();
        }
    }
}
