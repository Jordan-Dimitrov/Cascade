using Application.Shared.Abstractions;
using Application.Shared.Constants;
using Application.Shared.CustomExceptions;
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
using Users.Application;
using Users.Application.Abstractions;
using Users.Application.Constants;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.Wrappers;
using System.Net;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
namespace Users.Infrastructure.Services
{
    internal class AuthService : IAuthService
    {
        private const int _Length = 32;
        private readonly ICacheService _CacheService;
        private readonly JwtTokenSettings _JwtTokenSettings;
        private readonly RefreshTokenSettings _RefreshTokenSettings;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public AuthService(IOptions<JwtTokenSettings> jwtTokenSettings,
            IHttpContextAccessor httpContextAccessor,
            IOptions<RefreshTokenSettings> refreshTokenSettings,
            ICacheService cacheService)
        {
            _JwtTokenSettings = jwtTokenSettings.Value;
            _HttpContextAccessor = httpContextAccessor;
            _RefreshTokenSettings = refreshTokenSettings.Value;
            _CacheService = cacheService;
        }
        public string GenerateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username.Value),
                new Claim(ClaimTypes.Role, user.PermissionType.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
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
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(_Length));
        }

        public async Task<RefreshToken> GenerateRefreshToken(User user)
        {
            RefreshToken token = new RefreshToken(Convert.ToHexString(RandomNumberGenerator.GetBytes(_Length)),
                 DateTime.UtcNow.AddDays(_RefreshTokenSettings.DaysExpiry), user.Id);

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.UtcNow.AddDays(_RefreshTokenSettings.DaysExpiry)
            };

            await _CacheService.SetAsync(CacheKeys.GetRefreshTokenKey(token.Value), token, options);

            return token;
        }

        public async Task<RefreshToken> GetRefreshToken(string token)
        {
            RefreshToken? value = await _CacheService
                .GetAsync<RefreshToken>(CacheKeys.GetRefreshTokenKey(token));

            if(value is null)
            {
                throw new AppException("Invalid or expired token", HttpStatusCode.NotFound);
            }

            return value;
        }

        public async Task RemoveRefreshToken(string token)
        {
            await _CacheService
                .RemoveAsync(CacheKeys.GetRefreshTokenKey(token));
        }

        public UserPassword CreatePasswordHash(string password)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                UserPassword pass = new UserPassword();
                pass.PasswordSalt = hmac.Key;
                pass.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return pass;
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
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

        public void SetRefreshToken(RefreshToken refreshToken)
        {
            HttpResponse? response = _HttpContextAccessor.HttpContext?.Response;

            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
            };

            response?.Cookies.Append(Tokens.RefreshToken, refreshToken.Value, cookieOptions);
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
