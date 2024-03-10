using AutoMapper;
using MediatR;
using Users.Application.Abstractions;

namespace Users.Application.Users.Commands
{
    internal sealed class SaveChangesForPatchCommandHandler : IRequestHandler<SaveChangesForPatchCommand>
    {
        private readonly IUserUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;
        public SaveChangesForPatchCommandHandler(IUserUnitOfWork unitOfWork, IMapper mapper)
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
