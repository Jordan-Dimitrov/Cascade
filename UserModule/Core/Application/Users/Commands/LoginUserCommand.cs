using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Users.Commands
{
    public sealed record LoginUserCommand(string Username, string Password) : IRequest;
}
