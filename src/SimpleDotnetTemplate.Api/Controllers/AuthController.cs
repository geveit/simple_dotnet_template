using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleDotnetTemplate.Core.Users.Dto;
using SimpleDotnetTemplate.Core.Users.Exceptions;
using SimpleDotnetTemplate.Core.Users.Interfaces;

namespace SimpleDotnetTemplate.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> AuthAsync(AuthInput input)
        {
            try
            {
                var output = await _authService.Authenticate(input);
                return Ok(output);
            }
            catch (AuthFailedException)
            {
                return Unauthorized();
            }
        }
    }
}