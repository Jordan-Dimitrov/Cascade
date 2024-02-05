using Domain.DomainEvents;
using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DomainEventsHandlers
{
    internal sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(notification.user.PermissionType + "-----------------------------");
        }
    }
}
