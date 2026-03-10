using Quizate.Application.Quizzes.Constants;
using Quizate.Application.Quizzes.DTOs.Responses.Objects;
using System.Text.Json;

namespace Quizate.Application.Quizzes.Helpers;

public static class QuestionPayloadSerializer
{
    public static QuestionObject? DeserializeQuestionObject(string questionTypeName, string payload)
    {
        return questionTypeName switch
        {
            QuestionTypeNames.MultipleChoice => JsonSerializer.Deserialize<MultipleChoiceQuestionObject>(payload),
            _ => throw new NotSupportedException($"Question type '{questionTypeName}' is not supported.")
        };
    }
}
