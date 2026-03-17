using Quizate.Domain.Objects.Questions;

namespace Quizate.Application.Features.Quizzes.DTOs.Responses;

public class QuizQuestionsResponse
{
    public Guid QuizId { get; set; }

    public ICollection<QuestionObject> Questions { get; set; } = [];
}
