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
    ICookieService cookieService,
    IConfiguration configuration) : ControllerBase
{
    // onur
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await authService.RegisterAsync(request);

        if (result.IsFailure)
        {
            result.AddErrorsToModelState(ModelState, "registerErrors");
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

        return Created();
    }

    // onur
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
            return Unauthorized();

        cookieService.DeleteRefreshTokenCookie(Response);
        cookieService.DeleteAccessTokenCookie(Response);

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
        {
            result.AddErrorsToModelState(ModelState, "tokenErrors");
            return ValidationProblem();
        }

        if (isSelf)
            cookieService.DeleteRefreshTokenCookie(Response);

        return NoContent();
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePassword([FromBody] PasswordChangeRequest request, CancellationToken ct)
    {
        if (!User.TryGetUserId(out Guid userId))
            return Unauthorized();

        var result = await authService.ChangePasswordAsync(request, userId, ct);

        if (result.IsFailure)
        {
            result.AddErrorsToModelState(ModelState, "changePasswordErrors");
            return ValidationProblem();
        }

        return NoContent();
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword()
    {
        throw new NotImplementedException();
    }
}
