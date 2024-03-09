using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Users.Commands
{
    public sealed record LogoutUserCommand(string Jwt, string RefreshToken) : IRequest;
}
