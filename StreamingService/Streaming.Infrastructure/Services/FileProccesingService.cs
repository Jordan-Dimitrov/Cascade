using Application.Shared.Abstractions;
using Application.Shared.Constants;
using Application.Shared.CustomExceptions;
using ATL;
using Domain.Shared.Constants;
using FFMpegCore;
using FFMpegCore.Enums;
using Streaming.Application.Abstractions;
using Streaming.Application.Wrappers;
using System.Net;

namespace Streaming.Infrastructure.Services
{
    internal sealed class FileProccesingService : IFileProcessingService
    {
        private readonly string _UploadsDirectory = Path
            .Combine(AppDomain.CurrentDomain.BaseDirectory, Directories.Media);

        private readonly IBackgroundQueue _BackgroundQueue;
        private readonly FFMpegConfig _FFMpegConfig;
        private readonly IFtpClient _FtpServer;
        public FileProccesingService(IBackgroundQueue backgroundQueue,
            FFMpegConfig fFMpegConfig,
            IFtpClient ftpServer)
        {
            _BackgroundQueue = backgroundQueue;
            _FFMpegConfig = fFMpegConfig;
            _FtpServer = ftpServer;

            if (!Directory.Exists(_UploadsDirectory))
            {
                Directory.CreateDirectory(_UploadsDirectory);
            }
        }

        private string GetContentType(string fileName)
        {
            if (fileName.EndsWith(SupportedAudioMimeTypes.Ogg, StringComparison.OrdinalIgnoreCase))
            {
                return "audio/ogg";
            }
            else
            {
                return "unsupported";
            }
        }

        public async Task<(MemoryStream, string)> GetSong(string fileName)
        {
            byte[] file = await _FtpServer.GetAsync(fileName);

            if (file == null)
            {
                throw new AppException("File not found", HttpStatusCode.NotFound);
            }

            MemoryStream memoryStream = new MemoryStream(file);

            string contentType = GetContentType(fileName);

            return (memoryStream, contentType);
        }

        public async Task RemoveFile(string fileName)
        {
            await _FtpServer.DeleteAsync(fileName);
        }

        private async Task RemoveFileLocally(string fileName)
        {
            string filePath = Path.Combine(_UploadsDirectory, fileName);

            if (!File.Exists(filePath))
            {
                throw new AppException("File not found", HttpStatusCode.NotFound);
            }

            await Task.Run(() => File.Delete(filePath));
        }

        public async Task<string> UploadSongAsync(string filename, string[] lyrics)
        {
            string fileName = Path.GetFileName(filename);
            string fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

            string oggFileName = Path.GetFileNameWithoutExtension(filename) + SupportedAudioMimeTypes.Ogg;
            string filePath = Path.Combine(_UploadsDirectory, fileName);
            string oggPath = Path.Combine(_UploadsDirectory, oggFileName);

            if (await _FtpServer.Exists(oggPath))
            {
                throw new AppException("File already exists", HttpStatusCode.BadRequest);
            }

            byte[] file = await _FtpServer.GetAsync(fileName);

            await File.WriteAllBytesAsync(filePath, file);

            if (fileExtension == SupportedAudioMimeTypes.Ogg)
            {
                return oggFileName;
            }

            await ConvertToOgg(filePath, Guid.NewGuid(), lyrics);

            return oggFileName;
        }


        private async Task ConvertToOgg(string inputPath, Guid taskId, string[] lyrics)
        {
            string outputPath = Path.Combine(_UploadsDirectory, Path.GetFileNameWithoutExtension(inputPath) + ".ogg");

            _BackgroundQueue.AddStatus(taskId, "In progress");

            await FFMpegArguments
               .FromFileInput(inputPath)
               .OutputToFile(outputPath, false, options =>
               options.WithAudioCodec(AudioCodec.LibVorbis)
               .ForceFormat("ogg")
               .WithConstantRateFactor(30)
               .WithVariableBitrate(3)
               .WithFastStart()
               .UsingThreads(_FFMpegConfig.ConversionThreads))
               .ProcessAsynchronously();

            await RemoveFile(inputPath);

            await RemoveFileLocally(inputPath);

            Track track = new Track(outputPath);

            LyricsInfo lyricsInfo = new LyricsInfo();
            lyricsInfo.ParseLRC(string.Join("\n", lyrics));

            track.Lyrics = lyricsInfo;
            track.Save();

            await _FtpServer.UploadAsync(Path.GetFileName(outputPath),
                await File.ReadAllBytesAsync(outputPath));

            await RemoveFileLocally(outputPath);

            _BackgroundQueue.AddStatus(taskId, "Finished");
        }
    }
}
