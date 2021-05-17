using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspros.Project.User.Application.Commands;
using Aspros.Project.User.Application.Dto;
using Aspros.Project.User.Application.Queries;
using Aspros.Project.User.Application.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aspros.Project.User.Api.Controllers
{
    [Route(Program.ServiceName)]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [Route("user.create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            // var result = await _userService.CreateUser(data);
            return result != 0 ? Ok(new { data =result, is_successd = true }) : Ok(new { is_successd = false });
        }

        [Route("user.get")]
        [HttpGet]
        public async Task<IActionResult> ValidUser(long id)
        {
            var result = await _mediator.Send(new UserGetQuery() {UserId = id});
            return Ok(new {data = result, is_successd = true});

            // var user = await _userService.GetUser(id);
            // return Ok(new {data = user, is_successd = true});
        }

        [Route("inside.valid.user")]
        [HttpGet]
        public IActionResult InsideApi()
        {
            return Ok(new {data = "the inside api", is_successd = true});
        }
    }
}