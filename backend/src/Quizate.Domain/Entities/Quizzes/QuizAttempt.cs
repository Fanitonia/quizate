using Quizate.Domain.Entities.Users;

namespace Quizate.Domain.Entities.Quizzes;

public class QuizAttempt
{
    public int Id { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public User? User { get; set; }
    public Guid? UserId { get; private set; }

    public Quiz Quiz { get; set; } = null!;
    public Guid QuizId { get; private set; }

    public int Score { get; private set; }

    public QuizAttempt(Guid quizId, int score, int id = default, Guid? userId = null, DateTime createdAt = default)
    {
        Id = id;
        QuizId = quizId;
        UserId = userId;
        Score = score;
        CreatedAt = createdAt == default ? DateTime.UtcNow : createdAt;
    }
}