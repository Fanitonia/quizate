namespace Quizate.Domain.Entities.Questions.QuestionTypes;

/// <summary>
/// Represents the payload for a multiple-choice question. stored as JSON in the Question.Payload property
/// </summary>
public class MultipleChoiceQuestionPayload
{
    public required string Title { get; set; }
    public string? ImageUrl { get; set; }
    public int Points { get; set; }
    public required List<Option> Options { get; set; }

    public class Option
    {
        public required string Text { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsCorrect { get; set; }
        public int DisplayOrder { get; set; }
    }
}
