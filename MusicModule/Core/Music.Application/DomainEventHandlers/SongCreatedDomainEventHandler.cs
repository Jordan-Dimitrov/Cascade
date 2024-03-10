using Application.Shared.Abstractions;
using MediatR;
using Music.Domain.DomainEvents;
using Music.IntegrationEvents;

namespace Music.Application.DomainEventHandlers
{
    internal sealed class SongCreatedDomainEventHandler : INotificationHandler<SongCreatedDomainEvent>
    {
        private readonly IEventBus _EventBus;
        public SongCreatedDomainEventHandler(IEventBus eventBus)
        {
            _EventBus = eventBus;
        }
        public async Task Handle(SongCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _EventBus
                .PublisAsync(new SongCreatedIntegrationEvent(notification.FileName,
                notification.Lyrics));
        }
    }
}
