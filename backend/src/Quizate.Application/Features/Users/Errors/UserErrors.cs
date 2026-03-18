using Quizate.Application.Common.Result;

namespace Quizate.Application.Features.Users.Errors;

public static class UserErrors
{
    public static Error UsernameTaken(string username) => new("USERNAME_TAKEN", $"The username '{username}' is already taken.");
    public static Error EmailTaken(string email) => new("EMAIL_TAKEN", $"The email '{email}' is already taken.");
    public static Error IncorrectPassword => new("INCORRECT_PASSWORD", "The provided password is incorrect.");
}
