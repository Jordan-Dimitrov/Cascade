using MediatR;

namespace Users.Application.Users.Commands
{
    public sealed record LoginUserCommand(string Username, string Password) : IRequest;
}
