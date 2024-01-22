using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserCommandRepository _UserRepository;
        private readonly IAuthService _AuthService;
        private readonly IRefreshTokenCommandRepository _RefreshTokenRepository;
        public CreateUserCommandHandler(IUserCommandRepository userRepository,
            IUnitOfWork unitOfWork,
            IAuthService authService,
            IRefreshTokenCommandRepository refreshTokenRepository)
        {
            _UserRepository = userRepository;
            _AuthService = authService;
            _RefreshTokenRepository = refreshTokenRepository;
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Username username = new Username(request.Username);
            UserPassword pass = _AuthService.CreatePasswordHash(request.Password);
            Token token = new Token(_AuthService.CreateRandomToken());
            TokenDates dates = new TokenDates(DateTime.UtcNow, DateTime.UtcNow.AddDays(3));
            RefreshToken refreshToken = new RefreshToken(token, dates);
            User user = new User(new Username(request.Username), pass.PasswordHash, pass.PasswordSalt,
                refreshToken, UserRole.User);

            if(!await _RefreshTokenRepository.InsertAsync(refreshToken) || !await _UserRepository.InsertAsync(user))
            {
                throw new ApplicationException("Unexpected error");
            }

            return user.Id;
        }
    }
}
