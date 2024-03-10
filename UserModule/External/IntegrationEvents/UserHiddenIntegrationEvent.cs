using Application.Shared.Abstractions;

namespace Users.IntegrationEvents
{
    public sealed record UserHiddenIntegrationEvent(Guid UserId, int Role) : IntegragtionEvent;
}
