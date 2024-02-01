using Application.Users.Queries;
using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand>
    {
        private readonly IUserCommandRepository _UserCommandRepository;
        private readonly ISender _Sender;
        private readonly IAuthService _AuthService;
        private readonly IUnitOfWork _UnitOfWork;
        public LoginUserCommandHandler(ISender sender, IAuthService authService,
            IUserCommandRepository userCommandRepository,
            IUnitOfWork unitOfWork)
        {
            _Sender = sender;
            _AuthService = authService;
            _UserCommandRepository = userCommandRepository;
            _UnitOfWork = unitOfWork;
        }
        public async Task Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _Sender.Send(new GetUserByUsernameQuery(request.Username));

            if(user is null)
            {
                throw new ApplicationException("User not found");
            }

            if (!_AuthService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new ApplicationException("Passwords do not match");
            }

            RefreshToken refreshToken = _AuthService.GenerateRefreshToken();

            RefreshToken token = user.RefreshToken;
            user.RefreshToken = refreshToken;

            await _UserCommandRepository.UpdateRefreshTokenAsync(user);
            await _UserCommandRepository.RemoveOldRefreshTokenAsync(token);

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
