using System;

namespace Quizate.Data.Models;

public class MultipleChoiceOption
{
    public int Id { get; set; }

    public MultipleChoiceQuestion? Question { get; set; }
    public int QuestionId { get; set; }

    public required string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int DisplayOrder { get; set; }
}
