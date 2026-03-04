using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Contracts;
using Quizate.API.Services.Auth;
using Quizate.API.Validators;

namespace Quizate.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController(
    IUserManager userManager,
    ICookieManager cookieManager,
    ITokenManager tokenManager,
    IConfiguration configuration,
    IValidator<LoginRequest> loginValidator,
    IValidator<RegisterRequest> registerValidator) : ControllerBase
{
    //TODO: password resetleme, email doğrulama, account silme, account güncelleme...

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var validation = registerValidator.Validate(request);

        if (!validation.IsValid)
        {
            validation.AddToModelState(ModelState);
            return ValidationProblem();
        }

        var result = await userManager.RegisterAsync(request);
        result.AddErrorsToModelState(ModelState, "registerErrors");

        if (!result.Success)
            return ValidationProblem();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var validation = loginValidator.Validate(request);

        if (!validation.IsValid)
        {
            validation.AddToModelState(ModelState);
            return ValidationProblem();
        }

        var result = await userManager.LoginAsync(request);
        result.AddErrorsToModelState(ModelState, "loginErrors");

        if (!result.Success)
            return ValidationProblem();

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
        result.AddErrorsToModelState(ModelState, "tokenErrors");

        if (!result.Success)
            return ValidationProblem();

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
        var result = await tokenManager.RevokeRefreshTokensAsync(userId);
        result.AddErrorsToModelState(ModelState, "tokenErrors");

        if (!result.Success)
            return ValidationProblem();

        return NoContent();
    }
}
