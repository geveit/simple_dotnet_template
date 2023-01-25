using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleDotnetTemplate.Core.Users.Dto;
using SimpleDotnetTemplate.Core.Users.Interfaces;

namespace SimpleDotnetTemplate.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var output = await _userService.ListUsersAsync();
            return Ok(output);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var output = await _userService.GetUserAsync(id);
            return Ok(output);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserInput input)
        {
            var output = await _userService.CreateUserAsync(input);
            return Created($"/users/{output.Id}", output);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}