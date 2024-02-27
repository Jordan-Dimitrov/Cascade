using Application.Shared.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.DomainEvents;
using Users.IntegrationEvents;

namespace Users.Application.DomainEventsHandlers
{
    internal sealed class UserHiddenDomainEventHandler : INotificationHandler<UserHiddenDomainEvent>
    {
        private readonly IEventBus _EventBus;
        public UserHiddenDomainEventHandler(IEventBus eventBus)
        {
            _EventBus = eventBus;
        }
        public async Task Handle(UserHiddenDomainEvent notification, CancellationToken cancellationToken)
        {
            await _EventBus
                .PublisAsync(new UserHiddenIntegrationEvent(notification.UserId, (int)notification.Role), cancellationToken);

            Console.WriteLine(notification.UserId + "-----------------------------");
        }
    }
}
