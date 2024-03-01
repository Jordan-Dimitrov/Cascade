using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public static class Utils
    {
        public static string GetUsernameFromJwtToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            string username = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;

            return username;
        }
        public static string GetRoleFromJwtToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            string role = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

            return role;
        }
        public static async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
        {
            if (file == null || Path.GetExtension(file.FileName).ToLowerInvariant() != ".mp3")
            {
                throw new ApplicationException("Invalid file");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                return memoryStream.ToArray();
            }
        }
    }
}
