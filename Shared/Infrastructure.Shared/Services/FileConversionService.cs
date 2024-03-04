using Application.Shared.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public sealed class FileConversionService : IFileConversionService
    {
        private static int _ByteCount = 4;
        public async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
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

        public string OriginalFileName(string fileName, string generated)
        {
            string extension = Path.GetExtension(fileName);

            return $"{fileName
                .Substring(0, fileName.Length - extension.Length)}_{generated}{extension}";
        }

        public string GenerateRandomString()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(_ByteCount));
        }
    }
}
