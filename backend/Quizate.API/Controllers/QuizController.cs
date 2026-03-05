using Microsoft.AspNetCore.Mvc;
using Quizate.API.Contracts;
using Quizate.API.Extensions.Utils;
using Quizate.API.Services.Quizzes;

namespace Quizate.API.Controllers;

// TODO: soruları da dahil etme, filtreleme, sıralama gibi özellikler ekle.
[Route("quizzes")]
[ApiController]
public class QuizController(IQuizService quizService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<QuizResponse>>> GetQuizzes(
        [FromQuery] PaginationParameters pagination,
        CancellationToken ct)
    {
        var (quizzes, paginationMetaData) = await quizService.GetQuizzesAsync(pagination, ct);

        if (!quizzes.Any())
            return NotFound();

        Response.Headers.Append("X-Pagination", paginationMetaData!.SerializeWithCamelCasing());

        return Ok(quizzes);
    }
}