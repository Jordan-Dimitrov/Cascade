using Application.Shared.Constants;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Abstractions;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.DomainEntities;
using Users.Domain.ValueObjects;
using Users.Domain.Wrappers;

namespace Infrastructure.Services
{
    internal class AuthService : IAuthService
    {
        private readonly JwtTokenSettings _JwtTokenSettings;
        private readonly RefreshTokenSettings _RefreshTokenSettings;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public AuthService(IOptions<JwtTokenSettings> jwtTokenSettings,
            IHttpContextAccessor httpContextAccessor,
            IOptions<RefreshTokenSettings> refreshTokenSettings)
        {
            _JwtTokenSettings = jwtTokenSettings.Value;
            _HttpContextAccessor = httpContextAccessor;
            _RefreshTokenSettings = refreshTokenSettings.Value;
        }
        public string GenerateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username.Value),
                new Claim(ClaimTypes.Role, user.PermissionType.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_JwtTokenSettings.Token));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(_JwtTokenSettings.MinutesExpiry),
                signingCredentials: creds);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        }

        public RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken(new Token(Convert.ToHexString(RandomNumberGenerator.GetBytes(32))),
                new TokenDates(DateTime.UtcNow, DateTime.UtcNow.AddDays(_RefreshTokenSettings.DaysExpiry)));

            return refreshToken;
        }
        public UserPassword CreatePasswordHash(string password)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                UserPassword pass = new UserPassword();
                pass.PasswordSalt = hmac.Key;
                pass.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return pass;
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
            {
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public DateTime GetDateFromJwtToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            DateTime date = jwtToken.ValidTo;

            return date;
        }

        public void SetRefreshToken(RefreshToken newRefreshToken)
        {
            HttpResponse? response = _HttpContextAccessor.HttpContext?.Response;

            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.TokenDates.TokenExpires,
            };

            response?.Cookies.Append(Tokens.RefreshToken, newRefreshToken.Token.Value, cookieOptions);
        }
        public void SetJwtToken(string jwtToken)
        {
            HttpResponse? response = _HttpContextAccessor.HttpContext?.Response;

            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = GetDateFromJwtToken(jwtToken),

                HttpOnly = true,
            };

            response?.Cookies.Append(Tokens.JwtToken, jwtToken, cookieOptions);
        }
        public void ClearTokens()
        {
            HttpResponse? response = _HttpContextAccessor.HttpContext?.Response;

            response?.Cookies.Delete(Tokens.JwtToken);

            response?.Cookies.Delete(Tokens.RefreshToken);
        }

    }
}
