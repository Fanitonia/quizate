namespace Quizate.Core.Entities.Users;

public class RefreshToken
{
    public required string TokenHash { get; set; }
    public DateTime ExpiresAtUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
