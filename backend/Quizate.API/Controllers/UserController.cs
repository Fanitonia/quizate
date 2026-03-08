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
    [HttpGet("me/quizzes")]
    public async Task<ActionResult<List<QuizResponse>>> GetMyQuizzes()
    {
        if (!User.GetUserId(out var userId))
            return BadRequest();

        throw new NotImplementedException();
    }
}
