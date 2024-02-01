using Application.Dtos;
using Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public sealed record GetUsersQuery(RequestParameters RequestParameters) : IRequest<List<UserDto>>;
}
