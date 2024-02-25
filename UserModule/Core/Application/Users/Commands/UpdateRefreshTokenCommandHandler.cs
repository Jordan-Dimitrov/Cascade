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
            User? user = await _UserQueryRepository.GetUserByRefreshTokenAsync(request.RefreshToken);

            if (user is null)
            {
                throw new ApplicationException("Invalid Refresh Token.");
            }

            user.RefreshToken.TokenDates.CheckTokenDates();

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
