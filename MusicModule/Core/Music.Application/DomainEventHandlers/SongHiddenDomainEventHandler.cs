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
    internal sealed class SongHiddenDomainEventHandler : INotificationHandler<SongHiddenDomainEvent>
    {
        private readonly IEventBus _EventBus;
        public SongHiddenDomainEventHandler(IEventBus eventBus, IFtpServer ftpServer)
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
