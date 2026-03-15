using Quizate.Application.Features.Quizzes.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Shared.Result;

namespace Quizate.Application.Features.Quizzes.Interfaces;

public interface ITopicService
{
    public Task<ICollection<TopicResponse>> GetTopicsAsync(CancellationToken ct);
    public Task<Result> CreateTopicAsync(CreateTopicRequest request);
    public Task<Result> UpdateTopicAsync(UpdateTopicRequest request, string topicName);
    public Task<Result> DeleteTopicAsync(string topicName);
}
