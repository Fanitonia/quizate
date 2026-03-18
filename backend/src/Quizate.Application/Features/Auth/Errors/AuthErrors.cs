using Quizate.Application.Common.Result;

namespace Quizate.Application.Features.Auth.Errors;

public static class AuthErrors
{
    public static Error UserExist => new("USER_EXIST", "User already exist.");
    public static Error InvalidCredentials = new("INVALID_CREDENTIALS", "Invalid username/email or password.");
    public static Error InvalidRefreshToken = new("INVALID_REFRESH_TOKEN", "Refresh token is either expired or invalid.");
}
