namespace Quizate.Core.Entities.Quizzes;

public class QuizLanguage
{
    public required string Code { get; set; }

    public ICollection<Quiz> Quizzes { get; set; } = [];
}
