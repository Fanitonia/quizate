using AutoMapper;
using Quizate.Application.Common.Result;
using Quizate.Application.Features.Quizzes.DTOs.Requests;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Domain.Entities.Quizzes;
using Quizate.Persistence;

namespace Quizate.Application.Features.Quizzes.Services;

public class TopicCommandService(
    QuizateDbContext context,
    IMapper mapper) : ITopicCommandService
{
    public async Task<Result> CreateTopicAsync(CreateTopicRequest request)
    {
        request.Name = request.Name.ToLower().Trim();
        var newTopic = mapper.Map<QuizTopic>(request);

        context.QuizTopics.Add(newTopic);
        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteTopicAsync(string topicName)
    {
        var topic = await context.QuizTopics.FindAsync(topicName);

        if (topic == null)
            return Result.Failure("Topic could not found.");

        context.QuizTopics.Remove(topic);
        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateTopicAsync(UpdateTopicRequest request, string topicName)
    {
        var topic = await context.QuizTopics.FindAsync(topicName);

        if (topic == null)
            return Result.Failure("Topic could not found");

        if (request.DisplayName != null ||
            request.Description != null)
        {
            topic.Update(request.DisplayName, request.Description);
        }
        else
        {
            return Result.Failure("No fields to update.");
        }

        await context.SaveChangesAsync();

        return Result.Success();
    }
}
