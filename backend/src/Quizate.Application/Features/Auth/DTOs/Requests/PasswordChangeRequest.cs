namespace Quizate.Application.Features.Auth.DTOs.Requests;

public class PasswordChangeRequest
{
    public required string NewPassword { get; set; }
    public required string OldPassword { get; set; }
}
