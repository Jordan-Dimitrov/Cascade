using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        RefreshToken GenerateRefreshToken();
        UserPassword CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string GetUsernameFromJwtToken(string token);
        string GetRoleFromJwtToken(string token);
        DateTime GetDateFromJwtToken(string token);
        public void SetRefreshToken(RefreshToken newRefreshToken);
        public void SetJwtToken(string jwtToken);
        public void ClearTokens();
        public string CreateRandomToken();
    }
}
