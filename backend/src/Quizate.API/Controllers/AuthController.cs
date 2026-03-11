using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Extensions;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Features.Auth.Interfaces;
using Quizate.Domain.Enums;
using System.Security.Claims;

namespace Quizate.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController(
    IAuthService authService,
    ICookieService cookieService,
    IConfiguration configuration) : ControllerBase
{
    //TODO: password resetleme, email doğrulama, account silme, account güncelleme...

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await authService.RegisterAsync(request);

        if (result.IsFailure)
        {
            result.AddErrorsToModelState(ModelState, "registerErrors");
            return ValidationProblem();
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await authService.LoginAsync(request);

        if (result.IsFailure)
        {
            result.AddErrorsToModelState(ModelState, "loginErrors");
            return ValidationProblem();
        }

        cookieService.SetRefreshTokenCookie(
            result.Value!.RefreshToken,
            configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"),
            Response);

        cookieService.SetAccessTokenCookie(
            result.Value.AccessToken,
            configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes"),
            Response);

        return Ok();
    }

    [HttpPost("refreshToken")]
    public async Task<ActionResult> RefreshToken()
    {
        Request.Cookies.TryGetValue("REFRESH_TOKEN", out var refreshToken);

        var result = await authService.RefreshAccessTokenAsync(refreshToken);

        if (result.IsFailure)
        {
            result.AddErrorsToModelState(ModelState, "tokenErrors");
            return ValidationProblem();
        }

        cookieService.SetRefreshTokenCookie(
            result.Value!.RefreshToken,
            configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"),
            Response);

        cookieService.SetAccessTokenCookie(
            result.Value.AccessToken,
            configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes"),
            Response);

        return Ok();
    }

    [Authorize]
    [HttpDelete("refreshToken/{userId:guid}")]
    public async Task<ActionResult> RevokeRefreshTokens([FromRoute] Guid userId)
    {
        string? currentUserIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(currentUserIdClaim, out Guid currentUserId))
            return BadRequest();

        bool isAdmin = User.IsInRole(UserRole.Admin.ToString());
        bool isSelf = currentUserId == userId;

        if (!isAdmin && !isSelf)
            return Forbid();

        var result = await authService.RevokeRefreshTokensAsync(userId);

        if (result.IsFailure)
        {
            result.AddErrorsToModelState(ModelState, "tokenErrors");
            return ValidationProblem();
        }

        if (isSelf)
            cookieService.RemoveRefreshTokenCookie(Response);

        return NoContent();
    }
}
