namespace Quizate.Data.Models;

public class QuizAttempt
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }
    public Guid? UserId { get; set; }

    public Quiz Quiz { get; set; } = null!;
    public Guid QuizId { get; set; }

    public int Score { get; set; }
}