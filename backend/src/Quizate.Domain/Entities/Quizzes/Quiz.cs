using Quizate.Domain.Entities.Questions;
using Quizate.Domain.Entities.Users;

namespace Quizate.Domain.Entities.Quizzes;

public class Quiz
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Guid? CreatorId { get; private set; }
    public User? Creator { get; set; }

    public string LanguageCode { get; private set; }
    public QuizLanguage Language { get; set; } = null!;

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public string? ThumbnailUrl { get; private set; }
    public bool IsPublic { get; private set; }

    public ICollection<Question> Questions { get; set; } = [];
    public ICollection<QuizTopic> Topics { get; set; } = [];
    public ICollection<QuizAttempt> Attempts { get; set; } = [];

    public Quiz(
        string title,
        string languageCode,
        Guid? creatorId = null,
        string? description = null,
        string? thumbnailUrl = null,
        bool isPublic = true,
        Guid id = default,
        DateTime createdAt = default,
        DateTime updatedAt = default)
    {
        Id = id;
        Title = title;
        LanguageCode = languageCode;
        CreatorId = creatorId;
        Description = description;
        ThumbnailUrl = thumbnailUrl;
        IsPublic = isPublic;
        CreatedAt = createdAt == default ? DateTime.UtcNow : createdAt;
        UpdatedAt = updatedAt == default ? DateTime.UtcNow : updatedAt;
    }

    public void UpdateTitle(string? title)
    {
        Title = title ?? Title;
        if (title != null)
            UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string? description)
    {
        Description = description ?? Description;
        if (description != null)
            UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateThumbnailUrl(string? thumbnailUrl)
    {
        ThumbnailUrl = thumbnailUrl ?? ThumbnailUrl;
        if (thumbnailUrl != null)
            UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateVisibiltity(bool? isPublic)
    {
        IsPublic = isPublic ?? IsPublic;
        if (isPublic != null)
            UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateLanguage(string? languageCode)
    {
        LanguageCode = languageCode ?? LanguageCode;
        if (languageCode != null)
            UpdatedAt = DateTime.UtcNow;
    }
}