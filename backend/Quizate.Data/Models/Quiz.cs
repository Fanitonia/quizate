using System;

namespace Quizate.Data.Models;

public class Quiz
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public User? Creator { get; set; }
    public Guid? CreatorId { get; set; }

    public QuizType? QuizType { get; set; }
    public Guid QuizTypeId { get; set; }

    public Language? Language { get; set; }
    public required string LanguageCode { get; set; }

    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsPublic { get; set; }

    public ICollection<MultipleChoiceQuestion>? MultipleChoiceQuestions { get; set; } = new List<MultipleChoiceQuestion>();
    public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    public ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
}