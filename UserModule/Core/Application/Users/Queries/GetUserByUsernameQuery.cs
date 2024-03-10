using MediatR;
using Users.Application.Dtos;

namespace Users.Application.Users.Queries
{
    public sealed record GetUserByUsernameQuery(string Username) : IRequest<UserDto>;
}
