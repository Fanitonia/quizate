namespace Quizate.API.Contracts.Question;

public class QuizQuestionsResponse
{
    public Guid QuizId { get; set; }

    public ICollection<QuestionResponse> Questions { get; set; } = [];
}
