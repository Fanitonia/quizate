using Quizate.Application.Features.Quizzes.Constants;
using System.Text.Json.Serialization;

namespace Quizate.Application.Features.Quizzes.DTOs.Responses.Objects;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(MultipleChoiceQuestionObject), QuestionTypes.MultipleChoice)]
public abstract class QuestionObject
{
}
