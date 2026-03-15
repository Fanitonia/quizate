using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Application.Shared.Pagination;
using Quizate.Application.Shared.Serializer;

namespace Quizate.API.Controllers;

[Route("quizzes")]
[ApiController]
public class QuizController(
    IQuizService quizService) : ControllerBase
{
    // onur
    [HttpGet]
    public async Task<ActionResult<List<QuizResponse>>> GetQuizzes(
        [FromQuery] PaginationParameters pagination,
        [FromQuery] Guid? userId,
        CancellationToken ct)
    {
        var (quizzes, paginationMetadata) = await quizService.GetQuizzesAsync(pagination, ct, userId);

        Response.Headers.Append("X-Pagination", paginationMetadata.SerializeWithCamelCasing());

        return Ok(quizzes);
    }

    // onur
    [HttpGet("{quizId:guid}")]
    public async Task<ActionResult<QuizResponse>> GetQuiz(Guid quizId, CancellationToken ct)
    {
        var quiz = await quizService.GetQuizAsync(quizId, ct);

        if (quiz == null)
            return NotFound();

        return Ok(quiz);
    }

    // onur
    [HttpGet("{quizId:guid}/questions")]
    public async Task<ActionResult<QuizQuestionsResponse>> GetQuestions(Guid quizId, CancellationToken ct)
    {
        var questions = await quizService.GetQuestionsAsync(quizId, ct);

        if (questions == null)
            return NotFound();

        return Ok(questions);
    }

    // onur
    [HttpPost]
    public async Task<ActionResult> CreateQuiz()
    {
        throw new NotImplementedException();
    }

    // onur
    [Authorize]
    [HttpPatch("{quizId:guid}")]
    public async Task<ActionResult> UpdateQuiz()
    {
        throw new NotImplementedException();
    }

    // onur
    [HttpDelete("{quizId:guid}")]
    public async Task<ActionResult> DeleteQuiz()
    {
        throw new NotImplementedException();
    }
}