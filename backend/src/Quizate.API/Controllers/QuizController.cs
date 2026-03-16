using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Extensions;
using Quizate.Application.Common.Pagination;
using Quizate.Application.Common.Serializer;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Quizzes.Interfaces;

namespace Quizate.API.Controllers;

[Route("quizzes")]
[ApiController]
public class QuizController(
    IQuizQueryService quizService) : ControllerBase
{
    // onur
    [HttpGet]
    public async Task<ActionResult<List<QuizResponse>>> GetQuizzes(
        [FromQuery] PaginationParameters pagination,
        [FromQuery] Guid? userId,
        CancellationToken ct)
    {
        var result = await quizService.GetAllQuizzesAsync(pagination, ct, userId);

        Response.SetHeader(Headers.XPagination, result.PaginationMetadata.SerializeWithCamelCasing());

        return Ok(result.Records);
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
        var questions = await quizService.GetQuizQuestionsAsync(quizId, ct);

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
    public async Task<ActionResult> UpdateQuiz(Guid quizId)
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