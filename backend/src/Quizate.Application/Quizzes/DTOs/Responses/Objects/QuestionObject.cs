using Quizate.Application.Quizzes.Constants;
using System.Text.Json.Serialization;

namespace Quizate.Application.Quizzes.DTOs.Responses.Objects;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(MultipleChoiceQuestionObject), QuestionTypeNames.MultipleChoice)]
public abstract class QuestionObject
{
}
