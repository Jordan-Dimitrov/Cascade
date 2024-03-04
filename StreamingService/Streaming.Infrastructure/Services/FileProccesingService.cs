using Application.Shared.CustomExceptions;
using FFMpegCore;
using FFMpegCore.Enums;
using Streaming.Application.Abstractions;
using Streaming.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Streaming.Infrastructure.Services
{
    internal sealed class FileProccesingService : IFileProcessingService
    {
        private readonly string _UploadsDirectory = Path
            .Combine(AppDomain.CurrentDomain.BaseDirectory, "Media");

        private readonly IBackgroundQueue _BackgroundQueue;
        private readonly FFMpegConfig _FFMpegConfig;

        public FileProccesingService(IBackgroundQueue backgroundQueue, FFMpegConfig fFMpegConfig)
        {
            _BackgroundQueue = backgroundQueue;
            _FFMpegConfig = fFMpegConfig;

            if (!Directory.Exists(_UploadsDirectory))
            {
                Directory.CreateDirectory(_UploadsDirectory);
            }
        }

        private string GetContentType(string fileName)
        {
            if (fileName.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase))
            {
                return "audio/ogg";
            }
            else
            {
                return "unsupported";
            }
        }

        public async Task<(FileStream, string)> GetSong(string fileName)
        {
            string filePath = Path.Combine(_UploadsDirectory, fileName);

            if (!File.Exists(filePath))
            {
                throw new AppException("File not found", HttpStatusCode.NotFound);
            }

            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            string contentType = GetContentType(fileName);
            byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
            return (stream, contentType);
        }

        public async Task RemoveAsync(string fileName)
        {
            string filePath = Path.Combine(_UploadsDirectory, fileName);

            if (!File.Exists(filePath))
            {
                throw new AppException("File not found", HttpStatusCode.NotFound);
            }

            await Task.Run(() => File.Delete(filePath));
        }

        public async Task<string> UploadSongAsync(byte[] file, string filename)
        {
            string fileName = Path.GetFileName(filename);
            string fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

            string oggFileName = Path.GetFileNameWithoutExtension(filename) + ".ogg";
            string filePath = Path.Combine(_UploadsDirectory, fileName);
            string oggPath = Path.Combine(_UploadsDirectory, oggFileName);

            if (File.Exists(filePath) || File.Exists(oggPath))
            {
                throw new AppException("File already exists", HttpStatusCode.BadRequest);
            }

            await File.WriteAllBytesAsync(filePath, file);

            if (fileExtension == ".ogg")
            {
                return oggFileName;
            }

            await ConvertToOgg(filePath, Guid.NewGuid());

            return oggFileName;
        }

        private async Task ConvertToOgg(string inputPath, Guid taskId)
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
               .WithoutMetadata()
               .UsingThreads(_FFMpegConfig.ConversionThreads))
               .ProcessAsynchronously();

            await RemoveAsync(inputPath);

            _BackgroundQueue.AddStatus(taskId, "Finished");
        }
    }
}
