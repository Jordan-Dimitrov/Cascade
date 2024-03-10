using MediatR;
using Users.Application.Dtos;

namespace Users.Application.Users.Queries
{
    public sealed record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;
}
