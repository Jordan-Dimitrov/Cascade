using Application.Shared.Abstractions;
using MediatR;
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
            await _EventBus
                .PublisAsync(new UserCreatedIntegrationEvent(notification.UserId,
                notification.Username, notification.Role));
        }
    }
}
