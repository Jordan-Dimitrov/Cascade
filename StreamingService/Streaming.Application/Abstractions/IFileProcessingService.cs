using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaming.Application.Abstractions
{
    public interface IFileProcessingService
    {
        Task RemoveAsync(string fileName);
        Task<string> UploadSongAsync(byte[] file, string filename, string[] lyrics);
        Task<(FileStream, string)> GetSong(string fileName);
    }
}
