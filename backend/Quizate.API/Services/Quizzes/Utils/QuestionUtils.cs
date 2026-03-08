using Quizate.API.Contracts;
using System.Text.Json;

namespace Quizate.API.Services.Quizzes.Utils;

public static class QuestionUtils
{
    public static QuestionObject? SerializeToQuestionObject(string questionTypeName, string payload)
    {
        return questionTypeName switch
        {
            QuestionTypeNames.MultipleChoice => JsonSerializer.Deserialize<MultipleChoiceQuestionObject>(payload),
            _ => throw new NotSupportedException($"Question type '{questionTypeName}' is not supported.")
        };
    }
}
