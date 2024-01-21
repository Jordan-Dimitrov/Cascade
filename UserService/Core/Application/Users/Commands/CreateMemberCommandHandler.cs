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
    internal sealed class CreateMemberCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IAuthService _AuthService;
        public CreateMemberCommandHandler(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IAuthService authService)
        {
            _UserRepository = userRepository;
            _UnitOfWork = unitOfWork;
            _AuthService = authService;
        }
        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Username username = new Username(request.Username);
            UserPassword pass = _AuthService.CreatePasswordHash(request.Password);
            Token token = new Token(_AuthService.CreateRandomToken());
            TokenDates dates = new TokenDates(DateTime.UtcNow, DateTime.UtcNow.AddDays(3));

            User user = new User(new Username(request.Username), pass.PasswordHash, pass.PasswordSalt,
                new RefreshToken(token, dates), request.PermissionType);

            await _UserRepository.InsertAsync(user);

            await _UnitOfWork.SaveChangesAsync();
        }
    }
}
