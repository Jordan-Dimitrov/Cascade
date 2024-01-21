using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Users.Commands;
using Application.Users.Queries;
using Domain.Aggregates.UserAggregate;
using MediatR;
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
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken)
        {
            GetUserByIdQuery query = new GetUserByIdQuery(userId);

            UserDto user = await _Sender.Send(query, cancellationToken);

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserDto user, CancellationToken cancellationToken)
        {
            var command = new CreateUserCommand(user.Username, user.Password, UserRole.User);

            await _Sender.Send(command, cancellationToken);

            return Ok();
        }
    }
}
