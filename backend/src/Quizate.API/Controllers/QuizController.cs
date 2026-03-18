using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Extensions;
using Quizate.Application.Common.Errors;
using Quizate.Application.Common.Pagination;
using Quizate.Application.Common.Serializer;
using Quizate.Application.Features.Quizzes.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Domain.Enums;

namespace Quizate.API.Controllers;

[Route("quizzes")]
[ApiController]
public class QuizController(
    IQuizQueryService quizQuery,
    IQuizCommandService quizCommand,
    IQuizAuthorizationService quizAuth) : ControllerBase
{
    // onur
    [HttpGet]
    public async Task<ActionResult<List<QuizResponse>>> GetQuizzes(
        [FromQuery] PaginationParameters pagination,
        [FromQuery] Guid? userId,
        CancellationToken ct)
    {
        var result = await quizQuery.GetAllQuizzesAsync(pagination, ct, userId);

        Response.Headers.Append(Headers.XPagination, result.PaginationMetadata.SerializeWithCamelCasing());

        return Ok(result.Records);
    }

    // onur
    [HttpGet("{quizId:guid}")]
    public async Task<ActionResult<QuizResponse>> GetQuiz(Guid quizId, CancellationToken ct)
    {
        var quiz = await quizQuery.GetQuizAsync(quizId, ct);

        if (quiz == null)
            return NotFound();

        return Ok(quiz);
    }

    // onur
    [HttpGet("{quizId:guid}/questions")]
    public async Task<ActionResult<QuizQuestionsResponse>> GetQuestions(Guid quizId, CancellationToken ct)
    {
        var questions = await quizQuery.GetQuizQuestionsAsync(quizId, ct);

        if (questions == null)
            return NotFound();

        return Ok(questions);
    }

    // onur
    [HttpPost]
    public async Task<ActionResult> CreateQuiz(CreateQuizRequest request)
    {
        var result = await quizCommand.CreateQuizAsync(request);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetQuiz), new { quizId = result.Value!.Id }, result.Value);
    }

    // onur
    [Authorize]
    [HttpPatch("{quizId:guid}")]
    public async Task<ActionResult> UpdateQuiz(Guid quizId, [FromBody] UpdateQuizRequest request)
    {
        // topic updateleme ekle
        var isAdmin = User.IsInRole(UserRole.Admin.ToString());
        var canUpdate = isAdmin;

        if (!canUpdate && User.TryGetUserId(out var userId))
            canUpdate = await quizAuth.IsUserQuizOwner(quizId, userId);

        if (!canUpdate)
            return Forbid();

        var result = await quizCommand.UpdateQuizAsync(request, quizId);

        if (result.IsFailure)
        {
            if (result.Error == CommonErrors.NotFound)
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return NoContent();
    }

    // onur
    [Authorize]
    [HttpDelete("{quizId:guid}")]
    public async Task<ActionResult> DeleteQuiz(Guid quizId)
    {
        var isAdmin = User.IsInRole(UserRole.Admin.ToString());
        var canDelete = isAdmin;

        if (!canDelete && User.TryGetUserId(out var userId))
            canDelete = await quizAuth.IsUserQuizOwner(quizId, userId);

        if (!canDelete)
            return Forbid();

        var result = await quizCommand.DeleteQuizAsync(quizId);

        if (result.IsFailure)
        {
            if (result.Error == CommonErrors.NotFound)
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return NoContent();
    }
}