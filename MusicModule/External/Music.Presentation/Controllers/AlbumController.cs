using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music.Application.Albums.Commands;
using Music.Application.Dtos;
using Presentation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Presentation.Controllers
{
    public sealed class AlbumController : ApiController
    {
        public AlbumController(ISender sender) : base(sender)
        {

        }

        [HttpPost, Authorize(Roles = "Artist")]
        [ResponseCache(CacheProfileName = "Default")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAlbum([FromForm]CreateAlbumDto createAlbumDto,
            IFormFile formFile, 
            CancellationToken cancellationToken)
        {
            string? jwtToken = Request.Cookies["jwtToken"];

            byte[] file = await Utils.ConvertToByteArrayAsync(formFile);

            CreateAlbumCommand command = new CreateAlbumCommand(createAlbumDto, jwtToken,
                file, formFile.FileName);

            Guid id = await _Sender.Send(command, cancellationToken);

            return Ok(id);
        }
    }
}
