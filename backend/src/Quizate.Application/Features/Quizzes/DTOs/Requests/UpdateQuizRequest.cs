namespace Quizate.Application.Features.Quizzes.DTOs.Requests;

public class UpdateQuizRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool? IsPublic { get; set; }
    public string? LanguageCode { get; set; }
}
