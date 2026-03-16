using Quizate.Application.Common.Result;
using Quizate.Application.Features.Quizzes.DTOs.Requests;

namespace Quizate.Application.Features.Quizzes.Interfaces;

public interface ITopicCommandService
{
    public Task<Result> CreateTopicAsync(CreateTopicRequest request);
    public Task<Result> UpdateTopicAsync(UpdateTopicRequest request, string topicName);
    public Task<Result> DeleteTopicAsync(string topicName);
}