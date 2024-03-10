using MediatR;
using Users.Application.Dtos;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Application.Users.Commands
{
    public sealed record SaveChangesForPatchCommand(UserPatchDto UserToPatch, User User) : IRequest;
}
