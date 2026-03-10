namespace Quizate.Application.Features.Auth.DTOs.Requests;

public class RegisterRequest
{
    public required string Username { get; set; }
    public string? Email { get; set; }
    public required string Password { get; set; }
}