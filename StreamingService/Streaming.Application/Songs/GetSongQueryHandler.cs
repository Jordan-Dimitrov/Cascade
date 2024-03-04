using MediatR;
using Streaming.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaming.Application.Songs
{
    internal sealed class GetSongQueryHandler : IRequestHandler<GetSongQuery, (FileStream, string)>
    {
        private readonly IFileProcessingService _FileProcessingService;
        public GetSongQueryHandler(IFileProcessingService fileProcessingService)
        {
            _FileProcessingService = fileProcessingService;
        }
        public async Task<(FileStream, string)> Handle(GetSongQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _FileProcessingService.GetSong(request.FileName);

            return result;
        }
    }
}
