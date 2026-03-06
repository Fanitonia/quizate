using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Contracts;
using Quizate.API.Extensions.Validation;
using Quizate.API.Services.Auth;
using Quizate.Data.Enums;
using System.Security.Claims;

namespace Quizate.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController(
    IUserManager userManager,
    ICookieManager cookieManager,
    ITokenManager tokenManager,
    IConfiguration configuration,
    IValidator<LoginRequest> loginRequestValidator,
    IValidator<RegisterRequest> registerRequestValidator) : ControllerBase
{
    //TODO: password resetleme, email doğrulama, account silme, account güncelleme...

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var validation = registerRequestValidator.Validate(request);

        if (!validation.IsValid)
        {
            validation.AddToModelState(ModelState);
            return ValidationProblem();
        }

        var result = await userManager.RegisterAsync(request);

        if (!result.Success)
        {
            result.AddErrorsToModelState(ModelState, "registerErrors");
            return ValidationProblem();
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var validation = loginRequestValidator.Validate(request);

        if (!validation.IsValid)
        {
            validation.AddToModelState(ModelState);
            return ValidationProblem();
        }

        var result = await userManager.LoginAsync(request);

        if (!result.Success)
        {
            result.AddErrorsToModelState(ModelState, "loginErrors");
            return ValidationProblem();
        }

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

        var result = await tokenManager.RefreshTokenAsync(refreshToken);

        if (!result.Success)
        {
            result.AddErrorsToModelState(ModelState, "tokenErrors");
            return ValidationProblem();
        }

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

        var result = await tokenManager.RevokeRefreshTokensAsync(userId);

        if (!result.Success)
        {
            result.AddErrorsToModelState(ModelState, "tokenErrors");
            return ValidationProblem();
        }

        if (isSelf)
            cookieManager.RemoveRefreshTokenCookie(Response);

        return NoContent();
    }
}
