namespace Application.Shared.Abstractions
{
    public interface IFtpClient
    {
        Task<byte[]> GetAsync(string filePath);
        Task DeleteAsync(string filePath);
        Task<string> UploadAsync(string filePath, byte[] file);
        Task<bool> Exists(string filePath);
    }
}
