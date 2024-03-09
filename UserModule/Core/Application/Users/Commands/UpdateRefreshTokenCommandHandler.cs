using MediatR;
using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Abstractions;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;
using Application.Shared.CustomExceptions;
using System.Net;

namespace Users.Application.Users.Commands
{
    internal sealed class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand>
    {
        private readonly IUserCommandRepository _UserCommandRepository;
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IAuthService _AuthService;
        private readonly IUserUnitOfWork _UnitOfWork;
        public UpdateRefreshTokenCommandHandler(IUserQueryRepository userQueryRepository,
            IUserCommandRepository userCommandRepository,
            IAuthService authService,
            IUserUnitOfWork unitOfWork)
        {
            _UserQueryRepository = userQueryRepository;
            _UserCommandRepository = userCommandRepository;
            _AuthService = authService;
            _UnitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            RefreshToken refreshToken = await _AuthService.GetRefreshToken(request.RefreshToken);

            User? user = await _UserQueryRepository.GetByIdAsync(refreshToken.UserId, false);

            if (user is null)
            {
                throw new AppException("Invalid Refresh Token.", HttpStatusCode.NotFound);
            }

            RefreshToken newRefreshToken = await _AuthService.GenerateRefreshToken(user);

            string jwtToken = _AuthService.GenerateJwtToken(user);

            _AuthService.SetRefreshToken(newRefreshToken);
            _AuthService.SetJwtToken(jwtToken);
        }
    }
}
