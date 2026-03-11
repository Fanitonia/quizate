using Quizate.Domain.Entities.Quizzes;
using Quizate.Domain.Enums;

namespace Quizate.Domain.Entities.Users;

public class User
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public string Username { get; private set; }
    public string NormalizedUsername { get; private set; } = null!;
    public string? Email { get; private set; }
    public DateTime? EmailVerifiedAt { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public bool IsDeleted { get; private set; }

    public ICollection<Quiz> Quizzes { get; set; } = [];
    public ICollection<QuizAttempt> QuizAttempts { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

    public User(
        string username,
        string passwordHash,
        string? email = null,
        string? profilePictureUrl = null,
        UserRole role = UserRole.User,
        Guid id = default,
        DateTime createdAt = default,
        DateTime updatedAt = default)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        ProfilePictureUrl = profilePictureUrl;
        Role = role;
        Id = id == default ? Guid.NewGuid() : id;
        CreatedAt = createdAt == default ? DateTime.UtcNow : createdAt;
        UpdatedAt = updatedAt == default ? DateTime.UtcNow : updatedAt;
        IsDeleted = false;
    }

    public void UpdateUsername(string username)
    {
        Username = username;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePasswordHash(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateRole(UserRole newRole)
    {
        Role = newRole;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProfilePicture(string newUrl)
    {
        ProfilePictureUrl = newUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkEmailAsVerified()
    {
        EmailVerifiedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RestoreUser()
    {
        IsDeleted = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
