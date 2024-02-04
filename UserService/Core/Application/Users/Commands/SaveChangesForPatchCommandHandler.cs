using AutoMapper;
using Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    internal sealed class SaveChangesForPatchCommandHandler : IRequestHandler<SaveChangesForPatchCommand>
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;
        public SaveChangesForPatchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }
        public async Task Handle(SaveChangesForPatchCommand request, CancellationToken cancellationToken)
        {
            _Mapper.Map(request.UserToPatch, request.User);

            if (await _UnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }
        }
    }
}
