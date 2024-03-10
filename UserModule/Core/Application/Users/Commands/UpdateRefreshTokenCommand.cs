using MediatR;

namespace Users.Application.Users.Commands
{
    public sealed record UpdateRefreshTokenCommand(string RefreshToken) : IRequest;
}
