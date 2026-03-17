using Quizate.Domain.Constants;
using System.Text.Json.Serialization;

namespace Quizate.Domain.Objects.Questions;

public class MultipleChoiceQuestionObject : QuestionObject
{
    [JsonIgnore]
    public override string QuestionType => QuestionTypes.MultipleChoice;
    public required string Title { get; set; }
    public string? ImageUrl { get; set; }
    public int Points { get; set; }
    public required List<Option> Options { get; set; }

    public class Option
    {
        public required string Text { get; set; }
        public string? ImageUrl { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsCorrect { get; set; }
    }
}