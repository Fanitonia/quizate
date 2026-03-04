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
        var result = await authService.RegisterAsync(request);

        if (!result.Success)
            return BadRequest(result.Errors);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await authService.LoginAsync(request);

        if (!result.Success)
            return BadRequest(result.Errors);

        Response.Cookies.Append("REFRESH_TOKEN", result.Data!.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays")),
            Path = "/auth/refreshToken"
        });

        Response.Cookies.Append("ACCESS_TOKEN", result.Data!.AccessToken, new CookieOptions
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

        var result = await authService.RefreshTokenAsync(refreshToken);

        if (!result.Success)
            return BadRequest(result.Errors);

        Response.Cookies.Append("REFRESH_TOKEN", result.Data!.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays")),
            Path = "/auth/refreshToken"
        });

        Response.Cookies.Append("ACCESS_TOKEN", result.Data!.AccessToken, new CookieOptions
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
        var result = await authService.RevokeRefreshTokensAsync(userId);

        if (!result.Success)
            return NotFound(result.Errors);

        return NoContent();
    }
}
