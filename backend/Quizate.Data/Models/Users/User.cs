using Quizate.Data.Enums;

namespace Quizate.Data.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public required string Username { get; set; }
    public string NormalizedUsername { get; private set; } = null!;
    public string? Email { get; set; }
    public DateTime? EmailVerifiedAt { get; set; }
    public required string PasswordHash { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    public string? ProfilePictureUrl { get; set; }
    public bool IsDeleted { get; set; } = false;

    public ICollection<Quiz> Quizzes { get; set; } = [];
    public ICollection<QuizAttempt> QuizAttempts { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}
