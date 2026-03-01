using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizate.API.Contracts;
using Quizate.API.Data;

namespace Quizate.API.Controllers
{
    [Route("quizzes")]
    [ApiController]
    public class QuizController(QuizateDbContext dbContext, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<QuizResponse>>> GetQuizzes()
        {
            var quizzes = await dbContext.Quizzes
                .AsNoTracking()
                .Include(q => q.Creator)
                .Include(q => q.QuizType)
                .Include(q => q.MultipleChoiceQuestions)
                .Include(q => q.QuizAttempts)
                .ToListAsync();

            if (quizzes.Count == 0)
                return NotFound("No quizzes found.");

            var response = mapper.Map<List<QuizResponse>>(quizzes);

            return Ok(response);

        }
    }
}
