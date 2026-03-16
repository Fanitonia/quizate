using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Persistence;

namespace Quizate.Application.Features.Quizzes.Services;

public class TopicQueryService(
    QuizateDbContext context,
    IMapper mapper) : ITopicQueryService
{
    public async Task<ICollection<TopicResponse>> GetTopicsAsync(CancellationToken ct)
    {
        var topics = await context.QuizTopics
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .ToListAsync(ct);

        return mapper.Map<ICollection<TopicResponse>>(topics);
    }
}
