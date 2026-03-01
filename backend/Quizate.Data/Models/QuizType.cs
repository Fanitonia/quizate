using System;

namespace Quizate.Data.Models;

public class QuizType
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public required string Name { get; set; }
    public required string DisplayName { get; set; }

    public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
