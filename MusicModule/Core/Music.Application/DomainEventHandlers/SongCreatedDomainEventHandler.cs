using Application.Shared.Abstractions;
using MediatR;
using Music.Domain.DomainEvents;
using Music.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                notification.File));
        }
    }
}
