using Microsoft.EntityFrameworkCore;
using Quizate.Application.Features.Topics.DTOs.Responses;
using Quizate.Application.Features.Topics.Interfaces;
using Quizate.Persistence;

namespace Quizate.Application.Features.Topics.Services;

public class TopicQueryService(
    QuizateDbContext context) : ITopicQueryService
{
    public async Task<ICollection<TopicResponse>> GetTopicsAsync(CancellationToken ct)
    {
        var topics = await context.QuizTopics
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .Select(t => new TopicResponse
            {
                Name = t.Name,
                DisplayName = t.DisplayName,
                Description = t.Description,
                QuizCount = t.Quizzes.Count
            })
            .ToListAsync(ct);

        return topics;
    }
}
