using System.Text.Json.Serialization;

namespace Quizate.API.Contracts.Question;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(MultipleChoiceQuestionResponse), QuestionTypeNames.MultipleChoice)]
public abstract class QuestionResponse
{
}
