using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Dtos;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Application.Users.Queries
{
    public sealed record PatchUserQuery(Guid Id) : IRequest<(UserPatchDto UserToPatch, User User)>;
}
