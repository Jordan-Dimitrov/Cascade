using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Users.Commands;
using Application.Users.Queries;
using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.RequestFeatures;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Presentation.Controllers
{
    public sealed class UserController : ApiController
    {
        public UserController(ISender sender) : base(sender)
        {
        }

        [HttpGet("{userId:guid}")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody]CreateUserDto user, CancellationToken cancellationToken)
        {
            CreateUserCommand command = new CreateUserCommand(user.Username, user.Password, UserRole.User);

            Guid id = await _Sender.Send(command, cancellationToken);

            return Ok(id);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] CreateUserDto user, CancellationToken cancellationToken)
        {
            LoginUserCommand command = new LoginUserCommand(user.Username, user.Password);

            await _Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpOptions]
        public IActionResult GetUsersOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, DELETE");
            return Ok();
        }

        [HttpGet("role"), Authorize(Roles = "User,Admin")]
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
    }
}
