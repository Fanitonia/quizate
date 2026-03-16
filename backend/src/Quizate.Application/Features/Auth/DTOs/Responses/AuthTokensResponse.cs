namespace Quizate.Application.Features.Auth.DTOs.Responses;

public class AuthTokensResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
