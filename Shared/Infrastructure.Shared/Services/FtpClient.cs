using Application.Shared.Abstractions;
using Application.Shared.Constants;
using Application.Shared.CustomExceptions;
using Domain.Shared.Wrappers;
using Microsoft.Extensions.Options;
using System.Net;
namespace Infrastructure.Shared.Services
{
    public class FtpClient : IFtpClient
    {
        private readonly FtpServerSettings _FtpServerSettings;
        private readonly string _Uri;
        public FtpClient(IOptions<FtpServerSettings> ftpServerSettings)
        {
            _FtpServerSettings = ftpServerSettings.Value;
            _Uri = _FtpServerSettings.Host + $"/";
        }
        public async Task DeleteAsync(string filePath)
        {
            string path = _Uri + Path.GetFileName(filePath);
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest
                .Create(new Uri(path));

            ftpRequest.KeepAlive = false;
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;

            ftpRequest.Credentials = new NetworkCredential(_FtpServerSettings.Username,
                _FtpServerSettings.Password);

            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;

            await ftpRequest.GetResponseAsync();
        }

        public async Task<bool> Exists(string filePath)
        {
            string path = _Uri + Path.GetFileName(filePath);

            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest
                .Create(new Uri(path));

            ftpRequest.KeepAlive = false;
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;

            ftpRequest.Credentials = new NetworkCredential(_FtpServerSettings.Username,
                _FtpServerSettings.Password);

            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;

            try
            {
                FtpWebResponse response = (FtpWebResponse)await ftpRequest.GetResponseAsync();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<byte[]> GetAsync(string filePath)
        {
            try
            {
                string uri = _Uri + filePath;

                WebClient webClient = new WebClient();
                webClient.Credentials = new NetworkCredential(_FtpServerSettings.Username,
                    _FtpServerSettings.Password);

                byte[] fileBytes = await webClient.DownloadDataTaskAsync(uri);

                return fileBytes;
            }
            catch
            {
                throw new AppException("Can't find file", HttpStatusCode.NotFound);
            }
        }

        public async Task<string> UploadAsync(string filePath, byte[] file)
        {
            string path = _Uri + filePath;

            FtpWebRequest request = (FtpWebRequest)WebRequest
                .Create(new Uri(path));

            request.Method = WebRequestMethods.Ftp.UploadFile;

            request.Credentials = new NetworkCredential(_FtpServerSettings.Username,
                _FtpServerSettings.Password);

            using (MemoryStream fileStream = new MemoryStream(file))
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    await fileStream.CopyToAsync(requestStream);
                }
            }

            return path;
        }
    }
}
