using Application.Shared.Abstractions;
using Application.Shared.Constants;
using Domain.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Music.Application.Albums.Commands;
using Music.Application.Albums.Queries;
using Music.Application.Dtos;
using Music.Domain.RequestFeatures;
using Presentation.Shared;
using Presentation.Shared.ActionFilters;
using Presentation.Shared.Constants;
using System.Text.Json;

namespace Music.Presentation.Controllers
{
    public sealed class AlbumController : ApiController
    {
        private readonly IFileConversionService _FileConversionService;
        public AlbumController(ISender sender, IFileConversionService fileConversionService) : base(sender)
        {
            _FileConversionService = fileConversionService;
        }

        [HttpPost, Authorize(Roles = AllowedRoles.Artist)]
        [ValidateModelState]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAlbum([FromForm] CreateAlbumDto createAlbumDto,
            IFormFile formFile,
            CancellationToken cancellationToken)
        {
            string? jwtToken = Request.Cookies[Tokens.JwtToken];

            byte[] file = await _FileConversionService.ConvertToByteArrayAsync(formFile);

            CreateAlbumCommand command = new CreateAlbumCommand(createAlbumDto, jwtToken,
                file, formFile.FileName, createAlbumDto.Lyrics);

            Guid id = await _Sender.Send(command, cancellationToken);

            return Ok(id);
        }

        [HttpPut("add/{albumId:guid}"), Authorize(Roles = AllowedRoles.Artist)]
        [ValidateModelState]
        [EndpointName("AddSong")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSongToAlbum(Guid albumId,
            [FromForm] CreateSongDto createSongDto,
            IFormFile formFile,
            CancellationToken cancellationToken)
        {
            string? jwtToken = Request.Cookies[Tokens.JwtToken];

            byte[] file = await _FileConversionService.ConvertToByteArrayAsync(formFile);

            AddSongToAlbumCommand command = new AddSongToAlbumCommand(createSongDto, jwtToken,
                file, formFile.FileName, albumId);

            await _Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPut("remove/{albumId:guid}"), Authorize(Roles = AllowedRoles.Artist)]
        [EndpointName("RemoveSong")]
        [ValidateModelState]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveSongFromAlbum(Guid albumId,
            [FromQuery] Guid songId,
            CancellationToken cancellationToken)
        {
            string? jwtToken = Request.Cookies[Tokens.JwtToken];

            RemoveSongFromAlbumCommand command = new RemoveSongFromAlbumCommand(albumId, songId, jwtToken);

            await _Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAlbumsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, DELETE");

            return Ok();
        }

        [HttpGet("{albumId:guid}"), Authorize(Roles = AllowedRoles.All)]
        [ResponseCache(CacheProfileName = CacheProfiles.Default)]
        [ProducesResponseType(typeof(AlbumWithSongsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid albumId, CancellationToken cancellationToken)
        {
            GetAlbumQuery query = new GetAlbumQuery(albumId);

            AlbumWithSongsDto album = await _Sender.Send(query, cancellationToken);

            return Ok(album);
        }

        [HttpGet, Authorize(Roles = AllowedRoles.All)]
        [HttpHead, Authorize(Roles = AllowedRoles.All)]
        [ProducesResponseType(typeof(ICollection<AlbumDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAlbums([FromQuery] AlbumParameters requestParameters,
            CancellationToken cancellationToken)
        {
            GetAlbumsQuery query = new GetAlbumsQuery(requestParameters);

            var result = await _Sender.Send(query, cancellationToken);

            Response.Headers.Add(CustomHeaders.PaginationHeader,
                JsonSerializer.Serialize(result.metaData));

            return Ok(result.albums);
        }

        [HttpPatch("{albumId:guid}"), Authorize(Roles = AllowedRoles.Artist)]
        [EndpointName("PatchAlbum")]
        [ValidateModelState]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PatchAlbum(Guid albumId,
            [FromBody] JsonPatchDocument<AlbumPatchDto> patchDoc,
            CancellationToken cancellationToken)
        {
            string? jwtToken = Request.Cookies[Tokens.JwtToken];

            PatchAlbumQuery query = new PatchAlbumQuery(albumId, jwtToken);
            var result = await _Sender.Send(query, cancellationToken);

            patchDoc.ApplyTo(result.AlbumToPatch);

            SaveChangesForPatchCommand command = new SaveChangesForPatchCommand(result.AlbumToPatch,
                result.Album);

            await _Sender.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
