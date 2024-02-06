using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Abstractions;

namespace Users.Application.Users.Queries
{
    internal sealed class GetRoleFromJwtHandler : IRequestHandler<GetRoleFromJwtQuery, string>
    {
        private readonly IAuthService _AuthService;
        public GetRoleFromJwtHandler(IAuthService authService)
        {
            _AuthService = authService;
        }
        public Task<string> Handle(GetRoleFromJwtQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_AuthService.GetRoleFromJwtToken(request.JwtToken));
        }
    }
}
