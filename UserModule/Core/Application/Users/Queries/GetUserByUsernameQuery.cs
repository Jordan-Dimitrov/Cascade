using Application.Dtos;
using Domain.Aggregates.UserAggregate;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public sealed record GetUserByUsernameQuery(string Username) : IRequest<UserDto>;
}
