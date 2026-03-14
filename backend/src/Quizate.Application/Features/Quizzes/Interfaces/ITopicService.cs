using Quizate.Application.Features.Quizzes.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Shared.Result;

namespace Quizate.Application.Features.Quizzes.Interfaces;

public interface ITopicService
{
    public Task<ICollection<TopicResponse>> GetTopicsAsync();
    public Task<Result> CreateTopic(CreateTopicRequest request);
    public Task<Result> UpdateTopic(UpdateTopicRequest request, string topicName);
    public Task<Result> DeleteTopic(string topicName);
}
