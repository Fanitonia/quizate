using System.Security.Cryptography;
using System.Text;

namespace Quizate.API.Services.Auth;

public static class Hasher
{
    public static string ComputeHash(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        byte[] inputBytes = Encoding.UTF8.GetBytes(value);
        byte[] hashBytes = SHA256.HashData(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}
