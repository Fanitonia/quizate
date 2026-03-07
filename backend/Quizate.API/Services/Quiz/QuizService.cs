using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Quizate.API.Contracts;
using Quizate.API.Contracts.Question;
using Quizate.API.Data;
using Quizate.API.Services.Quiz.Utils;

namespace Quizate.API.Services.Quiz;

public class QuizService(
    QuizateDbContext context,
    IMapper mapper) : IQuizService
{
    public async Task<(IEnumerable<QuizResponse>, PaginationMetadata)> GetQuizzesAsync(
        PaginationParameters pagination,
        CancellationToken ct)
    {
        var result = await context.Quizzes
            .AsNoTracking()
            .OrderByDescending(q => q.CreatedAt)
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<QuizResponse>(mapper.ConfigurationProvider)
            .ToListAsync(ct);

        var totalCount = await context.Quizzes
            .AsNoTracking()
            .CountAsync(ct);

        var paginationMetaData = new PaginationMetadata(
            pagination.PageSize, pagination.PageNumber, totalCount);

        return (result, paginationMetaData);
    }

    public async Task<QuizResponse?> GetQuizAsync(Guid quizId, CancellationToken ct)
    {
        return await context.Quizzes
            .AsNoTracking()
            .Where(q => q.Id == quizId)
            .ProjectTo<QuizResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<QuizQuestionsResponse?> GetQuestionsAsync(Guid quizId, CancellationToken ct)
    {
        var questionRows = await context.Questions
             .AsNoTracking()
             .Where(q => q.QuizId == quizId)
             .Select(q => new { q.QuestionTypeName, q.Payload })
             .ToListAsync(ct);

        if (questionRows.Count == 0)
            return null;

        var questions = questionRows
            .Select(q => QuestionUtils.MapToQuestionResponse(q.QuestionTypeName, q.Payload))
            .OfType<QuestionResponse>()
            .ToList();

        var response = new QuizQuestionsResponse
        {
            QuizId = quizId,
            Questions = questions
        };

        return response;
    }
}
