namespace Streaming.Application.Abstractions
{
    public interface IFileProcessingService
    {
        Task RemoveFile(string fileName);
        Task<string> UploadSongAsync(string filename, string[] lyrics);
        Task<(MemoryStream, string)> GetSong(string fileName);
    }
}
