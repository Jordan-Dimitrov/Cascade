using Microsoft.AspNetCore.Http;

namespace Application.Shared.Abstractions
{
    public interface IFileConversionService
    {
        Task<byte[]> ConvertToByteArrayAsync(IFormFile file);
        string OriginalFileName(string fileName, string generated);
        public string GenerateRandomString();
    }
}
