using MassTransit;
using Music.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaming.Application.Consumers
{
    public sealed class SongHiddenEventConsumer : IConsumer<SongHiddenIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<SongHiddenIntegrationEvent> context)
        {
            Console.WriteLine("agfkjhgafas");
        }
    }
}
