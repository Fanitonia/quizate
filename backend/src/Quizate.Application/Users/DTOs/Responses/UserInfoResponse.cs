namespace Quizate.Application.Users.DTOs.Responses;

public class UserInfoResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Username { get; set; }
    public required string NormalizedUsername { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
