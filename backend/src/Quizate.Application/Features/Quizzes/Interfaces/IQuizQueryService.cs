using Quizate.Application.Common.Pagination;
using Quizate.Application.Features.Quizzes.DTOs.Responses;

namespace Quizate.Application.Features.Quizzes.Interfaces;

public interface IQuizQueryService
{
    public Task<PaginatedList<QuizResponse>> GetAllQuizzesAsync(
        PaginationParameters pagination,
        CancellationToken ct,
        Guid? userId = null);

    public Task<QuizResponse?> GetQuizAsync(Guid quizId, CancellationToken ct);

    public Task<QuizQuestionsResponse?> GetQuizQuestionsAsync(Guid quizId, CancellationToken ct);
}
