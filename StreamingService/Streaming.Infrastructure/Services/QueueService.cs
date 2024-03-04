using Microsoft.Extensions.Hosting;
using Streaming.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
