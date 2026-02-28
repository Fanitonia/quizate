using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizate.API.Contracts;
using Quizate.API.Data;

namespace Quizate.API.Controllers
{
    [Route("quizzes")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizateDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuizController(QuizateDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuizResponse>>> GetQuizzes()
        {
            var quizzes = await _dbContext.Quizzes
                .AsNoTracking()
                .Include(q => q.Creator)
                .Include(q => q.QuizType)
                .Include(q => q.MultipleChoiceQuestions)
                .Include(q => q.QuizAttempts)
                .ToListAsync();

            if (quizzes.Count == 0)
                return NotFound("No quizzes found.");

            var response = _mapper.Map<List<QuizResponse>>(quizzes);

            return Ok(response);

        }
    }
}
