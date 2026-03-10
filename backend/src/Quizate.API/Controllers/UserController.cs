using Microsoft.AspNetCore.Mvc;
using Quizate.API.Extensions;
using Quizate.Application.Quizzes.DTOs.Responses;
using Quizate.Application.Users.DTOs.Requests;
using Quizate.Application.Users.DTOs.Responses;
using Quizate.Application.Users.Interfaces;
namespace Quizate.API.Controllers;

[Route("users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserInfoResponse>> GetUser(Guid userId, CancellationToken ct)
    {
        var result = await userService.GetUserAsync(userId, ct);

        if (!result.IsSuccess)
            return NotFound();

        return Ok(result.Value);
    }

    [HttpGet("me")]
    public async Task<ActionResult<MyInfoResponse>> GetMe(CancellationToken ct)
    {
        if (!User.TryGetUserId(out var userId))
            return BadRequest();

        var result = await userService.GetMyInfoAsync(userId, ct);

        if (!result.IsSuccess)
            return NotFound();

        return Ok(result.Value);
    }

    [HttpPatch("me")]
    public async Task<ActionResult> UpdateMe([FromBody] UpdateMyInfoRequest request, CancellationToken ct)
    {
        if (!User.TryGetUserId(out var userId))
            return BadRequest();

        var result = await userService.UpdateUserAsync(userId, request, ct);

        if (!result.IsSuccess)
            return NotFound();

        return Ok();
    }

    [HttpDelete("me")]
    public async Task<ActionResult> DeleteMe(CancellationToken ct)
    {
        if (!User.TryGetUserId(out var userId))
            return BadRequest();

        var result = await userService.DeleteUserAsync(userId, ct);

        if (!result.IsSuccess)
            return NotFound();

        return Ok();
    }

    [HttpGet("me/quizzes")]
    public async Task<ActionResult<List<QuizResponse>>> GetMyQuizzes()
    {
        if (!User.TryGetUserId(out var userId))
            return BadRequest();

        throw new NotImplementedException();
    }
}
