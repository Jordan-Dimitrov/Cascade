using MediatR;

namespace Users.Application.Users.Commands
{
    public sealed record HideUserCommand(Guid Id) : IRequest;
}
