using AutoMapper;
using Quizate.Application.Common.Errors;
using Quizate.Application.Common.Result;
using Quizate.Application.Features.Topics.DTOs.Requests;
using Quizate.Application.Features.Topics.DTOs.Responses;
using Quizate.Application.Features.Topics.Errors;
using Quizate.Application.Features.Topics.Interfaces;
using Quizate.Domain.Entities.Quizzes;
using Quizate.Persistence;

namespace Quizate.Application.Features.Topics.Services;

public class TopicCommandService(
    QuizateDbContext context,
    IMapper mapper) : ITopicCommandService
{
    public async Task<Result<TopicResponse>> CreateTopicAsync(CreateTopicRequest request)
    {
        request.Name = request.Name.ToLower().Trim();
        var newTopic = mapper.Map<QuizTopic>(request);

        var existingTopic = await context.QuizTopics.FindAsync(newTopic.Name);
        if (existingTopic != null)
            return TopicErrors.TopicAlreadyExists(newTopic.Name);

        context.QuizTopics.Add(newTopic);
        await context.SaveChangesAsync();

        return mapper.Map<TopicResponse>(newTopic);
    }

    public async Task<Result> DeleteTopicAsync(string topicName)
    {
        var topic = await context.QuizTopics.FindAsync(topicName);

        if (topic == null)
            return CommonErrors.NotFound;

        context.QuizTopics.Remove(topic);
        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateTopicAsync(UpdateTopicRequest request, string topicName)
    {
        var topic = await context.QuizTopics.FindAsync(topicName);

        if (topic == null)
            return CommonErrors.NotFound;

        if (request.DisplayName != null ||
            request.Description != null)
        {
            topic.Update(request.DisplayName, request.Description);
            await context.SaveChangesAsync();
        }
        else
        {
            return CommonErrors.UpdateRequestEmpty;
        }

        return Result.Success();
    }
}
