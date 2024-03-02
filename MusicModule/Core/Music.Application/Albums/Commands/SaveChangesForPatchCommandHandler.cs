using AutoMapper;
using MediatR;
using Music.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Albums.Commands
{
    internal sealed class SaveChangesForPatchCommandHandler : IRequestHandler<SaveChangesForPatchCommand>
    {
        private readonly IMusicUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;
        public SaveChangesForPatchCommandHandler(IMusicUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }
        public async Task Handle(SaveChangesForPatchCommand request, CancellationToken cancellationToken)
        {
            _Mapper.Map(request.AlbumToPatch, request.Album);

            if (await _UnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }
        }
    }
}
