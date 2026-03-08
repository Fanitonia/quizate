using Microsoft.AspNetCore.Mvc;
using Quizate.API.Contracts;
using Quizate.API.Helpers.Extensions;
using Quizate.API.Services.Users;

namespace Quizate.API.Controllers;

[Route("users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    // onur
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserInfoResponse>> GetUser(Guid userId, CancellationToken ct)
    {
        var result = await userService.GetUserAsync(userId, ct);

        if (!result.Success)
            return NotFound();

        return Ok(result.Data);
    }

    [HttpGet("me")]
    public async Task<ActionResult<MyInfoResponse>> GetMe(CancellationToken ct)
    {
        if (!User.GetUserId(out var userId))
            return BadRequest();

        var result = await userService.GetMyInfoAsync(userId, ct);

        if (!result.Success)
            return NotFound();

        return Ok(result.Data);
    }

    [HttpPatch("me")]
    public async Task<ActionResult> UpdateMe([FromBody] UpdateMyInfoRequest request, CancellationToken ct)
    {
        if (!User.GetUserId(out var userId))
            return BadRequest();

        var result = await userService.UpdateUserAsync(userId, request, ct);

        if (!result.Success)
            return NotFound();

        return Ok();
    }

    [HttpDelete("me")]
    public async Task<ActionResult> DeleteMe(CancellationToken ct)
    {
        if (!User.GetUserId(out var userId))
            return BadRequest();

        var result = await userService.DeleteUserAsync(userId, ct);

        if (!result.Success)
            return NotFound();

        return Ok();
    }

    [HttpGet("me/quizzes")]
    public async Task<ActionResult<List<QuizResponse>>> GetMyQuizzes()
    {
        if (!User.GetUserId(out var userId))
            return BadRequest();

        throw new NotImplementedException();
    }
}
