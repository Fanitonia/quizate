using Quizate.Application.Quizzes.DTOs.Responses.Objects;

namespace Quizate.Application.Quizzes.DTOs.Responses;

public class QuizQuestionsResponse
{
    public Guid QuizId { get; set; }

    public ICollection<QuestionObject> Questions { get; set; } = [];
}
