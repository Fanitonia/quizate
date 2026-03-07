using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Contracts;
using Quizate.API.Contracts.Question;
using Quizate.API.Helpers.Extensions;
using Quizate.API.Services.Quiz;

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