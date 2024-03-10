using Application.Shared.Abstractions;

namespace Music.IntegrationEvents
{
    public sealed record SongHiddenIntegrationEvent(string FileName) : IntegragtionEvent;
}
