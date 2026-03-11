namespace Quizate.Domain.Entities.Users;

public class RefreshToken
{
    public string TokenHash { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; set; } = null!;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;

    public RefreshToken(string tokenHash, Guid userId, DateTime expiresAtUtc, DateTime createdAtUtc = default)
    {
        TokenHash = tokenHash;
        ExpiresAtUtc = expiresAtUtc;
        CreatedAtUtc = createdAtUtc == default ? DateTime.UtcNow : createdAtUtc;
        UserId = userId;
    }
}
