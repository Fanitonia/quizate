namespace Quizate.Domain.Entities.Quizzes;

public class QuizTopic
{
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public string Name { get; private set; }
    public string DisplayName { get; private set; }
    public string? Description { get; private set; }

    public ICollection<Quiz> Quizzes { get; set; } = [];

    public QuizTopic(string name, string displayName, string? description = null, DateTime createdAt = default, DateTime updatedAt = default)
    {
        Name = name;
        DisplayName = displayName;
        Description = description;
        CreatedAt = createdAt == default ? DateTime.UtcNow : createdAt;
        UpdatedAt = updatedAt == default ? DateTime.UtcNow : updatedAt;
    }

    public void Update(string? displayName = null, string? description = null)
    {
        DisplayName = displayName ?? DisplayName;
        Description = description ?? Description;

        if (displayName != null || description != null)
            UpdatedAt = DateTime.UtcNow;
    }
}