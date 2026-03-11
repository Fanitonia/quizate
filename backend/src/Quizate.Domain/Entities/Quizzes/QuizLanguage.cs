namespace Quizate.Domain.Entities.Quizzes;

public class QuizLanguage
{
    public string Code { get; private set; }

    public ICollection<Quiz> Quizzes { get; set; } = [];

    public QuizLanguage(string code)
    {
        Code = code;
    }
}
