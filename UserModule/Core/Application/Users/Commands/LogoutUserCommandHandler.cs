using Application.Shared;
using Application.Shared.Abstractions;
using Application.Shared.CustomExceptions;
using Domain.Shared.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Abstractions;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Application.Users.Commands
{
    internal sealed class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
    {
        private readonly IAuthService _AuthService;
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IUserCommandRepository _UserCommandRepository;
        private readonly IUserUnitOfWork _UnitOfWork;
        private readonly IUserInfoService _UserInfoService;
        public LogoutUserCommandHandler(IAuthService authService,
            IUserCommandRepository userCommandRepository,
            IUserQueryRepository userQueryRepository,
            IUserUnitOfWork unitOfWork,
            IUserInfoService userInfoService)
        {
            _AuthService = authService;
            _UserQueryRepository = userQueryRepository;
            _UserCommandRepository = userCommandRepository;
            _UnitOfWork = unitOfWork;
            _UserInfoService = userInfoService;
        }
        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            string username = _UserInfoService.GetUsernameFromJwtToken(request.Jwt);

            User? user = await _UserQueryRepository.GetByNameAsync(username);

            if (user is null)
            {
                throw new AppException("User not found", HttpStatusCode.NotFound);
            }

            await _AuthService.RemoveRefreshToken(request.RefreshToken);

            _AuthService.ClearTokens();
        }
    }
}
