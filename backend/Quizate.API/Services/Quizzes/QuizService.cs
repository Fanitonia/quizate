using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Quizate.API.Contracts;
using Quizate.API.Data;

namespace Quizate.API.Services.Quizzes;

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

        var paginationMetaData = new PaginationMetadata
        {
            PageSize = pagination.PageSize,
            CurrentPage = pagination.PageNumber,
            TotalCount = totalCount,
            TotalPages = totalCount == 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pagination.PageSize)
        };

        return (result, paginationMetaData);
    }

    public async Task<QuizResponse?> GetQuiz(Guid quizId, CancellationToken ct)
    {
        return await context.Quizzes
            .AsNoTracking()
            .Where(q => q.Id == quizId)
            .ProjectTo<QuizResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }
}
