using Quizate.Application.Common.Result;

namespace Quizate.Application.Features.Topics.Errors;

public static class TopicErrors
{
    public static Error TopicAlreadyExists(string topicName) => new("TOPIC_ALREADY_EXISTS", $"Topic with name '{topicName}' already exists.");
    public static Error TopicsNotFound(string[] topicNames) => new("TOPICS_NOT_FOUND", $"Topic(s) with name(s) '{string.Join(", ", topicNames)}' not found.");
}
