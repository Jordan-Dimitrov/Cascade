using Application.Shared.CustomExceptions;
using MediatR;
using System.Net;
using Users.Application.Abstractions;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Application.Users.Commands
{
    internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand>
    {
        private readonly IUserCommandRepository _UserCommandRepository;
        private readonly IAuthService _AuthService;
        private readonly IUserUnitOfWork _UnitOfWork;
        private readonly IUserQueryRepository _UserQueryRepository;
        public LoginUserCommandHandler(ISender sender, IAuthService authService,
            IUserCommandRepository userCommandRepository,
            IUserUnitOfWork unitOfWork, IUserQueryRepository userQueryRepository)
        {
            _AuthService = authService;
            _UserCommandRepository = userCommandRepository;
            _UnitOfWork = unitOfWork;
            _UserQueryRepository = userQueryRepository;
        }
        public async Task Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _UserQueryRepository.GetByNameAsync(request.Username);

            if (user is null)
            {
                throw new AppException("User not found", HttpStatusCode.NotFound);
            }

            if (!_AuthService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new AppException("Passwords do not match", HttpStatusCode.BadRequest);
            }

            RefreshToken refreshToken = await _AuthService.GenerateRefreshToken(user);

            string jwtToken = _AuthService.GenerateJwtToken(user);

            _AuthService.SetRefreshToken(refreshToken);
            _AuthService.SetJwtToken(jwtToken);
        }
    }
}
