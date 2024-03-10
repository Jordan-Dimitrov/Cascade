using MediatR;

namespace Streaming.Application.Songs
{
    public sealed record GetSongQuery(string FileName) : IRequest<(MemoryStream, string)>;
}
