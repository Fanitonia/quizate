namespace Quizate.Application.Features.Users.DTOs.Responses;

public class DetailedUserInfoResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Username { get; set; }
    public required string NormalizedUsername { get; set; }
    public string? Email { get; set; }
    public bool IsEmailVerified { get; set; } = false;
    public string? ProfilePictureUrl { get; set; }
    public required string Role { get; set; }
}
