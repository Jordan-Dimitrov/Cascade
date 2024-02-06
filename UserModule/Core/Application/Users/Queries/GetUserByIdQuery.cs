using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Dtos;

namespace Users.Application.Users.Queries
{
    public sealed record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;
}
