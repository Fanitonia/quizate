using System;

namespace Quizate.Data.Models;

public class Quiz
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public User? Creator { get; set; }
    public Guid? CreatorId { get; set; }

    public required string LanguageCode { get; set; }
    public QuizLanguage Language { get; set; } = null!;

    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsPublic { get; set; }

    public ICollection<Question> Questions { get; set; } = [];
    public ICollection<QuizTopic> Topics { get; set; } = [];
    public ICollection<QuizAttempt> Attempts { get; set; } = [];
}