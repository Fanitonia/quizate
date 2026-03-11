using Quizate.Domain.Entities.Quizzes;

namespace Quizate.Domain.Entities.Questions;

public class Question
{
    public int Id { get; private set; }

    public Quiz Quiz { get; private set; } = null!;
    public Guid QuizId { get; private set; }

    public string QuestionTypeName { get; private set; }
    public QuestionType QuestionType { get; set; } = null!;

    public string Payload { get; set; }

    public Question(Guid quizId, string questionTypeName, string payload, int id = default)
    {
        Id = id;
        QuizId = quizId;
        QuestionTypeName = questionTypeName;
        Payload = payload;
    }
}
