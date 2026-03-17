namespace Quizate.Application.Features.Topics.DTOs.Requests;

public class CreateTopicRequest
{
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public string? Description { get; set; }
}
