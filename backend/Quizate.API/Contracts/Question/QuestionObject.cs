using System.Text.Json.Serialization;

namespace Quizate.API.Contracts;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(MultipleChoiceQuestionObject), QuestionTypeNames.MultipleChoice)]
public abstract class QuestionObject
{
}
