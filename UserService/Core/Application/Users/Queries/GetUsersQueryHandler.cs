using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ICollection<UserDto>>
    {
        public GetUsersQueryHandler()
        {

        }
        public Task<ICollection<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
