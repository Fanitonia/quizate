using Quizate.Application.Features.Topics.DTOs.Responses;

namespace Quizate.Application.Features.Topics.Interfaces;

public interface ITopicQueryService
{
    public Task<ICollection<TopicResponse>> GetTopicsAsync(CancellationToken ct);
}
