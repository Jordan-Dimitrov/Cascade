using Users.Application.Users.Queries;
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
using Users.Domain.DomainEntities;

namespace Users.Application.Users.Commands
{
    internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand>
    {
        private readonly IUserCommandRepository _UserCommandRepository;
        private readonly IAuthService _AuthService;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IUserQueryRepository _UserQueryRepository;
        public LoginUserCommandHandler(ISender sender, IAuthService authService,
            IUserCommandRepository userCommandRepository,
            IUnitOfWork unitOfWork, IUserQueryRepository userQueryRepository)
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
                throw new ApplicationException("User not found");
            }

            if (!_AuthService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new ApplicationException("Passwords do not match");
            }

            RefreshToken refreshToken = _AuthService.GenerateRefreshToken();

            await _UserCommandRepository.UpdateRefreshTokenAsync(user, refreshToken);

            if (await _UnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }

            string jwtToken = _AuthService.GenerateJwtToken(user);

            _AuthService.SetRefreshToken(refreshToken);
            _AuthService.SetJwtToken(jwtToken);
        }
    }
}
