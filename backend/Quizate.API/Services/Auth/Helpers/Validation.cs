using System.Text.RegularExpressions;

namespace Quizate.API.Auth;

public static class Validation
{
    public static bool IsValidUsername(string username)
    {
        return Regex.IsMatch(username, "^[A-Za-z0-9_]+$");
    }

    public static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
