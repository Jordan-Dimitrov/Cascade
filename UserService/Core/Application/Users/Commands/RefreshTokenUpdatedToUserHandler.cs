using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.Users.Commands
{
    internal sealed class RefreshTokenUpdatedToUserHandler : INotificationHandler<RefreshTokenUpdatedToUser>
    {
        private readonly IUserCommandRepository _UserRepository;
        private readonly IAuthService _AuthService;
        private readonly IRefreshTokenCommandRepository _RefreshTokenRepository;
        public RefreshTokenUpdatedToUserHandler(IUserCommandRepository userRepository,
            IAuthService authService, IRefreshTokenCommandRepository refreshTokenRepository)
        {
            _UserRepository = userRepository;
            _AuthService = authService;
            _RefreshTokenRepository = refreshTokenRepository;
        }
        public async Task Handle(RefreshTokenUpdatedToUser notification, CancellationToken cancellationToken)
        {

            RefreshToken refreshToken = _AuthService.GenerateRefreshToken();

            await _RefreshTokenRepository.InsertAsync(refreshToken);
            notification.User.RefreshToken = refreshToken;
            await _UserRepository.UpdateRefreshTokenAsync(notification.User);
        }
    }
}
