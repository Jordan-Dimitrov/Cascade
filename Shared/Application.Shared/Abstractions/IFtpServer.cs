using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Abstractions
{
    public interface IFtpServer
    {
        Task<byte[]> GetAsync(string filePath);
        Task DeleteAsync(string filePath);
        Task<string> UploadAsync(string filePath, byte[] file);
        Task<bool> Exists(string filePath);
    }
}
