using Domain.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Shared;
using Presentation.Shared.Constants;
using Streaming.Application.Abstractions;
using Streaming.Application.Songs;

namespace Streaming.Presentation.Controllers
{
    public sealed class SongController : ApiController
    {
        private readonly IBackgroundQueue _BackgroundQueue;
        public SongController(ISender sender, IBackgroundQueue backgroundQueue) : base(sender)
        {
            _BackgroundQueue = backgroundQueue;
        }

        [HttpGet("{fileName}"), Authorize(Roles = AllowedRoles.All)]
        [ResponseCache(CacheProfileName = CacheProfiles.Default)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetSong(string fileName)
        {
            GetSongQuery query = new GetSongQuery(fileName);

            var result = await _Sender.Send(query);

            FileStreamResult fileStreamResult = new FileStreamResult(result.Item1, result.Item2);

            return fileStreamResult;
        }
    }
}
