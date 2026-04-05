using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Extensions;
using Quizate.Application.Common.Errors;
using Quizate.Application.Common.Pagination;
using Quizate.Application.Common.Serializer;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Application.Features.Users.DTOs.Requests;
using Quizate.Application.Features.Users.DTOs.Responses;
using Quizate.Application.Features.Users.Interfaces;
using Quizate.Domain.Enums;

namespace Quizate.API.Controllers;

[Route("users")]
[ApiController]
public class UserController(
    IUserCommandService userCommand,
    IUserQueryService userQuery,
    IQuizQueryService quizService) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<ICollection<DetailedUserInfoResponse>>> GetAllUsers(
        [FromQuery] PaginationParameters pagination,
        CancellationToken ct)
    {
        var result = await userQuery.GetAllUsersAsync(pagination, ct);

        Response.Headers.Append(Headers.XPagination, result.PaginationMetadata.SerializeWithCamelCasing());

        return Ok(result.Records);
    }

    // onur
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserInfoResponse>> GetUser(
        Guid userId, CancellationToken ct)
    {
        var user = await userQuery.GetUserAsync(userId, ct);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("{userId:guid}/quizzes")]
    public async Task<ActionResult<ICollection<QuizResponse>>> GetUserQuizzes(
        [FromRoute] Guid userId,
        [FromQuery] PaginationParameters pagination,
        CancellationToken ct)
    {
        var result = await quizService.GetAllQuizzesAsync(pagination, ct, userId);

        Response.Headers.Append(Headers.XPagination, result.PaginationMetadata.SerializeWithCamelCasing());

        return Ok(result.Records);
    }

    // onur
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult> DeleteUser(Guid userId)
    {
        var result = await userCommand.DeleteUserAsync(userId);

        if (result.IsFailure)
        {
            if (result.Error == CommonErrors.NotFound)
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return NoContent();
    }

    // huseyin
    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<DetailedUserInfoResponse>> GetMe(CancellationToken ct)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        var user = await userQuery.GetDetailedUserAsync(userId, ct);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // huseyin
    [Authorize]
    [HttpPatch("me")]
    public async Task<ActionResult> UpdateMe([FromBody] UpdateUserRequest request)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        var result = await userCommand.UpdateUserInfoAsync(request, userId);

        if (result.IsFailure)
            return BadRequest();

        return NoContent();
    }

    [Authorize]
    [HttpPatch("me/profile-picture")]
    public async Task<ActionResult> UpdateUserProfilePicture()
    {
        throw new NotImplementedException();
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{userId:guid}/role")]
    public async Task<ActionResult> UpdateUserRole(Guid userId, [FromBody] UpdateUserRoleRequest request)
    {
        var result = await userCommand.UpdateUserRoleAsync(request, userId);

        if (result.IsFailure)
        {
            if (result.Error == CommonErrors.NotFound)
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return Ok();
    }

    // huseyin
    [Authorize]
    [HttpDelete("me")]
    public async Task<ActionResult> DeleteMe()
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        var result = await userCommand.DeleteUserAsync(userId);

        if (result.IsFailure)
            return BadRequest();

        return NoContent();
    }

    // onur
    [Authorize]
    [HttpGet("me/quizzes")]
    public async Task<ActionResult<List<QuizResponse>>> GetMyQuizzes(
        [FromQuery] PaginationParameters pagination, CancellationToken ct)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        var result = await quizService.GetAllQuizzesAsync(pagination, ct, userId);

        Response.Headers.Append("X-Pagination", result.PaginationMetadata.SerializeWithCamelCasing());

        return Ok(result.Records);

    }

    [Authorize]
    [HttpPost("me/change-password")]
    public async Task<ActionResult> ChangePassword([FromBody] PasswordChangeRequest request)
    {
        if (!User.TryGetUserId(out Guid userId))
            return Unauthorized();

        var result = await userCommand.ChangePasswordAsync(request, userId);

        if (result.IsFailure)
            return BadRequest(result.Error);


        return NoContent();
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword()
    {
        throw new NotImplementedException();
    }
}
