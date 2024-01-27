﻿using System;
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

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody]CreateUserDto user, CancellationToken cancellationToken)
        {
            var command = new CreateUserCommand(user.Username, user.Password, UserRole.User);

            Guid id = await _Sender.Send(command, cancellationToken);

            return Ok(id);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] CreateUserDto user, CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(user.Username, user.Password);

            await _Sender.Send(command, cancellationToken);

            return Ok();
        }
    }
}
