using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizate.API.Contracts;
using Quizate.API.Contracts.Question;

namespace Quizate.API.Services.Quiz;

public interface IQuizService
{
    public Task<(IEnumerable<QuizResponse>, PaginationMetadata)> GetQuizzesAsync(
        PaginationParameters pagination,
        CancellationToken ct);

    public Task<QuizResponse?> GetQuizAsync(Guid quizId, CancellationToken ct);

    public Task<QuizQuestionsResponse?> GetQuestionsAsync(Guid quizId, CancellationToken ct);
}
