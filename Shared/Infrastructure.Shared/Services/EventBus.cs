using Application.Shared.Abstractions;
using MassTransit;

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
