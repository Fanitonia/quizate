using Quizate.API.Contracts;

namespace Quizate.API.Services.Quizzes;

public interface IQuizService
{
    public Task<(IEnumerable<QuizResponse>, PaginationMetadata)> GetQuizzesAsync(
        PaginationParameters pagination,
        CancellationToken ct);

    public Task<QuizResponse?> GetQuizAsync(Guid quizId, CancellationToken ct);

    public Task<QuizQuestionsResponse?> GetQuestionsAsync(Guid quizId, CancellationToken ct);
}
