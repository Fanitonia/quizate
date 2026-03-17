using Quizate.Domain.Constants;
using System.Text.Json.Serialization;

namespace Quizate.Domain.Objects.Questions;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(MultipleChoiceQuestionObject), QuestionTypes.MultipleChoice)]
public abstract class QuestionObject
{
    [JsonIgnore]
    public abstract string QuestionType { get; }
}
