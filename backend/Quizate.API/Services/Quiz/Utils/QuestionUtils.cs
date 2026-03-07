using Quizate.API.Contracts.Question;
using System.Text.Json;

namespace Quizate.API.Services.Quiz.Utils;

public static class QuestionUtils
{
    public static QuestionObject? MapToQuestionResponse(string questionTypeName, string payload)
    {
        return questionTypeName switch
        {
            QuestionTypeNames.MultipleChoice => JsonSerializer.Deserialize<MultipleChoiceQuestionObject>(payload),
            _ => throw new NotSupportedException($"Question type '{questionTypeName}' is not supported.")
        };
    }
}
