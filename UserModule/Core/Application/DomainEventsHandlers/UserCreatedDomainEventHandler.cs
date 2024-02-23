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
    internal sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IEventBus _EventBus;
        public UserCreatedDomainEventHandler(IEventBus eventBus)
        {
            _EventBus = eventBus;
        }
        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(notification.user.PermissionType + "-----------------------------");
        }
    }
}
