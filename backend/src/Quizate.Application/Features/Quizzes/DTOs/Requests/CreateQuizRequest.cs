using Quizate.Domain.Objects.Questions;

namespace Quizate.Application.Features.Quizzes.DTOs.Requests;

public class CreateQuizRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsPublic { get; set; }
    public required string LanguageCode { get; set; }
    public required string[] Topics { get; set; }
    public required List<QuestionObject> Questions { get; set; } = [];
}
