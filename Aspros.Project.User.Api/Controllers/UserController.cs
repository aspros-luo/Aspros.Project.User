using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspros.Project.User.Application.Dto;
using Aspros.Project.User.Application.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aspros.Project.User.Api.Controllers
{
    [Route(Program.ServiceName)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("user.create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel data)
        {
            var result = await _userService.CreateUser(data);
            return Ok(new {data = "the test api", is_successd = result});
        }

        [Route("user.get")]
        [HttpGet]
        public async Task<IActionResult> ValidUser(long id)
        {
            var user = await _userService.GetUser(id);
            return Ok(new {data = user, is_successd = true});
        }

        [Route("inside.valid.user")]
        [HttpGet]
        public IActionResult InsideApi()
        {
            return Ok(new {data = "the inside api", is_successd = true});
        }
    }
}