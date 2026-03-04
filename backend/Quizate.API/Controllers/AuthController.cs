using Microsoft.AspNetCore.Mvc;
using Quizate.API.Contracts;
using Quizate.API.Services.Auth;

namespace Quizate.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController(
    IAuthService authService,
    ICookieManager cookieManager,
    IConfiguration configuration) : ControllerBase
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

        cookieManager.SetRefreshTokenCookie(
            result.Data!.RefreshToken,
            configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"),
            Response);

        cookieManager.SetAccessTokenCookie(
            result.Data.AccessToken,
            configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes"),
            Response);

        return Ok();
    }

    [HttpPost("refreshToken")]
    public async Task<ActionResult> RefreshToken()
    {
        Request.Cookies.TryGetValue("REFRESH_TOKEN", out var refreshToken);

        var result = await authService.RefreshTokenAsync(refreshToken);

        if (!result.Success)
            return BadRequest(result.Errors);

        cookieManager.SetRefreshTokenCookie(
            result.Data!.RefreshToken,
            configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"),
            Response);

        cookieManager.SetAccessTokenCookie(
            result.Data.AccessToken,
            configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes"),
            Response);

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
