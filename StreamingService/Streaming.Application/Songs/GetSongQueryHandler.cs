using MediatR;
using Streaming.Application.Abstractions;

namespace Streaming.Application.Songs
{
    internal sealed class GetSongQueryHandler : IRequestHandler<GetSongQuery, (MemoryStream, string)>
    {
        private readonly IFileProcessingService _FileProcessingService;
        public GetSongQueryHandler(IFileProcessingService fileProcessingService)
        {
            _FileProcessingService = fileProcessingService;
        }
        public async Task<(MemoryStream, string)> Handle(GetSongQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _FileProcessingService.GetSong(request.FileName);

            return result;
        }
    }
}
