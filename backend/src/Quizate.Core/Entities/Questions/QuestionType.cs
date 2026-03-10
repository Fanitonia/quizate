using Quizate.Core.Entities.Questions;

public class QuestionType
{
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Question> Questions { get; set; } = [];
}