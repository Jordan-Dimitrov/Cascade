using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Dtos;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Application.Users.Commands
{
    public sealed record SaveChangesForPatchCommand(UserPatchDto UserToPatch, User User) : IRequest;
}
