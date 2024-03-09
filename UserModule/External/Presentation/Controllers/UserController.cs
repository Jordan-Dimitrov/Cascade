using Application.Shared;
using Application.Shared.Abstractions;
using Application.Shared.Constants;
using Domain.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Presentation.Shared;
using Presentation.Shared.ActionFilters;
using Presentation.Shared.Constants;
using System.Reflection.Metadata;
using System.Text.Json;
using Users.Application.Dtos;
using Users.Application.Users.Commands;
using Users.Application.Users.Queries;
using Users.Domain.RequestFeatures;
namespace Presentation.Controllers
{
    public sealed class UserController : ApiController
    {
        private readonly IUserInfoService _UserInfoService;
        public UserController(ISender sender, IUserInfoService userInfoService) : base(sender)
        {
            _UserInfoService = userInfoService;
        }

        //, Authorize(Roles = AllowedRoles.All)
        [HttpGet("{userId:guid}")]
        //[ResponseCache(CacheProfileName = CacheProfiles.Default)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken)
        {
            GetUserByIdQuery query = new GetUserByIdQuery(userId);

            UserDto user = await _Sender.Send(query, cancellationToken);

            return Ok(user);
        }

        [HttpGet, Authorize(Roles = AllowedRoles.All)]
        [HttpHead, Authorize(Roles = AllowedRoles.All)]
        [ResponseCache(CacheProfileName = CacheProfiles.Default)]
        [ProducesResponseType(typeof(ICollection<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers([FromQuery] UserParameters requestParameters, CancellationToken cancellationToken)
        {
            GetUsersQuery query = new GetUsersQuery(requestParameters);

            var result = await _Sender.Send(query, cancellationToken);

            Response.Headers.Add(CustomHeaders.PaginationHeader,
                JsonSerializer.Serialize(result.metaData));

            return Ok(result.users);
        }

        [HttpPost("register")]
        [ValidateModelState]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody]CreateUserDto user, CancellationToken cancellationToken)
        {
            CreateUserCommand command = new CreateUserCommand(user.Username, user.Password, UserRole.User);

            Guid id = await _Sender.Send(command, cancellationToken);

            return Ok(id);
        }

        [HttpPost("register-artist"), Authorize(Roles = AllowedRoles.Admin)]
        [ValidateModelState]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterArtist([FromBody] CreateUserDto user, CancellationToken cancellationToken)
        {
            CreateUserCommand command = new CreateUserCommand(user.Username, user.Password, UserRole.Artist);

            Guid id = await _Sender.Send(command, cancellationToken);

            return Ok(id);
        }

        [HttpPost("login")]
        [ValidateModelState]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] CreateUserDto user, CancellationToken cancellationToken)
        {
            LoginUserCommand command = new LoginUserCommand(user.Username, user.Password);

            await _Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsersOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PATCH, PUT, DELETE");

            return Ok();
        }

        [HttpGet("role"), Authorize(Roles = AllowedRoles.All)]
        [EndpointName("GetUserRole")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ResponseCache(CacheProfileName = CacheProfiles.Default)]
        public async Task<IActionResult> GetUserRole(CancellationToken cancellationToken)
        {
            string? jwtToken = Request.Cookies[Tokens.JwtToken];

            string role = await Task.Run(() => _UserInfoService.GetRoleFromJwtToken(jwtToken));

            return Ok(role);
        }

        [HttpPost("logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            string? jwtToken = Request.Cookies[Tokens.JwtToken];

            LogoutUserCommand command = new LogoutUserCommand(jwtToken);

            await _Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPut("hide/{userId:guid}"), Authorize(Roles = AllowedRoles.Admin)]
        [EndpointName("HideUser")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> HideUser(Guid userId, CancellationToken cancellationToken)
        {
            HideUserCommand command = new HideUserCommand(userId);

            await _Sender.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRefreshToken(CancellationToken cancellationToken)
        {
            string? refreshToken = Request.Cookies[Tokens.RefreshToken];

            UpdateRefreshTokenCommand command = new UpdateRefreshTokenCommand(refreshToken);

            await _Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPatch("{userId:guid}"), Authorize(Roles = AllowedRoles.Admin)]
        [EndpointName("PatchUser")]
        [ValidateModelState]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PatchUser(Guid userId,
            [FromBody] JsonPatchDocument<UserPatchDto> patchDoc,
            CancellationToken cancellationToken)
        {
            string? jwtToken = Request.Cookies[Tokens.JwtToken];

            PatchUserQuery query = new PatchUserQuery(userId);
            var result = await _Sender.Send(query, cancellationToken);

            patchDoc.ApplyTo(result.UserToPatch);

            SaveChangesForPatchCommand command = new SaveChangesForPatchCommand(result.UserToPatch,
                result.User);

            await _Sender.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
