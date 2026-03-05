using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Contracts;
using Quizate.API.Extensions.Utils;
using Quizate.API.Extensions.Validation;
using Quizate.API.Services.Quizzes;

namespace Quizate.API.Controllers;

[Route("quizzes")]
[ApiController]
public class QuizController(
    IQuizService quizService,
    IValidator<PaginationParameters> paginationValidator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<QuizResponse>>> GetQuizzes(
        [FromQuery] PaginationParameters pagination,
        CancellationToken ct)
    {
        var paginationValidation = paginationValidator.Validate(pagination);

        if (!paginationValidation.IsValid)
        {
            paginationValidation.AddToModelState(ModelState);
            return ValidationProblem();
        }

        var (quizzes, paginationMetaData) = await quizService.GetQuizzesAsync(pagination, ct);

        Response.Headers.Append("X-Pagination", paginationMetaData.SerializeWithCamelCasing());

        return Ok(quizzes);
    }

    [HttpGet("{quizId:guid}")]
    public async Task<ActionResult<QuizResponse>> GetQuiz(Guid quizId, CancellationToken ct)
    {
        var result = await quizService.GetQuiz(quizId, ct);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}