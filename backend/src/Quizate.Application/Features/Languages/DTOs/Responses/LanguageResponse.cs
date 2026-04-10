namespace Quizate.Application.Features.Languages.DTOs.Responses;

public class LanguageResponse
{
    public required string Code { get; set; }
    public int? QuizCount { get; set; }
}
