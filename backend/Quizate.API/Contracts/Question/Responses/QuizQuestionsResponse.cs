namespace Quizate.API.Contracts;

public class QuizQuestionsResponse
{
    public Guid QuizId { get; set; }

    public ICollection<QuestionObject> Questions { get; set; } = [];
}
