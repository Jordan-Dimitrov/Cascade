using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.DomainEntities;
using Users.Domain.Wrappers;

namespace Users.Application.Abstractions
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        RefreshToken GenerateRefreshToken();
        UserPassword CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        DateTime GetDateFromJwtToken(string token);
        public void SetRefreshToken(RefreshToken newRefreshToken);
        public void SetJwtToken(string jwtToken);
        public void ClearTokens();
        public string CreateRandomToken();
    }
}
