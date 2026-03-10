using Quizate.Core.Entities.Quizzes;
using System.Text.Json;

namespace Quizate.Core.Entities.Questions;

public class Question
{
    public int Id { get; set; }

    public Quiz Quiz { get; set; } = null!;
    public Guid QuizId { get; set; }

    public required string QuestionTypeName { get; set; }
    public QuestionType QuestionType { get; set; } = null!;

    public required string Payload { get; set; }
}
