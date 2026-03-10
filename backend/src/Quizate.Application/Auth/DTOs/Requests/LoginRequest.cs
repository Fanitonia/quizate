namespace Quizate.Application.Auth.DTOs.Requests;

public class LoginRequest
{
    public required string UsernameOrEmail { get; set; }
    public required string Password { get; set; }
}