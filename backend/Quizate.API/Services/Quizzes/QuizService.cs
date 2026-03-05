using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizate.API.Contracts;
using Quizate.API.Data;

namespace Quizate.API.Services.Quizzes;

public class QuizService(
    QuizateDbContext context,
    IMapper mapper) : IQuizService
{
    public async Task<(IEnumerable<QuizResponse>, PaginationMetadata?)> GetQuizzesAsync(
        PaginationParameters pagination,
        CancellationToken ct)
    {
        var result = await context.Quizzes
        .AsNoTracking()
        .Include(q => q.Creator)
        .Include(q => q.QuizType)
        .Include(q => q.MultipleChoiceQuestions)
        .Include(q => q.QuizAttempts)
        .Skip((pagination.PageNumber - 1) * pagination.PageSize)
        .Take(pagination.PageSize)
        .ToListAsync(ct);

        if (result.Count == 0)
            return ([], null);

        var totalCount = await context.Quizzes.CountAsync(ct);

        var paginationMetaData = new PaginationMetadata
        {
            PageSize = pagination.PageSize,
            CurrentPage = pagination.PageNumber,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize)
        };

        var quizzes = mapper.Map<List<QuizResponse>>(result);

        return (quizzes, paginationMetaData);
    }
}
