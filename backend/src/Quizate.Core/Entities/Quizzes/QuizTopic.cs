namespace Quizate.Core.Entities.Quizzes;

public class QuizTopic
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public string? Description { get; set; }

    public ICollection<Quiz> Quizzes { get; set; } = [];
}