using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Shared.Pagination;

namespace Quizate.Application.Features.Quizzes.Interfaces;

public interface IQuizService
{
    public Task<(IEnumerable<QuizResponse>, PaginationMetadata)> GetQuizzesAsync(
        PaginationParameters pagination,
        CancellationToken ct,
        Guid? userId = null);

    public Task<QuizResponse?> GetQuizAsync(Guid quizId, CancellationToken ct);

    public Task<QuizQuestionsResponse?> GetQuestionsAsync(Guid quizId, CancellationToken ct);
}
