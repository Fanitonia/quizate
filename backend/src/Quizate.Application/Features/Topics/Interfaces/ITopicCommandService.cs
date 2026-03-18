using Quizate.Application.Common.Result;
using Quizate.Application.Features.Topics.DTOs.Requests;
using Quizate.Application.Features.Topics.DTOs.Responses;

namespace Quizate.Application.Features.Topics.Interfaces;

public interface ITopicCommandService
{
    public Task<Result<TopicResponse>> CreateTopicAsync(CreateTopicRequest request);
    public Task<Result> UpdateTopicAsync(UpdateTopicRequest request, string topicName);
    public Task<Result> DeleteTopicAsync(string topicName);
}