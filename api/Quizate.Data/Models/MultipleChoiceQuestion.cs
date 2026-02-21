using System;

namespace Quizate.Data.Models;

public class MultipleChoiceQuestion
{
    public int Id { get; set; }

    public Quiz? Quiz { get; set; }
    public Guid QuizId { get; set; }

    public required string Text { get; set; }
    public string? ImageUrl { get; set; }
    public int DisplayOrder { get; set; }

    public ICollection<MultipleChoiceOption> Options { get; set; } = new List<MultipleChoiceOption>();
}
