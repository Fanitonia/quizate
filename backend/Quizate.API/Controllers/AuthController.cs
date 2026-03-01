using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Contracts;
using Quizate.API.Data;
using Quizate.API.Services;
using Quizate.Data.Models;

namespace Quizate.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var user = await _authService.RegisterAsync(request);

            if (user == null)
                return BadRequest("Username or email already exists.");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserRequest request)
        {
            var token = await _authService.LoginAsync(request);

            if (token == null)
                return BadRequest("Invalid username/email or password.");

            return Ok(token);
        }

    }
}
