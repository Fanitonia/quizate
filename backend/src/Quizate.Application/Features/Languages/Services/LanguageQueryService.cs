using Microsoft.EntityFrameworkCore;
using Quizate.Application.Features.Languages.DTOs.Responses;
using Quizate.Application.Features.Languages.Interfaces;
using Quizate.Persistence;

namespace Quizate.Application.Features.Languages.Services;

public class LanguageQueryService(QuizateDbContext context) : ILanguageQueryService
{
    public async Task<IEnumerable<LanguageResponse>> GetAllAsync(CancellationToken cancellationToken, bool includeQuizCount = false)
    {
        return await context.QuizLanguages
            .AsNoTracking()
            .Select(l => new LanguageResponse
            {
                Code = l.Code,
                QuizCount = includeQuizCount ? l.Quizzes.Count : null
            }).ToListAsync(cancellationToken);
    }
}
