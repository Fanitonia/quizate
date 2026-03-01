using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Contracts;
using Quizate.API.Contracts.User;
using Quizate.API.Data;
using Quizate.API.Services;
using Quizate.Data.Models;

namespace Quizate.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        //TODO: password resetleme, email doğrulama, account silme, account güncelleme, refresh tokenleri temizleme...

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = await authService.RegisterAsync(request);

            if (user == null)
                return BadRequest("Username or email already exists.");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var response = await authService.LoginAsync(request);

            if (response == null)
                return BadRequest("Invalid username/email or password.");

            return Ok(response);
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var response = await authService.RefreshTokenAsync(request);

            if (response == null)
                return BadRequest("Invalid refresh token.");

            return Ok(response);
        }
    }
}
