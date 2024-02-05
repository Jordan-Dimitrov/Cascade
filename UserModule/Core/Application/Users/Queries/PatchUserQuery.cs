using Application.Dtos;
using Domain.Aggregates.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public sealed record PatchUserQuery(Guid Id) : IRequest<(UserPatchDto UserToPatch, User User)>;
}
