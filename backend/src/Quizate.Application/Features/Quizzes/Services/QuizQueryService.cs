using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Quizate.Application.Common.Pagination;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Quizzes.Helpers;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Domain.Objects.Questions;
using Quizate.Persistence;

namespace Quizate.Application.Features.Quizzes.Services;

public class QuizQueryService(
    QuizateDbContext context,
    IMapper mapper) : IQuizQueryService
{
    public async Task<PaginatedList<QuizResponse>> GetAllQuizzesAsync(
        PaginationParameters pagination,
        CancellationToken ct,
        string? username = null)
    {
        var baseQuery = context.Quizzes
            .AsNoTracking();

        if (username != null)
        {
            var normalizedUsername = username.ToLowerInvariant();
            baseQuery = baseQuery.Where
                (q => q.Creator != null &&
                (q.Creator.Username == normalizedUsername));
        }

        baseQuery = baseQuery.OrderByDescending(q => q.CreatedAt);

        var totalCount = await baseQuery.CountAsync(ct);

        var result = await baseQuery
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<QuizResponse>(mapper.ConfigurationProvider)
            .ToListAsync(ct);

        var paginationMetaData = new PaginationMetadata(
            pagination, totalCount);

        return new(paginationMetaData, result);
    }

    public async Task<QuizResponse?> GetQuizAsync(Guid quizId, CancellationToken ct)
    {
        return await context.Quizzes
            .AsNoTracking()
            .Where(q => q.Id == quizId)
            .ProjectTo<QuizResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<QuizQuestionsResponse?> GetQuizQuestionsAsync(Guid quizId, CancellationToken ct)
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
