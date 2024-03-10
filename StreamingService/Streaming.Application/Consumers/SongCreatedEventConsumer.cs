using MassTransit;
using Music.IntegrationEvents;
using Streaming.Application.Abstractions;

namespace Streaming.Application.Consumers
{
    public sealed class SongCreatedEventConsumer : IConsumer<SongCreatedIntegrationEvent>
    {
        private readonly IFileProcessingService _FileProcessingService;
        public SongCreatedEventConsumer(IFileProcessingService fileProcessingService)
        {
            _FileProcessingService = fileProcessingService;
        }
        public async Task Consume(ConsumeContext<SongCreatedIntegrationEvent> context)
        {
            await _FileProcessingService.UploadSongAsync(context.Message.FileName,
                context.Message.Lyrics);
        }
    }
}
