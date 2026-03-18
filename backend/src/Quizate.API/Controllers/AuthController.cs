using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Extensions;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Features.Auth.Interfaces;
using Quizate.Domain.Enums;

namespace Quizate.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController(
    IAuthService authService,
    IConfiguration configuration) : ControllerBase
{
    // onur
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await authService.RegisterAsync(request);

        if (result.IsFailure)
            return BadRequest(result.Error);

        Response.SetCookie(Cookies.AccessToken, result.Value!.AccessToken, configuration);
        Response.SetCookie(Cookies.RefreshToken, result.Value!.RefreshToken, configuration);

        return Created();
    }

    // onur
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await authService.LoginAsync(request);

        if (result.IsFailure)
            return BadRequest(result.Error);

        Response.SetCookie(Cookies.AccessToken, result.Value!.AccessToken, configuration);
        Response.SetCookie(Cookies.RefreshToken, result.Value!.RefreshToken, configuration);

        return Ok();
    }

    // onur
    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        Request.Cookies.TryGetValue("REFRESH_TOKEN", out var refreshToken);

        if (refreshToken == null)
            return Unauthorized();

        var result = await authService.RevokeRefreshTokenAsync(refreshToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        Response.DeleteCookie(Cookies.AccessToken);
        Response.DeleteCookie(Cookies.RefreshToken);

        return NoContent();
    }


    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken()
    {
        Request.Cookies.TryGetValue("REFRESH_TOKEN", out var refreshToken);

        if (refreshToken == null)
            return Unauthorized();

        var result = await authService.RefreshAccessTokenAsync(refreshToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        Response.SetCookie(Cookies.AccessToken, result.Value!.AccessToken, configuration);
        Response.SetCookie(Cookies.RefreshToken, result.Value!.RefreshToken, configuration);

        return Ok();
    }

    [Authorize]
    [HttpDelete("refresh-token/{userId:guid}")]
    public async Task<ActionResult> RevokeRefreshTokens([FromRoute] Guid userId)
    {
        if (!User.TryGetUserId(out Guid currentUserId))
            return Unauthorized();

        bool isAdmin = User.IsInRole(UserRole.Admin.ToString());
        bool isSelf = currentUserId == userId;

        if (!isAdmin && !isSelf)
            return Forbid();

        var result = await authService.RevokeAllRefreshTokensAsync(userId);

        if (result.IsFailure)
            return BadRequest(result.Error);

        if (isSelf)
            Response.DeleteCookie(Cookies.RefreshToken);

        return NoContent();
    }
}
