namespace Quizate.Application.Features.Users.DTOs.Requests;

public class UpdateUserRequest
{
    public string? Username { get; set; }
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
}
