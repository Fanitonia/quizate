using Quizate.Application.Common.Result;

namespace Quizate.Application.Features.Quizzes.Errors;

public static class QuizErrors
{
    public static Error InvalidTopics(string[] invalidTopics) => new("INVALID_TOPICS", $"Topic(s) not found: {string.Join(", ", invalidTopics)}");
}
