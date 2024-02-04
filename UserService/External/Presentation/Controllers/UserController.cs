using Application.Dtos;
using Application.Users.Commands;
using Application.Users.Queries;
using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.RequestFeatures;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Presentation.ActionFilters;
using System.Text.Json;
namespace Presentation.Controllers
{
    public sealed class UserController : ApiController
    {
        private readonly IAuthService _AuthService;
        public UserController(ISender sender, IAuthService authService) : base(sender)
        {
            _AuthService = authService;
        }

        [HttpGet("{userId:guid}"), Authorize(Roles = "User,Admin")]
        [ResponseCache(CacheProfileName = "Default")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken)
        {
            GetUserByIdQuery query = new GetUserByIdQuery(userId);

            UserDto user = await _Sender.Send(query, cancellationToken);

            return Ok(user);
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
        [HttpHead, Authorize(Roles = "User,Admin")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsers([FromQuery] UserParameters requestParameters, CancellationToken cancellationToken)
        {
            GetUsersQuery query = new GetUsersQuery(requestParameters);

            var result = await _Sender.Send(query, cancellationToken);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

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
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, DELETE");

            return Ok();
        }

        [HttpGet("role"), Authorize(Roles = "User,Admin")]
        [EndpointName("GetUserRole")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<IActionResult> GetUserRole(CancellationToken cancellationToken)
        {
            string? jwtToken = Request.Cookies["jwtToken"];

            GetRoleFromJwtQuery query = new GetRoleFromJwtQuery(jwtToken);

            string role = await _Sender.Send(query, cancellationToken);

            return Ok(role);
        }

        [HttpPost("logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Logout()
        {
            _AuthService.ClearTokens();

            return Ok();
        }

        [HttpPut("hide/{userId:guid}"), Authorize(Roles = "Admin")]
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
            string refreshToken = Request.Cookies["refreshToken"];

            UpdateRefreshTokenCommand command = new UpdateRefreshTokenCommand(refreshToken);

            await _Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPatch("{userId:guid}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PatchUser(Guid userId,
            [FromBody] JsonPatchDocument<UserPatchDto> patchDoc,
            CancellationToken cancellationToken)
        {
            string? jwtToken = Request.Cookies["jwtToken"];

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
