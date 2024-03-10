using Application.Shared.Abstractions;

namespace Music.IntegrationEvents
{
    public sealed record SongCreatedIntegrationEvent(string FileName, string[] Lyrics)
        : IntegragtionEvent;
}
