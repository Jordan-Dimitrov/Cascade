using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Abstractions
{
    public interface IFileConversionService
    {
        Task<byte[]> ConvertToByteArrayAsync(IFormFile file);
        string OriginalFileName(string fileName, string generated);
        public string GenerateRandomString();
    }
}
