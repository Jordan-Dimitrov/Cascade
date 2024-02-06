using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.DomainEvents;

namespace Users.Application.DomainEventsHandlers
{
    internal sealed class UserHiddenDomainEventHandler : INotificationHandler<UserHiddenDomainEvent>
    {
        public async Task Handle(UserHiddenDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(notification.UserId + "-----------------------------");
        }
    }
}
