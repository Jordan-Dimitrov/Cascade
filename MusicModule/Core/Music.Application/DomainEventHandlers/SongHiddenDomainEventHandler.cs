using Application.Shared.Abstractions;
using MediatR;
using Music.Domain.DomainEvents;
using Music.IntegrationEvents;

namespace Music.Application.DomainEventHandlers
{
    internal sealed class SongHiddenDomainEventHandler : INotificationHandler<SongHiddenDomainEvent>
    {
        private readonly IEventBus _EventBus;
        public SongHiddenDomainEventHandler(IEventBus eventBus, IFtpClient ftpServer)
        {
            _EventBus = eventBus;
        }
        public async Task Handle(SongHiddenDomainEvent notification, CancellationToken cancellationToken)
        {
            await _EventBus
                .PublisAsync(new SongHiddenIntegrationEvent(notification.FileName));
        }
    }
}
