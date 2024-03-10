using Domain.Shared.Constants;
using MediatR;

namespace Users.Application.Users.Commands
{
    public sealed record CreateUserCommand(string Username,
        string Password,
        UserRole PermissionType) : IRequest<Guid>;

}
