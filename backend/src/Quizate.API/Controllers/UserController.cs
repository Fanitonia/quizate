using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Extensions;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Users.DTOs.Requests;
using Quizate.Application.Features.Users.DTOs.Responses;
using Quizate.Application.Features.Users.Interfaces;

namespace Quizate.API.Controllers;

[Route("users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ICollection<DetailedUserInfoResponse>>> GetAllUsers(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    // onur
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserInfoResponse>> GetUser(Guid userId, CancellationToken ct)
    {
        var result = await userService.GetUserAsync(userId, ct);

        if (result.IsFailure)
            return NotFound();

        return Ok(result.Value);
    }

    [HttpGet("{userId:guid}/quizzes")]
    public async Task<ActionResult<ICollection<QuizResponse>>> GetUserQuizzes(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    // onur
    [Authorize]
    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult> DeleteUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPatch("{userId:guid}/role")]
    public async Task<ActionResult> UpdateUserRole(Guid userId)
    {
        throw new NotImplementedException();
    }

    // huseyin
    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<DetailedUserInfoResponse>> GetMe(CancellationToken ct)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        var result = await userService.GetDetailedUserAsync(userId, ct);

        if (result.IsFailure)
            return NotFound();

        return Ok(result.Value);
    }

    // huseyin
    [Authorize]
    [HttpPatch("me")]
    public async Task<ActionResult> UpdateMe([FromBody] UpdateUserRequest request)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        var result = await userService.UpdateUserAsync(userId, request);

        if (result.IsFailure)
            return NotFound();

        return Ok();
    }

    [Authorize]
    [HttpPatch("me/profile-picture")]
    public async Task<ActionResult> UpdateUserProfilePicture()
    {
        throw new NotImplementedException();
    }

    // huseyin
    [Authorize]
    [HttpDelete("me")]
    public async Task<ActionResult> DeleteMe()
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        var result = await userService.DeleteUserAsync(userId);

        if (result.IsFailure)
            return NotFound();

        return Ok();
    }

    // onur
    [Authorize]
    [HttpGet("me/quizzes")]
    public async Task<ActionResult<List<QuizResponse>>> GetMyQuizzes()
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("me/change-password")]
    public async Task<ActionResult> ChangePassword([FromBody] PasswordChangeRequest request)
    {
        if (!User.TryGetUserId(out Guid userId))
            return Unauthorized();

        var result = await userService.ChangePasswordAsync(request, userId);

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
