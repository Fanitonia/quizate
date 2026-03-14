using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizate.Application.Features.Quizzes.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Application.Shared.Result;
using Quizate.Domain.Entities.Quizzes;
using Quizate.Persistence;

namespace Quizate.Application.Features.Quizzes.Services;

public class TopicService(
    QuizateDbContext context,
    IMapper mapper) : ITopicService
{
    public async Task<Result> CreateTopic(CreateTopicRequest request)
    {
        request.Name = request.Name.ToLower().Trim();
        var newTopic = mapper.Map<QuizTopic>(request);

        context.QuizTopics.Add(newTopic);
        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteTopic(string topicName)
    {
        var topic = await context.QuizTopics.FindAsync(topicName);

        if (topic == null)
            return Result.Failure("Topic could not found.");

        context.QuizTopics.Remove(topic);
        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<ICollection<TopicResponse>> GetTopicsAsync()
    {
        var topics = await context.QuizTopics.AsNoTracking().ToListAsync();

        return mapper.Map<ICollection<TopicResponse>>(topics);
    }

    public async Task<Result> UpdateTopic(UpdateTopicRequest request, string topicName)
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
