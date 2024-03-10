using Application.Shared.Abstractions;

namespace Users.IntegrationEvents
{
    public sealed record UserCreatedIntegrationEvent(Guid UserId, string Username, int Role) : IntegragtionEvent;
}
