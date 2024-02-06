using Users.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Users.Queries
{
    public sealed record GetRoleFromJwtQuery(string JwtToken) : IRequest<string>;
}
