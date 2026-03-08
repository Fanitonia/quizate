namespace Quizate.API.Contracts;

public class MyInfoResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Username { get; set; }
    public required string NormalizedUsername { get; set; }
    public string? Email { get; set; }
    public bool IsEmailVerified { get; set; } = false;
    public string? ProfilePictureUrl { get; set; }
}
