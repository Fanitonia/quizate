using System;

namespace Quizate.Data.Models;

public class Quiz
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsPublic { get; set; }

    public User? Creator { get; set; }
    public Guid? CreatorId { get; set; }

    public QuizType QuizType { get; set; } = null!;
    public Guid QuizTypeId { get; set; }
}