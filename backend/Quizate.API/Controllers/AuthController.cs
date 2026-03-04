using Microsoft.AspNetCore.Mvc;
using Quizate.API.Contracts;
using Quizate.API.Services.Auth;

namespace Quizate.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController(IAuthService authService, IConfiguration configuration) : ControllerBase
{
    //TODO: password resetleme, email doğrulama, account silme, account güncelleme...

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = await authService.RegisterAsync(request);

        if (!user)
            return BadRequest("Could not register user.");

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await authService.LoginAsync(request);

        if (response == null)
            return BadRequest("Invalid username/email or password.");

        Response.Cookies.Append("REFRESH_TOKEN", response.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays")),
            Path = "/auth/refreshToken"
        });

        Response.Cookies.Append("ACCESS_TOKEN", response.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes"))
        });

        return Ok();
    }

    [HttpPost("refreshToken")]
    public async Task<ActionResult> RefreshToken()
    {
        Request.Cookies.TryGetValue("REFRESH_TOKEN", out var refreshToken);

        var response = await authService.RefreshTokenAsync(refreshToken);

        if (response == null)
            return BadRequest("Invalid refresh token.");

        Response.Cookies.Append("REFRESH_TOKEN", response.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays")),
            Path = "/auth/refreshToken"
        });

        Response.Cookies.Append("ACCESS_TOKEN", response.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes"))
        });

        return Ok();
    }

    [HttpDelete("refreshToken/{userId}")]
    public async Task<ActionResult> RevokeRefreshTokens(Guid userId)
    {
        var success = await authService.RevokeRefreshTokensAsync(userId);

        if (!success)
            return NotFound("Could not found refresh tokens for the specified user.");

        return NoContent();
    }
}
