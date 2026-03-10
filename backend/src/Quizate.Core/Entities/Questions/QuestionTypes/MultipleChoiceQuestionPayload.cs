namespace Quizate.Core.Entities.Questions.QuestionTypes;

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
