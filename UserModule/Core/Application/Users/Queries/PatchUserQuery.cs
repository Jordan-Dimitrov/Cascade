using MediatR;
using Users.Application.Dtos;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Application.Users.Queries
{
    public sealed record PatchUserQuery(Guid Id) : IRequest<(UserPatchDto UserToPatch, User User)>;
}
