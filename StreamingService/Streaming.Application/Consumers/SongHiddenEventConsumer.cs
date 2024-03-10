using MassTransit;
using Music.IntegrationEvents;
using Streaming.Application.Abstractions;

namespace Streaming.Application.Consumers
{
    public sealed class SongHiddenEventConsumer : IConsumer<SongHiddenIntegrationEvent>
    {
        private readonly IFileProcessingService _FileProcessingService;
        public SongHiddenEventConsumer(IFileProcessingService fileProcessingService)
        {
            _FileProcessingService = fileProcessingService;
        }

        public async Task Consume(ConsumeContext<SongHiddenIntegrationEvent> context)
        {
            await _FileProcessingService.RemoveFile(context.Message.FileName);
        }
    }
}
