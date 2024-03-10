using MediatR;

namespace Users.Application.Users.Commands
{
    public sealed record LogoutUserCommand(string Jwt, string RefreshToken) : IRequest;
}
