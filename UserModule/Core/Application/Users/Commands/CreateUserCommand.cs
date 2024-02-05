using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public sealed record CreateUserCommand(string Username,
        string Password,
        UserRole PermissionType) : IRequest<Guid>;

}
