namespace Quizate.Data.Models;

public class QuizLanguage
{
    public required string Code { get; set; }

    public ICollection<Quiz> Quizzes { get; set; } = [];
}
