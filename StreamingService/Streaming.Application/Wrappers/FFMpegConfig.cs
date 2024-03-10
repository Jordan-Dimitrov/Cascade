namespace Streaming.Application.Wrappers
{
    public class FFMpegConfig
    {
        public string FFMpegBinaryDirectory { get; set; } = null!;
        public string TemporaryFilesDirectory { get; set; } = null!;
        public int ConversionThreads { get; set; }
    }
}
