using Quizate.Application.Features.Quizzes.DTOs.Responses;

namespace Quizate.Application.Features.Quizzes.Interfaces;

public interface ITopicQueryService
{
    public Task<ICollection<TopicResponse>> GetTopicsAsync(CancellationToken ct);
}
