using Domain.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Shared;
using Presentation.Shared.Constants;
using Streaming.Application.Abstractions;
using Streaming.Application.Songs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Streaming.Presentation.Controllers
{
    public sealed class SongController : ApiController
    {
        public SongController(ISender sender) : base(sender)
        {
        }

        [HttpGet("{fileName}"), Authorize(Roles = AllowedRoles.All)]
        [ResponseCache(CacheProfileName = CacheProfiles.Default)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetThumbnail(string fileName)
        {
            GetSongQuery query = new GetSongQuery(fileName);

            var result = await _Sender.Send(query);

            FileStreamResult fileStreamResult = new FileStreamResult(result.Item1, result.Item2);

            return fileStreamResult;
        }
    }
}
