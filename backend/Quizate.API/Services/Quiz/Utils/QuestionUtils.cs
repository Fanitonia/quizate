using Quizate.API.Contracts.Question;
using System.Text.Json;

namespace Quizate.API.Services.Quiz.Utils;

public static class QuestionUtils
{
    public static QuestionResponse? MapToQuestionResponse(string questionTypeName, string payload)
    {
        return questionTypeName switch
        {
            QuestionTypeNames.MultipleChoice => JsonSerializer.Deserialize<MultipleChoiceQuestionResponse>(payload),
            _ => throw new NotSupportedException($"Question type '{questionTypeName}' is not supported.")
        };
    }
}
