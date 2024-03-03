﻿using MediatR;
using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;
using Users.Application.Abstractions;
using Application.Shared.CustomExceptions;
using System.Net;

namespace Users.Application.Users.Commands
{
    internal sealed class HideUserCommandHandler : IRequestHandler<HideUserCommand>
    {
        private readonly IUserCommandRepository _UserCommandRepository;
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IUserUnitOfWork _UnitOfWork;
        public HideUserCommandHandler(IUserQueryRepository userQueryRepository,
            IUserCommandRepository userCommandRepository,
            IUserUnitOfWork unitOfWork)
        {
            _UserQueryRepository = userQueryRepository;
            _UserCommandRepository = userCommandRepository;
            _UnitOfWork = unitOfWork;
        }
        public async Task Handle(HideUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _UserQueryRepository.GetByIdAsync(request.Id, true);

            if (user is null)
            {
                throw new AppException("User not found", HttpStatusCode.NotFound);
            }

            user.HideUserDetails();

            await _UserCommandRepository.UpdateAsync(user);

            if (await _UnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }
        }
    }
}
