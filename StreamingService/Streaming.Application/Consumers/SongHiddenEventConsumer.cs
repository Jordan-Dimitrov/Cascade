using MassTransit;
using Music.IntegrationEvents;
using Streaming.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await _FileProcessingService.RemoveAsync(context.Message.FileName);
        }
    }
}
