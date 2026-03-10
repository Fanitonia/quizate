using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Quizzes.DTOs.Responses.Objects;
using Quizate.Application.Features.Quizzes.Helpers;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Application.Shared.Pagination;
using Quizate.Persistence;

namespace Quizate.Application.Features.Quizzes.Services;

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
            .Select(q => QuestionPayloadSerializer.DeserializeQuestionObject(q.QuestionTypeName, q.Payload))
            .OfType<QuestionObject>()
            .ToList();

        var response = new QuizQuestionsResponse
        {
            QuizId = quizId,
            Questions = questions
        };

        return response;
    }
}
