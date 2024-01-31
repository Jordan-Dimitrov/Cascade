using Application.Users.Queries;
using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.Exceptions;
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
        private readonly ISender _Sender;
        private readonly IUnitOfWork _UnitOfWork;
        public CreateUserCommandHandler(IUserCommandRepository userRepository,
            ISender sender,
            IAuthService authService,
            IRefreshTokenCommandRepository refreshTokenRepository,
            IUnitOfWork unitOfWork)
        {
            _UserRepository = userRepository;
            _AuthService = authService;
            _RefreshTokenRepository = refreshTokenRepository;
            _Sender = sender;
            _UnitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _Sender.Send(new GetUserByUsernameQuery(request.Username));

            if(user is not null)
            {
                throw new ApplicationException("Username already exists");
            }

            UserPassword pass = _AuthService.CreatePasswordHash(request.Password);
            Token token = new Token(_AuthService.CreateRandomToken());
            TokenDates dates = new TokenDates(DateTime.UtcNow, DateTime.UtcNow.AddDays(3));

            RefreshToken refreshToken = new RefreshToken(token, dates);
            user = new User(new Username(request.Username), pass.PasswordHash, pass.PasswordSalt,
                refreshToken, UserRole.User);

            await _RefreshTokenRepository.InsertAsync(refreshToken);
            await _UserRepository.InsertAsync(user);

            if (await _UnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }

            return user.Id;
        }
    }
}
