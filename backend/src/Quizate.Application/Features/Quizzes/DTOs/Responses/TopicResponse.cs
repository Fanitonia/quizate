namespace Quizate.Application.Features.Quizzes.DTOs.Responses;

public class TopicResponse
{
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public string? Description { get; set; }
}
