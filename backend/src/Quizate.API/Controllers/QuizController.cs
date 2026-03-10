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
    [HttpGet]
    public async Task<ActionResult<List<QuizResponse>>> GetQuizzes(
        [FromQuery] PaginationParameters pagination,
        CancellationToken ct)
    {
        var (quizzes, paginationMetaData) = await quizService.GetQuizzesAsync(pagination, ct);

        Response.Headers.Append("X-Pagination", paginationMetaData.SerializeWithCamelCasing());

        return Ok(quizzes);
    }

    [HttpGet("{quizId:guid}")]
    public async Task<ActionResult<QuizResponse>> GetQuiz(Guid quizId, CancellationToken ct)
    {
        var quiz = await quizService.GetQuizAsync(quizId, ct);

        if (quiz == null)
            return NotFound();

        return Ok(quiz);
    }

    [HttpGet("{quizId:guid}/questions")]
    public async Task<ActionResult<QuizQuestionsResponse>> GetQuestions(Guid quizId, CancellationToken ct)
    {
        var questions = await quizService.GetQuestionsAsync(quizId, ct);

        if (questions == null)
            return NotFound();

        return Ok(questions);
    }
}