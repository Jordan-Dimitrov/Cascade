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
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IUserQueryRepository _UserQueryRepository;
        public CreateUserCommandHandler(IUserCommandRepository userRepository,
            IAuthService authService,
            IUnitOfWork unitOfWork,
            IUserQueryRepository userQueryRepository)
        {
            _UserRepository = userRepository;
            _AuthService = authService;
            _UnitOfWork = unitOfWork;
            _UserQueryRepository = userQueryRepository;
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Username username = new Username(request.Username);

            if (await _UserQueryRepository.ExistsAsync(x => x.Username == username))
            {
                throw new ApplicationException("Username already exists");
            }

            UserPassword pass = _AuthService.CreatePasswordHash(request.Password);
            Token token = new Token(_AuthService.CreateRandomToken());
            RefreshToken refreshToken = _AuthService.GenerateRefreshToken();
            User user = User.CreateUser(request.Username, pass.PasswordHash, pass.PasswordSalt,
                refreshToken, UserRole.User);

            await _UserRepository.InsertAsync(user);

            if (await _UnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }

            return user.Id;
        }
    }
}
