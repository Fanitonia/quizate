namespace Quizate.Application.Features.Users.DTOs.Responses;

public class UserInfoResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Username { get; set; }
    public required string DisplayName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required string Role { get; set; }
}
