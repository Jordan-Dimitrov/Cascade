using Microsoft.Extensions.Hosting;
using Streaming.Application.Abstractions;

namespace Streaming.Infrastructure.Services
{
    internal sealed class QueueService : BackgroundService
    {
        private IBackgroundQueue _Queue;

        public QueueService(IBackgroundQueue queue)
        {
            _Queue = queue;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _Queue.ProcessQueueAsync(stoppingToken);
        }
    }
}
