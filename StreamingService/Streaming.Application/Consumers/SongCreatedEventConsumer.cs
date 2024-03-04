using MassTransit;
using Music.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaming.Application.Consumers
{
    public sealed class SongCreatedEventConsumer : IConsumer<SongCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<SongCreatedIntegrationEvent> context)
        {
            Console.WriteLine("SATGAG");
        }
    }
}
