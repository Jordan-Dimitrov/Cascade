using Application.Shared.Abstractions;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public sealed class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _PublishEndpoint;
        public EventBus(IPublishEndpoint publishEndpoint)
        {
            _PublishEndpoint = publishEndpoint;
        }

        public Task PublisAsync<T>(T message, CancellationToken cancellationToken = default)
            where T : class => _PublishEndpoint.Publish(message, cancellationToken);
    }
}
