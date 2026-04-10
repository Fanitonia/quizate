using Quizate.Application.Features.Languages.DTOs.Responses;

namespace Quizate.Application.Features.Languages.Interfaces;

public interface ILanguageQueryService
{
    public Task<IEnumerable<LanguageResponse>> GetAllAsync(CancellationToken cancellationToken, bool includeQuizCount = false);
}
