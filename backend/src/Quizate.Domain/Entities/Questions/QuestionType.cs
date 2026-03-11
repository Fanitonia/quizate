namespace Quizate.Domain.Entities.Questions;

public class QuestionType
{
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public ICollection<Question> Questions { get; set; } = [];

    public QuestionType(string name, DateTime createdAt = default, DateTime updatedAt = default)
    {
        Name = name;
        CreatedAt = createdAt == default ? DateTime.UtcNow : createdAt;
        UpdatedAt = updatedAt == default ? DateTime.UtcNow : updatedAt;
    }
}