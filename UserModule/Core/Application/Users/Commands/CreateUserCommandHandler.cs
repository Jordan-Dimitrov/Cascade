using Application.Shared.CustomExceptions;
using MediatR;
using System.Net;
using Users.Application.Abstractions;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.ValueObjects;
using Users.Domain.Wrappers;

namespace Users.Application.Users.Commands
{
    internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserCommandRepository _UserRepository;
        private readonly IAuthService _AuthService;
        private readonly IUserUnitOfWork _UnitOfWork;
        private readonly IUserQueryRepository _UserQueryRepository;
        public CreateUserCommandHandler(IUserCommandRepository userRepository,
            IAuthService authService,
            IUserUnitOfWork unitOfWork,
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
                throw new AppException("Username already exists", HttpStatusCode.BadRequest);
            }

            UserPassword pass = _AuthService.CreatePasswordHash(request.Password);

            User user = User.CreateUser(request.Username, pass.PasswordHash, pass.PasswordSalt,
                request.PermissionType);

            await _UserRepository.InsertAsync(user);

            if (await _UnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }

            return user.Id;
        }
    }
}
